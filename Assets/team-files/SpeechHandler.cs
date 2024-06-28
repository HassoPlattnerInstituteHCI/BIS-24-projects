using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using SpeechIO;

public class SpeechHandler : MonoBehaviour
{
    SpeechIn speechIn;
    string[] commands = new string[] { "hello", "hallo", "hola" };
    Dictionary<string, KeyCode> dict = new Dictionary<string, KeyCode>();
    void Start()
    {
        speechIn = new SpeechIn(onRecognized, commands);
        Dialog();
    } 
    async void Dialog()
    {
        dict.Add("hello", KeyCode.Space);
        dict.Add("hallo", KeyCode.Space);
        dict.Add("hola", KeyCode.Space);
        await speechIn.Listen(dict);
    }
    void onRecognized(string message)
    {
        Debug.Log("[MyScript]: " + message);
        // await speechIn.Listen(dict);
    }
    public void OnApplicationQuit()
    {
        speechIn.StopListening(); // [macOS] do not delete this line!
    }
}