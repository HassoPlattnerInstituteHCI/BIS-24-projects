using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class Coin : MonoBehaviour
{

    private float speed = 1f;

    private SpeechOut speachOut;

    // Start is called before the first frame update
    void Start()
    {
        speachOut = new SpeechOut();
        speachOut.Speak("New coin");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,0,-speed * Time.deltaTime);

        if(transform.position.z < -15.16) {
            // play sound
            speachOut.Speak("dudumm");

            Destroy(this);

        }
    }



}
