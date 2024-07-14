using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private bool finished = false;

    void OnTriggerEnter(Collider col)
    {
        if (!finished && col.gameObject.tag == "ItHandle")
        {
            finished = true;
            GameObject.FindObjectsOfType<Level5>()[0].levelFinished();
        }
    }
}
