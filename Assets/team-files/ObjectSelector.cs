using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class ObjectSelector : MonoBehaviour
{
    public float rotU;
    public float rotL;
    public string selectedObjectName;
    private ObjectHandler objectHandler;
    private int selectedObjectId = 0;
    private string[] objectNames = {"chair", "table", "lamp", "wall", "remove tool", "door"}; // todo
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    public float upperZeroRotation;
    public float lowerZeroRotation;
    public bool upperTurned;
    public bool lowerTurned;

    public bool objectsSelectable = false;
    public bool objectsPlaceable = false;
    public bool wallPlaceable = false;
    public bool removeToolActivated = false;
    public bool doorToolActivated = false;

    private SpeechOut speechOut;
    public SoundManager soundManager;

    void Start()
    {
        speechOut = new SpeechOut();
        objectHandler = GameObject.FindObjectsOfType<ObjectHandler>()[0];
        soundManager = GameObject.FindObjectsOfType<SoundManager>()[0];
        Debug.Log(GameObject.FindObjectsOfType<SoundManager>().Length);

        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();

        _upperHandle.Rotate(0);
        _lowerHandle.Rotate(0);

        selectedObjectName = objectNames[selectedObjectId];

        Invoke("SetInitialHandleRotation", 0.5f);
    }

    void Update()
    {
        rotU = _upperHandle.GetRotation();
        rotL = _lowerHandle.GetRotation();

        if (objectsSelectable && !lowerTurned && Mathf.Abs(lowerZeroRotation-rotL) >= 30)
        {
            // feedback sound
            lowerTurned = true;
            if (lowerZeroRotation-rotL < 0) NextObject();
            if (lowerZeroRotation-rotL > 0) PrevObject();
        }

        if (!upperTurned && Mathf.Abs(upperZeroRotation-rotU) >= 30)
        {
            upperTurned = true;
            
            if (objectsPlaceable && selectedObjectId < objectNames.Length - 3) // object selected
            {
                objectHandler.placeObject(selectedObjectName);
            } else if (wallPlaceable && selectedObjectId == objectNames.Length - 3) // wall  selected
            {
                objectHandler.placeWall();
            } else if (removeToolActivated && selectedObjectId == objectNames.Length - 2) // remove tool selected
            {
                objectHandler.destroyHoveredObject();
            } else if (doorToolActivated && selectedObjectId == objectNames.Length - 1) // door tool selected
            {
                objectHandler.makeDoor();
            }
        }

        if (Mathf.Abs(lowerZeroRotation-rotL) <= 5)
        {
            lowerTurned = false;
        }

        if (Mathf.Abs(upperZeroRotation-rotU) <= 5)
        {
            upperTurned = false;
        }
    }

    public void SetInitialHandleRotation()
    {
        upperZeroRotation = _upperHandle.GetRotation();
        lowerZeroRotation = _lowerHandle.GetRotation();
    }

    private void NextObject()
    {
        if (++selectedObjectId >= objectNames.Length-3 + (wallPlaceable ? 1 : 0) + (removeToolActivated ? 1 : 0) + (doorToolActivated ? 1 : 0)) selectedObjectId = 0;

        Debug.Log("PLAY SOUND");
        soundManager.playSelectSound();

        selectedObjectName = objectNames[selectedObjectId];
        speechOut.Speak(selectedObjectName + " selected.");
    }

    private void PrevObject()
    {
        if (--selectedObjectId < 0) selectedObjectId = objectNames.Length-4 + (wallPlaceable ? 1 : 0) + (removeToolActivated ? 1 : 0) + (doorToolActivated ? 1 : 0);

        Debug.Log("PLAY SOUND");
        soundManager.playSelectSound();

        selectedObjectName = objectNames[selectedObjectId];
        speechOut.Speak(selectedObjectName + " selected.");
    }
}
