using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    public GameObject panto;
    
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    
    // Start is called before the first frame update
    void Start()
    {
        _upperHandle = panto.GetComponent<UpperHandle>();
        _lowerHandle = panto.GetComponent<LowerHandle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
