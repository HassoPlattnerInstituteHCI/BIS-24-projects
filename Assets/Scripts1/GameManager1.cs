using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class GameManager1 : MonoBehaviour
{

    public GameObject player;
    public GameObject fieldPrefab;
    private GameObject field;

    public Transform playerSpawn;
    
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    private bool started = false;
    private bool notSpeaking = true;
    
    private SpeechOut _speechOut;

    private float rotation = 0;
    
    PantoCollider[] pantoColliders;
    
    // Start is called before the first frame update

    private void Awake()
    {
        _speechOut = new SpeechOut();
    }

    void Start()
    {
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();
        
        field = Instantiate(fieldPrefab);
        // TODO 1: remove this comment-out
        Introduction();
    }
    
    async void Update() {
        if(started&&notSpeaking) {
            if(rotation-5>_upperHandle.GetRotation()) {
                notSpeaking=false;
                Field fieldScript = (Field) field.GetComponent<Field>();
                fieldScript.changeColor();
                await _speechOut.Speak("Pixel colored!");
                notSpeaking=true;

            }
            rotation = _upperHandle.GetRotation();
        }
    }
    async void Introduction()
    {
        Level level = GetComponent<Level>();
        await level.PlayIntroduction(0.2f, 3000);
        await Task.Delay(1000);
        
        // TODO 2:
        await StartGame();
    }

    async Task StartGame()
    {
        await Task.Delay(1000);

        // TODO 4: activate PlayerWall game object at Unity editor, and remove this comment-out
        await RenderObstacle();
        
        await Task.Delay(1000);
        
        await _speechOut.Speak("Introduction finished. Rotate me-handle to the right to colour a pixel");
        
        Instantiate(player, playerSpawn);
        // Instantiate(enemy, new Vector3(0.35f, 0.0f, -5.64f), Quaternion.identity);
        // GameObject sb = Instantiate(ball, ballSpawn);
        
        // TODO 3:
        // await _lowerHandle.SwitchTo(sb, 50.0f);
        _upperHandle.Free();
        rotation = _upperHandle.GetRotation();
        started = true;
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
}
