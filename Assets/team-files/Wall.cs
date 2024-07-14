using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Wand");
        if (col.gameObject.tag == "MeHandle")
        {
            GameObject.FindObjectsOfType<ObjectHandler>()[0].setHoveredObject(gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        Debug.Log("WANDVERLASSEN");
        if (col.gameObject.tag == "MeHandle")
        {
           GameObject.FindObjectsOfType<ObjectHandler>()[0].resetHoveredObject(gameObject);
        }
    }
}
