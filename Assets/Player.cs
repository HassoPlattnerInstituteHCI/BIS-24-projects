using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEngine;
using UnityEngine.Android;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip sliceSound;

    PantoHandle handle;

    private int slicedFruit = 0; 
    void Start()
    {
        handle = GameObject.Find("Panto").GetComponent<UpperHandle>();
    }

    public void resetScore() {
        slicedFruit = 0;
    }

    public int getScore() {
        return slicedFruit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "fruit") {
            audioSource.PlayOneShot(sliceSound); 
            //handle.Freeze();
            slicedFruit++;
        }
    }
}
