
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter(Collision collision){
        if(!collision.gameObject.CompareTag("Ball")){
            return;
        }
        Destroy(gameObject);
    }
}
