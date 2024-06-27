using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    float x,y,z,w = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.rotation = new Quaternion(x,y,z,w);
    }

    void FixedUpdate(){
    }
}
