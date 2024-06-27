using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;
using System.Diagnostics;

public class Fruit : MonoBehaviour
{
    SpeechOut speechOut = new SpeechOut();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private async void OnMouseDown()
    //{
    //    Destroy(gameObject);
    //    await speechOut.Speak("Fruit destroyed");
    //}

    async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knife"))
        {
            // UnityEngine.Debug.Log("Fruit collided with Knife and will be destroyed");
            Destroy(gameObject);
            await speechOut.Speak("you sliced the Fruit!");
        }
    }
}
