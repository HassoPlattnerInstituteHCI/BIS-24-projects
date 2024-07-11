using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementDetector : MonoBehaviour
{
    private Vector3 lastPosition;
    public bool IsMoving { get; private set; }
    public GameObject boundsArea;
    private Collider boundsCollider;


    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        IsMoving = false;

        if (boundsArea != null)
        {
            boundsCollider = boundsArea.GetComponent<Collider>();
            if (boundsCollider == null)
            {
                Debug.LogError("No Collider component found on boundsArea GameObject.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != lastPosition && IsWithinBounds(transform.position))
        {
            IsMoving = true;
            lastPosition = transform.position;
        }
        else
        {
            IsMoving = false;
        }
    }

    bool IsWithinBounds(Vector3 position)
    {
        if (boundsCollider != null)
        {
            return boundsCollider.bounds.Contains(position);
        }
        return false;
    }
}
