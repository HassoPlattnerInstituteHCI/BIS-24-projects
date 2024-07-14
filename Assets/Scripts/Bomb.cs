using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class Bomb : MonoBehaviour
{
    private float speed_z;  
    private float speed_x;
    private bool moving;
    private GameManager gameManager;
    private SoundEffects soundeffects;

    // Start is called before the first frame update
void Start()
    {
        gameManager = GameObject.FindWithTag("Panto").GetComponent<GameManager>();

        soundeffects = GameObject.FindWithTag("Panto").GetComponent<SoundEffects>();

        speed_z = gameManager.speed;

        speed_x = UnityEngine.Random.Range(0.5f * speed_z, speed_z);

        if(UnityEngine.Random.Range(0, 2) == 0) {
            speed_x *= -1;
        }

        moving = gameManager.lvl_moving;


    }
    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < -15.16) {
            soundeffects.PlayPositiveBombClip();
            gameManager.IncreaseBombCounter();
            Destroy(this.gameObject);

        }
    }

    void FixedUpdate() {

        transform.position += new Vector3(0,0,speed_z * Time.fixedDeltaTime);

        if (moving) {
            transform.position += new Vector3(speed_x * Time.fixedDeltaTime, 0,0);
        }
    }

    void OnTriggerEnter(Collider other) {

        Debug.Log("Collision detected.");

        if (other.CompareTag("Wall")) {
            Debug.Log("Collision with wall.");
            speed_x *= -1;
            soundeffects.PlayWallClip();
        }
    }




}
