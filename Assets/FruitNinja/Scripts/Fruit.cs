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
        if (other.CompareTag("MeHandle"))
        {
            Destroy(gameObject);
            GameObject itHandleGodObject = GameObject.FindWithTag("ItHandle");
            if (itHandleGodObject != null)
            {
                Destroy(itHandleGodObject);
            }
            await speechOut.Speak("you sliced the Fruit!");
            
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            GameObject itHandleGodObject = GameObject.FindWithTag("ItHandle");
            if (itHandleGodObject != null)
            {
                Destroy(itHandleGodObject);
            }
            await speechOut.Speak("you missed the Fruit!");
        }
        else if (other.CompareTag("ItHandle"))
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>());
        }
        else if (other.CompareTag("MeHandle"))
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>());
        }
    }
}
