using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDoneDetector : MonoBehaviour
{
    public GameObject propertyHandlerObject;
    public GameObject trigger;
    public GameObject handle;
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
            propertyHandler.madePotion = potionName;
        }

        if (IsWithinBounds(handle.transform.position))
        {
            if (potionName == "AIR")
            {
                propertyHandler.firstSlotTriggered = true;
                propertyHandler.Say("SWOOOSH");
                propertyHandler.soundLocked = true;
                Invoke("UnlockSound", 3);
                return;
            }

            if (potionName == "WATER")
            {
                propertyHandler.Say("Splash");
                propertyHandler.soundLocked = true;
                Invoke("UnlockSound", 3);
                return;
            }

            if (potionName == "FIRE")
            {
                propertyHandler.Say("HOT HOT HOT");
                propertyHandler.soundLocked = true;
                Invoke("UnlockSound", 3);
                return;
            }

            if (potionName == "EARTH")
            {
                propertyHandler.Say("Knirsch");
                propertyHandler.soundLocked = true;
                Invoke("UnlockSound", 3);
                return;
            }
        }

    }

    bool IsWithinBounds(Vector3 position)
    {
        Boolean isValid = collider.bounds.Contains(position);

        propertyHandler.soundLocked = true;
        Invoke("UnlockSound", 3);

        return isValid;
    }

    void UnlockSound()
    {
        propertyHandler.soundLocked = false;
    }
}
