using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SpeachIO;
using DualPantoToolkit;

public class ObjectHandler : MonoBehaviour
{
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    public GameObject box;
    public GameObject circle;
    private ObjectSelector _objectSelector;
    private bool placementStarted;
    private Vector3 boxPos1;
    private Vector3 boxPos2;
    private Vector3 circleCenter;
    private Vector3 circleRadius;
    private GameObject hoveredObject = null;
    //private SpeechOut _speechOut;

    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    { 
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();
        _objectSelector = GetComponent<ObjectSelector>();
    }

    public void resetPlacementStarted(){
        placementStarted = false;
    }

    public void placeBox(){
        soundManager.playPlaceSound();
        if (placementStarted) 
        {
            //_speechOut.Speak("Textbox platziert.");

            boxPos2 = _upperHandle.GetPosition();

            //Check box rotation here
            GameObject clone = Instantiate(box, boxPos1,  Quaternion.identity);
            //ggf. mit .magnitude
            clone.transform.localScale = new Vector3(boxPos1.x - boxPos2.x, 1, boxPos1.z - boxPos2.z);

            PantoCollider _pantoCollider = clone.GetComponent<PantoCollider>();

            _pantoCollider.CreateObstacle();
            _pantoCollider.Enable();

            placementStarted = false;

        } else 
        {
            boxPos1 = _upperHandle.GetPosition();
            placementStarted = true;
        }
    }

    public void connectObjects(){
        soundManager.playPlaceSound();
        if (placementStarted) 
        {
            //_speechOut.Speak("Textbox editiert.");

            boxPos2 = hoveredObject.transform.position;

            //Check box rotation here
            GameObject clone = Instantiate(box, boxPos1, Quaternion.identity); //todo: Box ersetzen mit Pfeil oder andere Verbindung
            //ggf. mit .magnitude
            clone.transform.localScale = new Vector3(boxPos1.x - boxPos2.x, 1, boxPos1.z - boxPos2.z);

            PantoCollider _pantoCollider = clone.GetComponent<PantoCollider>();

            _pantoCollider.CreateObstacle();
            _pantoCollider.Enable();

            placementStarted = false;

        } else 
        {
            boxPos1 = hoveredObject.transform.position;
            placementStarted = true;
        }
    }

    public void editBox(){
        //soundManager.playPlaceSound();
        if (placementStarted) 
        {
            //_speechOut.Speak("Textbox editiert.");

            boxPos2 = _upperHandle.GetPosition();

            hoveredObject.transform.localScale = new Vector3(boxPos1.x - boxPos2.x, 1, boxPos1.z - boxPos2.z);

            PantoCollider _pantoCollider = hoveredObject.GetComponent<PantoCollider>();

            _pantoCollider.CreateObstacle();
            _pantoCollider.Enable();

            placementStarted = false;

        } else 
        {
            boxPos1 = hoveredObject.transform.position;
            placementStarted = true;
        }
    }


     public void placeCircle(){
        soundManager.playPlaceSound();
        if (placementStarted) 
        {
            //_speechOut.Speak("Kreis erstellt.");

            circleRadius = _upperHandle.GetPosition();

            //Check circle placement here
            GameObject clone = Instantiate(circle, circleCenter, Quaternion.identity);
            clone.transform.localScale = new Vector3((boxPos2-boxPos1).magnitude, 1, (boxPos2-boxPos1).magnitude);

            PantoCollider _pantoCollider = clone.GetComponent<PantoCollider>();

            _pantoCollider.CreateObstacle();
            _pantoCollider.Enable();

            placementStarted = false;

        } else 
        {
            circleCenter = _upperHandle.GetPosition();
            placementStarted = true;
        }
    }

    public void destroyHoveredObject()
    {
        //soundManager.playDestroySound();
        if (hoveredObject)
        {
            //_speechOut.Speak(hoveredObject.GetComponent<Object>().name + " entfernt.");
            Destroy(hoveredObject);
        }
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
