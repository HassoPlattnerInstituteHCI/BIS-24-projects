using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;

public class collider_me2 : MonoBehaviour
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
        if (other.GetComponent<Collider>().CompareTag("block1")) {
            _speechOut.Speak("Yay, you found the bed!");
            
        }
    }
}
