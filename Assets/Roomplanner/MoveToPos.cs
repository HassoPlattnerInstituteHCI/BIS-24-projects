using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;


public class MoveToPos : MonoBehaviour
{
    public int speed;
    public Transform meStart;
    public Transform itStart;

    PantoHandle upperHandle;
    PantoHandle lowerHandle;

    bool shouldFreeHandle = true;
    // Start is called before the first frame update
    void Start()
    {
        upperHandle = (PantoHandle) GameObject.Find("Panto").GetComponent<UpperHandle>();
        lowerHandle = (PantoHandle) GameObject.Find("Panto").GetComponent<LowerHandle>();

        lowerHandle.MoveToPosition(meStart.position, speed, shouldFreeHandle);
        lowerHandle.MoveToPosition(itStart.position, speed, shouldFreeHandle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
