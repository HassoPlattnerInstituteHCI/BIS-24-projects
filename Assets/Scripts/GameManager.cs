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

    
    public GameObject player;
    public GameObject coin;

    public Transform playerSpawn;
    public Transform coinSpawn;

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
        
        await _speechOut.Speak("Introduction finished, game starts.");
        
        Instantiate(player, playerSpawn);
        // GameObject c = Instantiate(coin, coinSpawn);      
        GameObject c = Instantiate(coin, new Vector3(-0.52f, 0f, -7.02f), Quaternion.identity);       
        
        await _lowerHandle.SwitchTo(c, 50.0f);
        _upperHandle.Free();
    }

    void Update() {

        if(Input.GetKey("s") && !started) {
            started = true;
            Introduction();
        }

    }


    


}
