using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;
public class GameManager : MonoBehaviour
{
    public GameObject wall;
    
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    
    private SpeechOut _speechOut;
    private SoundManager soundManager;
    private ObjectHandler objectHandler;
    private ObjectSelector objectSelector;
    
    PantoCollider[] pantoColliders;

    private void Awake()
    {
        _speechOut = new SpeechOut();
    }
        
    void Start()
    {
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();

        //_speechOut.Speak("Move both handles in the middle to start the intro.");
        
        _speechOut.Speak("Bewege beide Griffe in die Mitte um das Intro zu starten.");
    }

    async void Introduction()
    {
        Debug.Log("Intro");
        
    }

    

   

   

}
