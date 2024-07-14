using System.Threading.Tasks;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;

public class RegisterWallNoBall : MonoBehaviour
{
    PantoHandle meHandle;
    private SpeechOut speechOut;

    private void Awake()
    {
        speechOut = new SpeechOut();
    }

    async void Start()
    {
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        await speechOut.Speak("Welcome to Panto Putt.");
        await Task.Delay(3000);
        await speechOut.Speak("This is your golf club.");
        await meHandle.MoveToPosition(new Vector3(2, 0, -6), 2f);
        await speechOut.Speak("For the first level there is no ball. Just find the whole.");
        PantoCollider[] pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.CreateObstacle();
            collider.Enable();
        }

    }
}
