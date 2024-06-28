using UnityEngine;
using SpeechIO;
using DualPantoToolkit;

public class JJManager : MonoBehaviour
{
    SpeechOut speech;
    PantoHandle upperHandle;
    PantoHandle lowerHandle;
    PantoCollider[] pantoColliders;
    GameObject panto; 
    async void Start()
    {
        speech = new SpeechOut();
        panto =  GameObject.Find("Panto");
        upperHandle = panto.GetComponent<UpperHandle>();
        lowerHandle = panto.GetComponent<LowerHandle>();
        Invoke("createObstacles", 1.0f);

        // GameObject[] startObjects = GameObject.FindGameObjectsWithTag("StartPosition");
        // await upperHandle.SwitchTo(startObjects[0], 10f);
        upperHandle.Free();

        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag("TargetPosition");
        await lowerHandle.SwitchTo(targetObjects[0], 10f);
        Debug.Log("Finished starting");
        await speech.Speak("Geh zu Haus K", 1.0f, SpeechBase.LANGUAGE.GERMAN);
    }
    void Update() {
        
    }
    void OnApplicationQuit()
    {
        speech.Stop();
    }
    private void createObstacles()
    {
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.CreateObstacle();
            collider.Enable();
        }
    }

}
