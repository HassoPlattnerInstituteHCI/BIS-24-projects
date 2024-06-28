using UnityEngine;
using SpeechIO;
using DualPantoToolkit;

public class PacmanIntroductionManager : MonoBehaviour
{
    SpeechOut speech;
    PantoHandle lowerHandle;
    GameObject start;
    async void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        start = GameObject.FindGameObjectsWithTag("MapsStart")[0];
        speech = new SpeechOut();
        Level_1_Pacman level = GameObject.Find("Panto").GetComponent<Level_1_Pacman>();
        await level.PlayIntroduction();
        await speech.Speak("Introduction finished, moving to start");
        await lowerHandle.MoveToPosition(start.transform.position, 1, true);
    }

    void OnApplicationQuit()
    {
        speech.Stop();
    }

}
