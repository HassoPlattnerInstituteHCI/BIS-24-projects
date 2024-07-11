using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointGenerator : MonoBehaviour
{
    public GameObject monitoredHandle;

    public Vector3[] controlPointsUp;
    public Vector3[] controlPointsRight;
    public Vector3[] controlPointsDown;
    public Vector3[] controlPointsLeft;

    private Vector3[] basePoints;

    public GameObject triggerObject;

    // Start is called before the first frame update
    void Start()
    {

        if (triggerObject == null )
        {
            Debug.LogError("No trigger area for compass found");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    
}
