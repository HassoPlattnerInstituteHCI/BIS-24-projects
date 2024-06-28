using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using System.Threading.Tasks;




public class Dot : MonoBehaviour
{
    SpeechOut speechOut = new SpeechOut();
    bool collided = false;
    int sinceCollision = 0;
    
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
        if (collided) sinceCollision += 1;
        if (sinceCollision > 50) {
            Destroy(this);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LowerHandle") {
            speechOut.Speak("b");
            collided = true;
        }
    }

    void OnApplicationQuit()
    {
        speechOut.Stop();
    }

}
