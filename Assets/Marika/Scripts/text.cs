using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;


public class text : MonoBehaviour
{
    // Start is called before the first frame update

    string inhalt = "Beispiel";
    public SpeechOut speechOut = new SpeechOut();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void readOut(){
        speechOut.Speak("Text that reads:" + inhalt);

    }
}
