using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class Player : MonoBehaviour
{
    public Transform targetPos;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position,targetPos.position) < 0.1)
        {
            SpeechOut speechOut = new SpeechOut();
            speechOut.Speak("Great you finished your first shape");
            Debug.Break();
        }
    }
}
