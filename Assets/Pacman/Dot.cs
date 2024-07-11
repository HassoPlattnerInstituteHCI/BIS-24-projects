using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using System.Threading.Tasks;
using DualPantoToolkit;



public class Dot : MonoBehaviour
{
    SpeechOut speechOut = new SpeechOut();
    public bool collided = false;
    public DotManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LowerHandle") {
            collided = true;
            manager.DotEaten();
            Destroy(this.gameObject);
        }
    }

    void OnApplicationQuit()
    {
        speechOut.Stop();
    }

}
