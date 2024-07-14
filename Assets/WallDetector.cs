using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;
using System.Threading.Tasks;

public class WallDetector : MonoBehaviour
{
    SpeechOut speechOut;
    bool free = true;
    PantoHandle upperHandle;

    // Start is called before the first frame update
    void Start()
    {
        speechOut = new SpeechOut();
        speechOut.SetLanguage(SpeechBase.LANGUAGE.ENGLISH); // alternatively GERMAN or JAPANESE
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 currentPos = upperHandle.HandlePosition(transform.position);
        currentPos.y = -6.5f;
        transform.position = currentPos;
        transform.eulerAngles = new Vector3(0, upperHandle.GetRotation(), 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PantoCollider>() != null && !collision.gameObject.GetComponent<PantoCollider>().IsEnabled())
        {
            collision.gameObject.GetComponent<PantoCollider>().Enable();
        }
    }

    async void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.GetComponent<PantoCollider>() != null)
        {
            await Task.Delay(100);
            if (Physics.CheckSphere(collision.gameObject.transform.position, 1.5f))
            {
                collision.gameObject.GetComponent<PantoCollider>().Disable();
            }
        }
    }
}
