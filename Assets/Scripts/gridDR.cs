using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridDR : MonoBehaviour
{
    public int state = 0;
    //private gameObject g;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject.renderer.material.color = Color.blue;
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.RightArrow) && state == 2) || (Input.GetKeyDown(KeyCode.DownArrow) && state == 1)){
            gameObject.GetComponent<Renderer> ().material.color = Color.green;
            state = 3;
        } else if(state == 3 && Input.GetKeyDown(KeyCode.LeftArrow)){
            gameObject.GetComponent<Renderer> ().material.color = Color.white;
            state = 2;
        } else if(state == 3 && Input.GetKeyDown(KeyCode.UpArrow)){
            gameObject.GetComponent<Renderer> ().material.color = Color.white;
            state = 1;
        } else if(state == 2 && Input.GetKeyDown(KeyCode.UpArrow)){
            state = 0;
        } else if(state == 0 && Input.GetKeyDown(KeyCode.DownArrow)){
            state = 2;
        } else if(state == 1 && Input.GetKeyDown(KeyCode.LeftArrow)){
            state = 0;
        } else if(state == 0 && Input.GetKeyDown(KeyCode.RightArrow)){
            state = 1;
        }
    }
}
