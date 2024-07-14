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
    public GameObject bomb;

    public Transform playerSpawn;
    public Transform coinSpawn;

    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;

    private SpeechOut _speechOut;

    private bool started = false;
    private int level = 0;
    private int coinCounter = 0;
    private int bombCounter = 0;

    private int lvl_coins = 0;
    private int lvl_bombs = 0;
    private int lvl_moving = 0;

    // Levels
    // [Coin count, bomb count, moving bool]
    private int[,] levels = {{1,0,0}, {1,1,1}};

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
        Level level1 = GetComponent<Level>();
        await level1.PlayIntroduction(0.5f, 3000);

        await _speechOut.Speak("Introduction finished, game starts.");

        // await StartGame();
        await LevelManager();
    }

    // async Task StartGame()
    // {
    //     await Task.Delay(500);
        
    //     Instantiate(player, playerSpawn);
    //     // GameObject c = Instantiate(coin, coinSpawn);      
    //     // GameObject c = Instantiate(coin, new Vector3(UnityEngine.Random.Range(-5f, 0.5f), 0f, -7.02f), Quaternion.identity); 
    //     GameObject c = InstantiateCoin();  
        
    //     await _lowerHandle.SwitchTo(c, 50.0f);
    //     _upperHandle.Free();
    // }

    GameObject InstantiateCoin() {
        // return Instantiate(coin, new Vector3(UnityEngine.Random.Range(-5f, 0.5f), 0f, -7.02f), Quaternion.identity);
        return Instantiate(coin, new Vector3(-2.44f, 0f, -7.02f), Quaternion.identity);
    }

    GameObject InstantiateBomb() {
        // return Instantiate(bomb, new Vector3(UnityEngine.Random.Range(-5f, 0.5f), 0f, -7.02f), Quaternion.identity);
        return Instantiate(bomb, new Vector3(-2.44f, 0f, -7.02f), Quaternion.identity);    
        }

    void Update() {

        if(Input.GetKey("s") && !started) {
            started = true;
            Introduction();
        }

        if(Input.GetKey("c")) {
            
            _speechOut.Speak(coinCounter + " coins collected");

        }

        if(Input.GetKey("p")) {
            
            _speechOut.Speak(bombCounter + " bombs avoided");

        }

    }

    public void IncreaseCoinCounter() {
        coinCounter++;
        Debug.Log("Counter increased to " + coinCounter);
        lvl_coins--;
    }


    public void IncreaseBombCounter() {
        bombCounter++;
        lvl_bombs--;
    }


    async Task LevelManager() {

        Instantiate(player, playerSpawn);
        Debug.Log("Player instantiated");
        await _speechOut.Speak("There are " + levels.GetLength(0) + " Levels. Good Luck!");

        for(int i = 0; i < levels.GetLength(0); i++) {
            await _speechOut.Speak("Level " + (level+1) + " starts now.");
            await PlayLevel(levels[i,0], levels[i,1], levels[i,2]);
            await _speechOut.Speak("Level " + (level+1) + " finished.");            
            level++;
        }

        await _speechOut.Speak("Game finished");
    }


    async Task PlayLevel(int c, int b, int m) {

        lvl_coins = c;
        lvl_bombs = b;
        lvl_moving = m;

        Debug.Log("Start of Level " + level + ". Coins: " + lvl_coins + "; Bombs: " + lvl_bombs);

        while (lvl_coins > 0 || lvl_bombs > 0) {

            if(GameObject.FindGameObjectsWithTag("Coin").Length + GameObject.FindGameObjectsWithTag("Bomb").Length < 1) {

                int type = UnityEngine.Random.Range(0,2); // 0 for coin, 1 for bomb

                if(type == 0 && lvl_coins > 0) {
                    Debug.Log("Instantiating coin.");
                    GameObject coinprefab = InstantiateCoin();
                    await _lowerHandle.SwitchTo(coinprefab, 50.0f);
                } else if ( type == 1 && lvl_bombs > 0) {
                    GameObject bombprefab = InstantiateBomb();
                    await _lowerHandle.SwitchTo(bombprefab, 50.0f);
                }

            }

            await Task.Delay(100);

        }

    }

}
