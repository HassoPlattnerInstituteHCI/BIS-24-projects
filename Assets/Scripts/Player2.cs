using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class Player2 : MonoBehaviour
{

    public AudioSource cling;
    private SpeechOut speachOut;

    // Start is called before the first frame update
    void Start()
    {
        speachOut = new SpeechOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other){
    Debug.Log("Collion with Exit");
        if(other.CompareTag("Exit")) {
            Debug.Log("Collion with Exit");
            cling.Play();
            speachOut.Speak("Level Complete");
            Destroy(other.gameObject);
            
        } 

    }
        
}
