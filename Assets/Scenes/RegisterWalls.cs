using System.Threading.Tasks;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;

public class RegisterWalls : MonoBehaviour
{
    PantoHandle meHandle;
    private SpeechOut speechOut;

    private void Awake()
    {
        speechOut = new SpeechOut();
    }

    async void Start()
    {
        await speechOut.Speak("Welcome to PantoPutt");
        await speechOut.Speak("This is the ball.");
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();

        await speechOut.Speak("This is your golf club.");
        await meHandle.MoveToPosition(new Vector3(-2, 0, -6), 2f);
        await speechOut.Speak("Find the whole and push the ball into the whole. Do it");
        PantoCollider[] pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.CreateObstacle();
            collider.Enable();
        }

    }
}
