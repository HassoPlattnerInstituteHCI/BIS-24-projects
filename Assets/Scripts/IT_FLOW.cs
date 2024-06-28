using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;
using System.Threading.Tasks;

public class IT_FLOW : MonoBehaviour {

    SpeechOut so;
    string currentSelect = "NO";
    Collider otherObject;
    PantoHandle lowerHandle;
    PantoHandle upperHandle;
    private float lastRotation;
    private float timer = 0f;
    private float interval = 0.2f;

    void Start() {
        so = new SpeechOut();
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        lastRotation = lowerHandle.GetRotation();
    }
    private void Update()
    {
        Debug.Log(lowerHandle.GetRotation());
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            if (rotatedHandle())
            {
                SelectObject(currentSelect);
            }
            timer -= interval;
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Square"))
        {
            Debug.Log("Seen: Square!");
            so.Speak("Square");
            currentSelect = "Square";
            otherObject = other;
        }
        else if (other.CompareTag("Circle"))
        {
            Debug.Log("Seen: Circle!");
            so.Speak("Circle");
            currentSelect = "Circle";
            otherObject = other;
        }
        else if (other.CompareTag("Rhombus"))
        {
            Debug.Log("Seen: Rhombus!");
            so.Speak("Rhombus");
            currentSelect = "Rhombus";
            otherObject = other;
        }
        else if (other.CompareTag("Rectangle"))
        {
            Debug.Log("Seen: Rectangle!");
            so.Speak("Rectangle");
            currentSelect = "Rectangle";
            otherObject = other;
        }
    }
    
    bool rotatedHandle()
    {
        float currentRotation = lowerHandle.GetRotation();
        if (Math.Abs(currentRotation - lastRotation) > 60)
        {
            lastRotation = currentRotation;
            return true;
        }
        lastRotation = currentRotation;
        return false;
    }
    void SelectObject(string Tag)
    {
        if (Tag == "NO") return;
        Vector3 currentPosition = upperHandle.HandlePosition(transform.position);
        Instantiate(otherObject.gameObject, currentPosition, Quaternion.identity);
        so.Speak("Selected " + Tag);
    }
}