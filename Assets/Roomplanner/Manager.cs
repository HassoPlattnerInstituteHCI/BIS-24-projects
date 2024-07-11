using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using SpeechIO;
using DualPantoToolkit;

public class Manager : MonoBehaviour
{
    SpeechIn speechIn;
    string[] pickUp = new string[] { "select", "pick up", "choose" };
    Dictionary<string, KeyCode> dict = new Dictionary<string, KeyCode>();
    void Start()
    {
        speechIn = new SpeechIn(onRecognized);
        dict.Add("select", KeyCode.A);
        dict.Add("drop", KeyCode.S);
        dict.Add("delete", KeyCode.D);
        dict.Add("choose", KeyCode.Space);
        Hoere();
    }

    void Update()
    {
    }

    async void Hoere() {
        await speechIn.Listen(dict);
        Hoere();
    }

    void onRecognized(string message)
    {
        switch(message) {
            case "select":
                switchMeObject();
                break;
            case "drop":
                dropSelected();
                break;
            case "delete":
                deleteObject();
                break;
            default:
                break;
        }
    }

    void switchMeObject() {
        MeHandle mho = (MeHandle) GameObject.FindObjectOfType<MeHandle>();
        Vector3 mhov = mho.GetComponent<Transform>().position;

        GameObject[] ios = GameObject.FindGameObjectsWithTag("Interactive");
        float minDist = float.PositiveInfinity;
        GameObject minDistObject = null;

        foreach (GameObject io in ios) {
            if (io.GetComponent<InteractiveObject>().doFollow == true) {
                io.GetComponent<InteractiveObject>().doFollow = false;
                continue;
            }
            float dist = (io.GetComponent<Transform>().position - mhov).magnitude;
            if (dist < minDist) {
                minDist = dist;
                minDistObject = io;
            }
        }
        // TODO: minDIst

        // PantoHandle upperHandle = (PantoHandle) GameObject.Find("Panto").GetComponent<UpperHandle>();
        InteractiveObject io2 = minDistObject.GetComponent<InteractiveObject>();
        io2.doFollow = true;
        io2.GotPickedUp();
    }

    void dropSelected() {
        GameObject[] ios = GameObject.FindGameObjectsWithTag("Interactive");
        foreach (GameObject io in ios) {
            if (io.GetComponent<InteractiveObject>().doFollow == true) {
                io.GetComponent<InteractiveObject>().doFollow = false;
                break;
            }
        }
    }

    void deleteObject() {
        MeHandle mho = (MeHandle) GameObject.FindObjectOfType<MeHandle>();
        Vector3 mhov = mho.GetComponent<Transform>().position;

        GameObject[] ios = GameObject.FindGameObjectsWithTag("Interactive");

        foreach (GameObject io in ios) {
            if (io.GetComponent<InteractiveObject>().doFollow) {
                io.GetComponent<InteractiveObject>().GotDeleted();
                Destroy(io);
                continue;
            }
        }
    }

    public void OnApplicationQuit()
    {
        speechIn.StopListening(); // [macOS] do not delete this line!
    }
}
