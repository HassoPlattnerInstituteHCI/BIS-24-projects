using UnityEngine;
using SpeechIO;
using DualPantoToolkit;

public class MapsIntroductionManager : MonoBehaviour
{
    SpeechOut speech;
    PantoHandle lowerHandle;
    GameObject start;
    async void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        start = GameObject.FindGameObjectsWithTag("MapsStart")[0];
        speech = new SpeechOut();
        Level_1 level = GameObject.Find("Panto").GetComponent<Level_1>();
        await level.PlayIntroduction();
        await speech.Speak("Introduction finished, moving to start");
        // await lowerHandle.MoveToPosition(start.transform.position, 0.1, true);
    }

    void OnApplicationQuit()
    {
        speech.Stop();
    }

}
