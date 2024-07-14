using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetArea : MonoBehaviour
{
    private bool upperHasEntered = false;
    private bool lowerHasEntered = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "MeHandle")
        {
            upperHasEntered = true;
            if (lowerHasEntered) GameObject.FindObjectsOfType<LevelManager>()[0].reset();
        }

        if (col.gameObject.tag == "ItHandle")
        {
            lowerHasEntered = true;
            if (upperHasEntered) GameObject.FindObjectsOfType<LevelManager>()[0].reset();   
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "MeHandle") upperHasEntered = false;

        if (col.gameObject.tag == "ItHandle") lowerHasEntered = false;
    }
}
