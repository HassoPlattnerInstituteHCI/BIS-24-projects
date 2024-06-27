using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks; //note that you need to include this if you want to use Task.Delay.
using SpeechIO;

public class speech : MonoBehaviour
{
    // Start is called before the first frame update
    SpeechOut speechOut;
    void Start()
    {
        speechOut = new SpeechOut();
        Dialog();
    }

    public async void Dialog()
    {
        await speechOut.Speak("Use the me handle to move around the canvas and turn it to place a pixel.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnApplicationQuit()
    {
        speechOut.Stop(); //Windows: do not remove this line.
    }

}