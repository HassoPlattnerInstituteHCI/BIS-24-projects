using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;

public class collider_me : MonoBehaviour
{   
    PantoHandle upperHandle;
    SpeechOut _speechOut;
    // Start is called before the first frame update
    void Start()
    {   
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        _speechOut = new SpeechOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if (other.GetComponent<Collider>().CompareTag("blue")) {
            _speechOut.Speak("blue");
            
        }else if (other.GetComponent<Collider>().CompareTag("red")) {
            _speechOut.Speak("red");
            
        }
    }

}
