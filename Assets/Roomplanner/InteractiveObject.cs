using UnityEngine;
using DualPantoToolkit;
using SpeechIO;


public class InteractiveObject : MonoBehaviour
{
    public bool doFollow;
    public string name;
    PantoHandle upperHandle;
    SpeechOut speechOut;

    void Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        speechOut = new SpeechOut();
    }

    void FixedUpdate()
    {
        if (doFollow) {
            transform.position = (upperHandle.HandlePosition(transform.position));
            transform.eulerAngles = new Vector3(0, upperHandle.GetRotation(), 0);
        }
    }

    public async void GotPickedUp() {
        speechOut.Stop();
        await speechOut.Speak("picked up" + name);
    }

    public async void GotDeleted() {
        speechOut.Stop();
        await speechOut.Speak("deleted" + name);
    }
}