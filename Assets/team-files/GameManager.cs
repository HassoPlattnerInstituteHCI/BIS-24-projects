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
        
        _speechOut.Speak("Move both handles in the middle to start the game");
    }

    async void Introduction()
    {
        Debug.Log("Intro");
        Level level = GetComponent<Level>();

        _upperHandle.Freeze();
        _lowerHandle.Freeze();

        Debug.Log("play Intro");

        await level.PlayIntroduction(1.0f, 500);
        
        await StartGame();
    }

    public async Task StartGame()
    {

        await RenderObstacle();
        
    }

    async Task RenderObstacle()
    {
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.CreateObstacle();
            collider.Enable();
        }
    }

    public async Task DestroyObstacle()
    {
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.Remove();
        }
    }

}
