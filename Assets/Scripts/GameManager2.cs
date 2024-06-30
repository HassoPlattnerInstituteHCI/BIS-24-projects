using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class GameManager2 : MonoBehaviour
{

    
    public GameObject player;
    public GameObject exit;

    public Transform playerSpawn;
    public Transform exitSpawn;

    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;

    private SpeechOut _speechOut;

    private bool started = false;

    // Start is called before the first frame update
    private void Awake()
    {
        _speechOut = new SpeechOut();
    }

    void Start()
    {
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();
        
        // Introduction();
    }
    
    async void Introduction()
    {
        Level level = GetComponent<Level>();
        await level.PlayIntroduction(0.5f, 3000);

        await StartGame();
    }

    async Task StartGame()
    {
        
        await Task.Delay(500);
        await _speechOut.Speak("Game starts. Try finding your way out of the labyrinth.");
        
        Instantiate(player, playerSpawn);
        GameObject e = Instantiate(exit, new Vector3(4.02f, 0f, -15.88f), Quaternion.identity);
        _upperHandle.Free();
    }

    void Update() {

        if(Input.GetKey("s") && !started) {
            started = true;
            Introduction();
        }

    }


    


}
