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
    public GameObject[] objects;
    private ObjectSelector _objectSelector;
    private bool wallPlacementStarted;
    private Vector3 wallPos1;
    private Vector3 wallPos2;
    private GameObject selectedObject = null;
    private SpeechOut _speechOut;

    // Start is called before the first frame update
    void Start()
    {
        _objectSelector = GetComponent<ObjectSelector>();

        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();

        _speechOut = new SpeechOut();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            place();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object") {
            selectedObject = collision.gameObject;
            _speechOut.Speak(collision.gameObject.GetComponent<Object>().name);
        }
    }

    void OnCollisionLeave(Collision collision)
    {
        if (collision.gameObject.tag == "Object") {
            selectedObject = null;
        }
    }

    void place() 
    {
        if (_objectSelector.selectedObjectName == "wall") {
            if (wallPlacementStarted) {
                wallPos2 = _upperHandle.GetPosition();
                placeWall();
                wallPlacementStarted = false;
            } else {
                wallPos1 = _upperHandle.GetPosition();
                wallPlacementStarted = true;
            }
        } else {
            placeObject();
        }
    }

    void placeWall()
    {
        GameObject clone = Instantiate(wall, (wallPos1+wallPos2)/2, Quaternion.LookRotation((wallPos2-wallPos1), Vector3.up));
        clone.transform.localScale = new Vector3(1, 1, (wallPos2-wallPos1).magnitude);

        PantoCollider _pantoCollider = clone.GetComponent<PantoCollider>();

        _pantoCollider.CreateObstacle();
        _pantoCollider.Enable();
    }

    void placeObject() 
    {

    }
}
