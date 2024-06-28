using UnityEngine;
using DualPantoToolkit;
using System;
using SpeechIO;

public class Object_Follow : MonoBehaviour
{
    SpeechOut so;
    PantoHandle upperHandle;
    bool free = true;
    bool new_Object = false;
    private float lastRotation;
    private float timer = 0f;
    private float interval = 0.2f;
    void Start()
    {
        so = new SpeechOut();
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        if (transform.position.y > -4) new_Object = true;
        lastRotation = upperHandle.GetRotation();
    }

    void FixedUpdate()
    {
        if (new_Object) transform.position = upperHandle.HandlePosition(transform.position);
    }

    void Update()
    {
        if (new_Object) timer += Time.deltaTime;
        if (timer >= interval)
        {
            if (rotatedHandle())
            {
                new_Object = false;
                so.Speak("Placed Object");

            }
            timer -= interval;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (free)
            {
                upperHandle.Freeze();
            }
            else
            {
                upperHandle.Free();
            }
            free = !free;
        }
    }
    bool rotatedHandle()
    {
        float currentRotation = upperHandle.GetRotation();
        if (Math.Abs(currentRotation - lastRotation) > 60)
        {
            lastRotation = currentRotation;
            return true;
        }
        lastRotation = currentRotation;
        return false;
    }
}
