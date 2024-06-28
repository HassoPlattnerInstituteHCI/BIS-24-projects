using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using DualPantoToolkit;

public class Player : MonoBehaviour
{


    private Rigidbody rigidbody;
    PantoHandle handle;

    public bool isUpper = true;

    // Start is called before the first frame update
    void Start()
    {
        handle = isUpper
            ? (PantoHandle)GameObject.Find("Panto").GetComponent<UpperHandle>()
            : (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();
    }
    
    void Update() {
    }
}
