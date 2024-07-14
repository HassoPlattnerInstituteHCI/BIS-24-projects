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

    public Player player;

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

    void freeHandle() {
        handle.Free();
        handle.StopApplyingForce();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "fruit") {
            audioSource.PlayOneShot(sliceSound); 
            //handle.ApplyForce(col.gameObject.transform.position - gameObject.transform.position, 0.5f);
            //handle.Freeze();
            //Invoke("freeHandle", 0.5f);
            
            slicedFruit++;
        }
    }
}
