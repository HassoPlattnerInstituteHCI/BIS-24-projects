using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class ObjectHandler : MonoBehaviour
{
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    public GameObject wall;
    public GameObject obj;
    private ObjectSelector _objectSelector;
    private bool wallPlacementStarted;
    private Vector3 wallPos1;
    private Vector3 wallPos2;
    private GameObject hoveredObject = null;
    private SpeechOut _speechOut;

    public float doorSize = 1f;

    private SoundManager soundManager;

    void Start()
    {
        _objectSelector = GetComponent<ObjectSelector>();
        soundManager = GameObject.FindObjectsOfType<SoundManager>()[0];

        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();

        _speechOut = new SpeechOut();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlacedObject") 
        {
            hoveredObject = collision.gameObject;
            _speechOut.Speak(collision.gameObject.GetComponent<Object>().name);
        }

        if (collision.gameObject.tag == "Wall") 
        {
            hoveredObject = collision.gameObject;
        }
    }

    void OnCollisionLeave(Collision collision)
    {
        if (collision.gameObject.tag == "PlacedObject" || collision.gameObject.tag == "Wall") 
        {
            hoveredObject = null;
        }
    }

    public void placeWall()
    {
        soundManager.playPlaceSound();
        if (wallPlacementStarted) 
        {
            _speechOut.Speak("Wall placed.");

            wallPos2 = _upperHandle.GetPosition();

            GameObject clone = Instantiate(wall, (wallPos1+wallPos2)/2, Quaternion.LookRotation((wallPos2-wallPos1), Vector3.up));
            clone.transform.localScale = new Vector3(1, 1, (wallPos2-wallPos1).magnitude);

            PantoCollider _pantoCollider = clone.GetComponent<PantoCollider>();

            _pantoCollider.CreateObstacle();
            _pantoCollider.Enable();

            wallPlacementStarted = false;

            GameObject level = GameObject.FindGameObjectsWithTag("Level")[0];

            if (level.name == "Level 3(Clone)")
            {
                level.GetComponent<Level3>().wallPlaced();
            }
        } else 
        {
            wallPos1 = _upperHandle.GetPosition();
            wallPlacementStarted = true;
        }
        
    }

    public void placeObject(string name) 
    {
        soundManager.playPlaceSound();
        GameObject o = Instantiate(obj, _upperHandle.GetPosition(), Quaternion.identity);
        o.GetComponent<Object>().name = name;

        GameObject level = GameObject.FindGameObjectsWithTag("Level")[0];

        // place sound
        _speechOut.Speak(name + " placed");

        if (level.name == "Level 2(Clone)")
        {
            level.GetComponent<Level2>().objectPlaced(name);
        }
    }

    public void destroyAllObjectsWithTag(string tag)
    {
        GameObject[] objects;

        objects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject g in objects)
        {
            Destroy(g);
        }
    }

    public void destroyHoveredObject()
    {
        soundManager.playDestroySound();
        if (hoveredObject)
        {
            if (hoveredObject.tag == "Wall") 
            {
                hoveredObject.GetComponent<PantoCollider>().Remove();
                _speechOut.Speak("Wall removed.");
            } 
            else
            {
                _speechOut.Speak(hoveredObject.GetComponent<Object>().name + " removed.");
            }
            Destroy(hoveredObject);
            // delete sound
        }
    }

    public void makeDoor()
    {
        if (!hoveredObject || hoveredObject.tag != "Wall") return;

        Vector3 wallMid = hoveredObject.transform.position;

        float wallLength;
        Quaternion wallRotation;

        
        wallLength = hoveredObject.transform.localScale.z;
        wallRotation = Quaternion.Euler(0, hoveredObject.transform.rotation.y+90, 0);

        Vector3 wallBegin = wallMid - hoveredObject.transform.rotation * (new Vector3(0, 0, wallLength/2));
        Vector3 wallEnd = wallMid + hoveredObject.transform.rotation * (new Vector3(0, 0, wallLength/2));

        Vector3 doorPosition = wallBegin + Vector3.Project(_upperHandle.GetPosition() - wallBegin, wallEnd - wallBegin);

        Vector3 doorBegin = doorPosition - hoveredObject.transform.rotation * (new Vector3(0, 0, doorSize/2));
        Vector3 doorEnd = doorPosition + hoveredObject.transform.rotation * (new Vector3(0, 0, doorSize/2));

        Vector3 wall1Mid = (wallBegin + doorBegin)/2;
        Vector3 wall2Mid = (doorEnd + wallEnd)/2;

        GameObject wall1 = Instantiate(wall, wall1Mid, hoveredObject.transform.rotation);
        GameObject wall2 = Instantiate(wall, wall2Mid, hoveredObject.transform.rotation);
        
        wall1.transform.localScale = new Vector3(1,1,(wallBegin - doorBegin).magnitude);
        wall2.transform.localScale = new Vector3(1,1,(doorEnd - wallEnd).magnitude);

        hoveredObject.GetComponent<PantoCollider>().Remove();
        Destroy(hoveredObject);

        wall1.GetComponent<PantoCollider>().CreateObstacle();
        wall2.GetComponent<PantoCollider>().CreateObstacle();

        wall1.GetComponent<PantoCollider>().Enable();
        wall2.GetComponent<PantoCollider>().Enable();


    }

    public void setHoveredObject(GameObject obj)
    {
        hoveredObject = obj;
    }

    public void resetHoveredObject(GameObject obj)
    {
        if (hoveredObject == obj)
        {
            hoveredObject = null;
        }
    }
}
