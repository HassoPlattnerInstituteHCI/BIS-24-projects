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
    public bool ready;
    public string startMessage;
    async void Start()
    {
        ready = false;
        speech = new SpeechOut();
        panto =  GameObject.Find("Panto");
        upperHandle = panto.GetComponent<UpperHandle>();
        lowerHandle = panto.GetComponent<LowerHandle>();

        GameObject[] startObjects = GameObject.FindGameObjectsWithTag("StartPosition");
        await upperHandle.SwitchTo(startObjects[0], 4.0f);
        upperHandle.Free();

        ready = true;
        Invoke("createObstacles", 1.0f);

        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag("TargetPosition");
        await lowerHandle.SwitchTo(targetObjects[0], 4.0f);
        //Invoke("Gefrieren", 1.0f);
        await speech.Speak(startMessage, 1.0f, SpeechBase.LANGUAGE.GERMAN);
    }
    void Gefrieren() {
        lowerHandle.Freeze();
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
            Debug.Log("ccollider");
            //Debug.Log(collider.name);
            collider.CreateObstacle();
            collider.Enable();
        }
    }
    public void disableObstacles() {
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.Disable();
        }
    }

}
