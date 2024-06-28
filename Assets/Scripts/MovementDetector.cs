using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDetector : MonoBehaviour
{
    private Vector3 lastPosition;
    public bool IsMoving { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        IsMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != lastPosition)
        {
            IsMoving = true;
            lastPosition = transform.position;
        }
        else
        {
            IsMoving = false;
        }
    }
}
