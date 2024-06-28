using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public AudioSource cling;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other){

        if(other.CompareTag("Coin")) {
            Debug.Log("Collison with coin");
            cling.Play();
            Destroy(other.gameObject);

        } 
        else if(other.CompareTag("FinishWall")) {
            ;
        }

    }
        
}
