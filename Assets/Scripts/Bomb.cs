using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class Bomb : MonoBehaviour
{
    private float speed = 1f;
    private bool moving = false;

    private SpeechOut speachOut;
    private GameManager gameManager;

    // public Coin(bool _moving) {
    //     moving = _moving;
    // }

    // Start is called before the first frame update
    void Start()
    {
        speachOut = new SpeechOut();
        speachOut.Speak("New bomb");
        gameManager = GameObject.FindWithTag("Panto").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,0,-speed * Time.deltaTime);

        if(transform.position.z < -15.16) {
            // play sound
            speachOut.Speak("yeah");
            gameManager.IncreaseBombCounter();

            Destroy(this.gameObject);

        }
    }

}
