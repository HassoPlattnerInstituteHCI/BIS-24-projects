using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementDetector : MonoBehaviour
{

    public GameObject propertyHandlerObject;
    public GameObject trigger;

    private Collider collider;
    private Vector3 lastPosition;
    private PropertyHandler propertyHandler;


    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        if (collider == null)
        {
            Debug.LogError("No collider found!");
        }

        propertyHandler = propertyHandlerObject.GetComponent<PropertyHandler>();
        if (collider == null)
        {
            Debug.LogError("No propertyHandler found!");
        }

        if (!trigger)
        {
            Debug.LogError("No trigger object set");
        }


        lastPosition = trigger.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        propertyHandler.caldronActionActive = trigger.transform.position != lastPosition && IsWithinBounds(trigger.transform.position);
        lastPosition = trigger.transform.position;

        if (propertyHandler.pathCompleted && propertyHandler.directionSelected != -1 && propertyHandler.selectionWasActive) { propertyHandlerObject.GetComponent<BezierCurveBuilder>().TranslateRotation(); }
    }

    bool IsWithinBounds(Vector3 position)
    {
        return collider.bounds.Contains(position);
    }
}
