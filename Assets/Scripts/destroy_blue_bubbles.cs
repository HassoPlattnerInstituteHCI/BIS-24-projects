using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;


public class destroy_blue_bubbles : MonoBehaviour
{   
    SpeechOut _speechOut;
    // Start is called before the first frame update
    public int blue_bubbles = 3;
    void Start()
    {   
        _speechOut = new SpeechOut();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    async void OnTriggerEnter(Collider other){
        if (other.tag == "blue") {
            await _speechOut.Speak("blue");
            GameObject.Destroy(other.gameObject);
            blue_bubbles--;
    }

}
}