using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDoneDetector : MonoBehaviour
{
    public GameObject propertyHandlerObject;
    public GameObject trigger;
    public string potionName;

    private Collider collider;
    private PropertyHandler propertyHandler;
    // Start is called before the first frame update
    void Start()
    {
        if (propertyHandlerObject == null)
        {
            Debug.LogError("No Propertyhandler set");
        }
        else
        {
            propertyHandler = propertyHandlerObject.GetComponent<PropertyHandler>();

            if (propertyHandler == null)
            {
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
        if (IsWithinBounds(trigger.transform.position))
        {
            propertyHandler.madePotion = potionname
        }

    }

    bool IsWithinBounds(Vector3 position)
    {
        Boolean isValid = collider.bounds.Contains(position);

        return isValid;
    }
}
