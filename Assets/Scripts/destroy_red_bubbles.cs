using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;


public class destroy_red_bubbles : MonoBehaviour
{   
    SpeechOut _speechOut;
    // Start is called before the first frame update
    public int red_bubbles = 2;
    void Start()
    {   
        _speechOut = new SpeechOut();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    async void OnTriggerEnter(Collider other){
        if (other.tag == "red") {
            await _speechOut.Speak("red");
            GameObject.Destroy(other.gameObject);
            red_bubbles--;
    }

}
}
