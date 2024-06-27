using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;
using System.Threading.Tasks;

public class IT_FLOW : MonoBehaviour {

    SpeechOut so;
    SpeechIn si;

    void Start() {
        so = new SpeechOut();
        si = new SpeechIn(null);
    }
    private void Update()
    {
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Square"))
        {
            Debug.Log("Seen: Square!");
            so.Speak("Square");
            SelectObject().ContinueWith(result => PlaceObject(result.Result, "Square"));
        }
        else if (other.CompareTag("Circle"))
        {
            Debug.Log("Seen: Circle!");
            so.Speak("Circle");
            SelectObject().ContinueWith(result => PlaceObject(result.Result, "Circle"));
        }
        else if (other.CompareTag("Rhombus"))
        {
            Debug.Log("Seen: Rhombus!");
            so.Speak("Rhombus");
            SelectObject().ContinueWith(result => PlaceObject(result.Result, "Rhombus"));
        }
        else if (other.CompareTag("Rectangle"))
        {
            Debug.Log("Seen: Rectangle!");
            so.Speak("Rectangle");
            SelectObject().ContinueWith(result => PlaceObject(result.Result, "Rectangle"));
        }
    }
    async Task<bool> SelectObject()
    {
        so.Speak("SelectObject called");
        //si.Listen(new Dictionary<string, KeyCode>() { { "yes", KeyCode.Y }, { "no", KeyCode.N } });
        string decision = await si.Listen(new string[] { "yes", "no" });
        so.Speak("There is a Decision");
        if (decision == "no") { so.Speak("Return Value False"); return false; }
        so.Speak("Return Value True");
        return true;
    }
    void PlaceObject(bool decision, string Tag)
    {
        if (!decision) return;
        if (Tag == "Sqaure")
        {
            so.Speak("Place Sqaure");
        }
        if (Tag == "Circle")
        {
            so.Speak("Place Circle");
        }
        if (Tag == "Rhombus")
        {
            so.Speak("Place Rhombus");
        }
        if (Tag == "Rectangle")
        {
            so.Speak("Place Rectangle");
        }
    }
}