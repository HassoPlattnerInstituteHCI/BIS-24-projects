using SpeechIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PropertyHandler : MonoBehaviour
{
    public Boolean caldronActionActive = false;
    public Boolean pathCompleted = false;
    public string directionSelected = "NONE";
    public Boolean directionSelectorActive = false;
    public Boolean selectionWasActive = true;
    public Vector3[] path;
    public string madePotion = "NONE";
    public Vector3 startPosition = Vector3.zero;
    public bool soundLocked = false;

    public bool firstStirr = false;
    public bool firstSlotTriggered = false;
    public bool ingredientAdded = false;
    public bool potionMade = false;
    public bool tutorialDone = false;

    private SpeechOut speechOut;

    private void Start()
    {
        speechOut = new SpeechOut();
        startPosition = transform.position;
        PlayTutorial();
    }

    async private void Update()
    {
        if (madePotion != "NONE")
        {
            Debug.Log("You made a Potion!" + madePotion);
            await speechOut.Speak("You made a Potion of " + madePotion);
            Reset();
        }

        
    }

    private void Reset()
    {
        path = new Vector3[0];
        directionSelected = "NONE";
        madePotion = "NONE";
        GetComponent<BezierCurveBuilder>().DrawBezierCurve();

        transform.position = Vector3.MoveTowards(transform.position, startPosition, 5 * Time.deltaTime);
    }

    async void PlayTutorial()
    {
        await speechOut.Speak("Ahh, the intern is here!");
        await speechOut.Speak("Can you grab the handle and stir the pot for me real quick?");

        await WaitUntilTrue(() => firstStirr == true);

        await speechOut.Speak("Thank you.");
        await speechOut.Speak("Use your magical senses to locate the potion of air for me please.");
        await speechOut.Speak("Air is found in the north if that helps.");

        await WaitUntilTrue(() => firstSlotTriggered == true);

        await speechOut.Speak("Now you know where to go.");
        await speechOut.Speak("I have many interns like you and can not babysit anyone.");
        await speechOut.Speak("I will call out any new ingredients in stock.");
        await speechOut.Speak("Grab them from the shelf above the caldron. I have conveniently named the, by the direction they will take you.");

        tutorialDone = true;

        await WaitUntilTrue(() => ingredientAdded == true);

        tutorialDone = false;

        await speechOut.Speak("You can only add more ingredients when the last one is completely dissolved. Stir to make that happen.");

        await WaitUntilTrue(() => pathCompleted == true);

        await speechOut.Speak("Find the rest of the path to the potion of air. This once I will be patient.");

        tutorialDone = true;

        await WaitUntilTrue(() => potionMade == true);

        await speechOut.Speak("Good. Time for the real deal.");
        await speechOut.Speak("We are an instant potion vendor.");
        await speechOut.Speak("I will tell you what to make and you make it quick.");
        await speechOut.Speak("If you run out of time...you are fired.");

        tutorialDone = true;

        return;
    }

    private async Task WaitUntilTrue(Func<bool> condition)
    {
        while (!condition())
        {
            await Task.Delay(100); // Check the condition every 100 milliseconds
        }
    }

    public async void Say(string inp)
    {
        await speechOut.Speak(inp);
    }
}
