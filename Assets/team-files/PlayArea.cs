using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    public GameObject levelManagerObject;

    private bool upperHasEntered = false;
    private bool lowerHasEntered = false;

    void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.tag == "MeHandle")
        {
            if (lowerHasEntered) 
            {
                levelManagerObject.GetComponent<LevelManager>().StartNextLevel();
                transform.position = new Vector3(0,0,10);
                Invoke("resetHandleStates", 5);
            }

            upperHasEntered = true;
        }
        
        if (col.gameObject.tag == "ItHandle")
        {
            if (upperHasEntered) 
            {
                levelManagerObject.GetComponent<LevelManager>().StartNextLevel();
                transform.position = new Vector3(0,0,10);
                Invoke("resetHandleStates", 5);
            }

            lowerHasEntered = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "MeHandle") upperHasEntered = false;

        if (col.gameObject.tag == "ItHandle") lowerHasEntered = false;
        
    }

    void resetHandleStates()
    {
        upperHasEntered = false;
        lowerHasEntered = false;
    }
}
