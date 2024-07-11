using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionReporter : MonoBehaviour
{
    public GameObject propertyHandlerObject;
    public GameObject trigger;

    private Collider collider;
    private PropertyHandler propertyHandler;
    // Start is called before the first frame update
    void Start()
    {
        if (propertyHandlerObject == null)
        {
            Debug.LogError("No Propertyhandler set");
        } else
        {
            propertyHandler = propertyHandlerObject.GetComponent<PropertyHandler>();

            if (propertyHandler == null ) {
                Debug.LogError("No property handler found on selected handler object.");
            }
        }
        
        collider = GetComponent<Collider>();

        if (collider == null)
        {
            Debug.LogError("Forgot to add a collider??");
        }
    }

    // Update is called once per frame
    void Update()
    {
        propertyHandler.directionSelectorActive = IsWithinBounds(trigger.transform.position);
    }

    bool IsWithinBounds(Vector3 position)
    {
        Boolean isValid = collider.bounds.Contains(position);
        if (propertyHandler.pathCompleted && isValid)
        {
            propertyHandler.selectionWasActive = true;
        }

        if (!propertyHandler.pathCompleted ) { propertyHandler.selectionWasActive = false; }

        return isValid;
    }
}
