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
    public float rot;
    public string selectedObjectName = "wall";
    public int selectedObjectId = 0;
    private string[] objectNames = {"wall", "object1", "object2"};
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    private float _initRotation;
    private float degreesPerObject;
    public float lowestRotation = 0;
    public float highestRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();

        _initRotation = _upperHandle.GetRotation();

        degreesPerObject = 360/objectNames.Length;
    }

    // Update is called once per frame
    void Update()
    {
        rot = _upperHandle.GetRotation();
        //Debug.Log(rot);

        if (rot < lowestRotation) {
            lowestRotation = rot;
            highestRotation = rot+360;
        } else if (rot > highestRotation) {
            highestRotation = rot;
            lowestRotation = rot-360;
        }

        //Debug.Log(Math.Floor(rotation/(360/objectNames.Length)));

        int lastSelectedObjectId = selectedObjectId;

        selectedObjectId = (int)Math.Floor((rot-lowestRotation)/degreesPerObject);
        //Debug.Log(selectedObjectId);
        //Debug.Log(lowestRotation+degreesPerObject*(0.5+selectedObjectId));
        //Debug.Log(rot);

        // if (lastSelectedObjectId != selectedObjectId) {
            //Debug.Log((selectedObjectId)*degreesPerObject+lowestRotation);
            //_upperHandle.Rotate(lowestRotation+degreesPerObject*(0.5f+selectedObjectId));
            //_upperHandle.Rotate((selectedObjectId+0.5f)*degreesPerObject+lowestRotation);
        // }

        //Debug.Log(degreesPerObject/2+degreesPerObject*selectedObjectId);

        // if (Input.GetKeyDown(KeyCode.UpArrow)) {
        //     Debug.Log(rot);
        //     Debug.Log(rot-(lowestRotation+degreesPerObject*(0.5f+selectedObjectId)));
        // }

        // if (Input.GetKeyDown(KeyCode.DownArrow)) {
        //     rotate_to_here = lowestRotation+degreesPerObject*(0.5f+selectedObjectId);
        //     Debug.Log(rotate_to_here);
        //     _upperHandle.Rotate(rotate_to_here);
        // }
        
    }
}
