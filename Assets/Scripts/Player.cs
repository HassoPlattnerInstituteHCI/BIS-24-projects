using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;


public class Player : MonoBehaviour
{

    private GameManager gameManager;
    private SoundEffects soundeffects;

    private SpeechOut _speechOut = new SpeechOut();

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("Panto").GetComponent<GameManager>();
        soundeffects = GameObject.FindWithTag("Panto").GetComponent<SoundEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other){

        if(other.CompareTag("Coin")) {
            Debug.Log("Collison with coin");
            soundeffects.PlayPositiveCoinClip();
            gameManager.IncreaseCoinCounter();

            Destroy(other.gameObject);

        } 
        else if(other.CompareTag("Bomb")) {
            Debug.Log("Collision with bomb");
            soundeffects.PlayNegativeBombClip();
            Destroy(other.gameObject);
        }

    }
        
}
