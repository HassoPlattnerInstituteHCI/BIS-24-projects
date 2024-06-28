using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;
public class GameManager : MonoBehaviour
{
    public GameObject wall;
    
    public Transform upperSpawn;
    public Transform lowerSpawn;
    
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    
    private SpeechOut _speechOut;
    
    PantoCollider[] pantoColliders;

    private void Awake()
    {
        _speechOut = new SpeechOut();
    }
        void NewStart()
    {
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();
        
        Introduction();
    }

    async void Introduction()
    {
        Level level = GetComponent<Level>();


        // _upperHandle.Freeze();
        // _lowerHandle.Freeze();

        await level.PlayIntroduction(1.0f, 500);
        //await level.PlayPAIntro();
        // await Task.Delay(1000);
        
        await StartGame();
    }

    async Task StartGame()
    {
        // await Task.Delay(1000);

        await RenderObstacle();
        
        // await Task.Delay(1000);
        
        await _speechOut.Speak("Try it yourself.");
        
        /*Instantiate(player, playerSpawn);
        Instantiate(enemy, new Vector3(0.35f, 0.0f, -5.64f), Quaternion.identity);
        GameObject sb = Instantiate(ball, ballSpawn);*/
        
        //await _lowerHandle.SwitchTo(sb, 50.0f);
        _upperHandle.Free();
        _lowerHandle.Free();
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

    void Update() 
    {
        if (Input.GetKey("s")) {
            NewStart();
        }
    }
}
