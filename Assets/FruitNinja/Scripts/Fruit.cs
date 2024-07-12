using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;
using System.Diagnostics;

public class Fruit : MonoBehaviour
{
    SpeechOut speechOut = new SpeechOut();

    PantoHandle handle;
    public float angle = 0;
    private float angleStep = 10f;
    private float waitTime = 0.01f;
    private bool sliced = false;

    // Start is called before the first frame update
    void Start()
    {
        handle = GameObject.Find("Panto").GetComponent<LowerHandle>();
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
            StartCoroutine(RotateBackAndForth());

            //Destroy(gameObject);
            GameObject itHandleGodObject = GameObject.FindWithTag("ItHandle");
            if (itHandleGodObject != null)
            {
                //Destroy(itHandleGodObject);
            }

            if (!sliced)
            {
                sliced = true;
                await speechOut.Speak("you sliced the Fruit!");
            }
                 

    }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            GameObject itHandleGodObject = GameObject.FindWithTag("ItHandle");
            if (itHandleGodObject != null)
            {
                Destroy(itHandleGodObject);
            }
            if (!sliced)
            {
                await speechOut.Speak("you missed the Fruit!");
            }
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

    IEnumerator RotateBackAndForth()
    {
        float angle = 0f;

        for (int j = 0; j < 3; j++)
        {
            // Drehungen in eine Richtung
            for (int i = 0; i < 10; i++)
            {
                handle.Rotate(angle);
                angle += angleStep;
                yield return new WaitForSeconds(waitTime);
            }

            // Drehungen in die andere Richtung
            for (int i = 0; i < 10; i++)
            {
                handle.Rotate(angle);
                angle -= angleStep;
                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}
