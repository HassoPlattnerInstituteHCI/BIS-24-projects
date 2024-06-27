using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    private int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(state==0 && Input.GetKeyDown(KeyCode.Space)){
            gameObject.GetComponent<Renderer> ().material.color = Color.white;
            state = 1;
        } else if(state==1 && Input.GetKeyDown(KeyCode.Space)){
            gameObject.GetComponent<Renderer> ().material.color = Color.black;
            state = 0;
        }
    }
}
