using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using SpeechIO;
using Unity.VisualScripting;

public class DirectionSelector : MonoBehaviour
{
    private PropertyHandler propertyHandler;
    private BezierCurveBuilder curveBuilder;
    private RotatingHandle handle;
    private List<string> directions = new List<string>();
    private SpeechOut speaker;
    private bool invoked = false; 

    // Start is called before the first frame update
    void Start()
    {
        propertyHandler = GetComponent<PropertyHandler>();
        speaker = new SpeechOut();
        directions.Add("AIR");
        directions.Add("EARTH");
        directions.Add("FIRE");
        directions.Add("WATER");
    }

    private void Update()
    {
        if (propertyHandler.tutorialDone && !invoked)
        {
            InvokeRepeating("SetNewDirection", 5f, 5f);
            invoked = true;
        }
    }

    async private void SetNewDirection()
    {
        propertyHandler.directionSelected = GetRandomElement(directions);
        await speaker.Speak(propertyHandler.directionSelected);
    }

    string GetRandomElement(List<string> list)
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogWarning("The list is empty or null.");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, list.Count);
        return list[randomIndex];
    }
}
