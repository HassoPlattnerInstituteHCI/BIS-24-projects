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
    public bool lvl_moving = false;
    public float speed = 1f;
    public float maxSpeed = 2.3f;

    private SoundEffects soundeffects;

    struct TRLevel {
        public string intro;
        public int coins;
        public int bombs;
        public bool moving;
        public bool startingSpeed;
        public bool increasingSpeed;

        public TRLevel(string _intro, int c, int b, bool m, bool sS, bool iS) {
            intro = _intro;
            coins = c;
            bombs = b;
            moving = m;
            startingSpeed = sS;
            increasingSpeed = iS;
        }

        public TRLevel(int c, int b) {
            intro = "";
            coins = c;
            bombs = b;
            moving = false;
            startingSpeed = false;
            increasingSpeed = false;
        }
    
    }

    private TRLevel[] levels = {
        new TRLevel(1,0),
        new TRLevel("", 100,0, true, true, true),
        new TRLevel("Coins are now moving sideways as well.", 2, 0, true, false, false),
        new TRLevel("Try to avoid the bombs", 2,2, true, false, false),
        new TRLevel("Try to keep up!", 3,2, true, true, false),
        new TRLevel("Set your own personal record!", 100, 20, true, false, true)
    };


    // Start is called before the first frame update
    private void Awake()
    {
        _speechOut = new SpeechOut();
    }

    void Start()
    {
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();

        soundeffects = GameObject.FindWithTag("Panto").GetComponent<SoundEffects>();
        
        // Introduction();
    }
    
    async void Introduction()
    {
        Level level1 = GetComponent<Level>();
        await level1.PlayIntroduction(0.5f, 2000);

        await _speechOut.Speak("Introduction finished.");

        await LevelManager();
    }


    GameObject InstantiateCoin() {
        soundeffects.SpawnCoinClip();
        return Instantiate(coin, new Vector3(UnityEngine.Random.Range(-2f, 1f), 0f, -7.02f), Quaternion.identity);
        // return Instantiate(coin, new Vector3(-2.44f, 0f, -7.02f), Quaternion.identity);
    }

    GameObject InstantiateBomb() {
        soundeffects.SpawnBombClip();
        return Instantiate(bomb, new Vector3(UnityEngine.Random.Range(-2f, 1f), 0f, -7.02f), Quaternion.identity);
        // return Instantiate(bomb, new Vector3(-2.44f, 0f, -7.02f), Quaternion.identity);    
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
            await PlayLevel(levels[i]);
            await _speechOut.Speak("Level " + (level+1) + " finished.");  
            await soundeffects.PlayFinishedLevelClip();     
            level++;
        }

        await _speechOut.Speak("Game finished");
    }


    async Task PlayLevel(TRLevel level) {

        lvl_coins = level.coins;
        lvl_bombs = level.bombs;
        lvl_moving = level.moving;

        if(level.intro != "") {
            await _speechOut.Speak(level.intro);
        }

        if(level.startingSpeed) {speed -= 0.2f;}

        Debug.Log("Start of Level " + level + ". Coins: " + lvl_coins + "; Bombs: " + lvl_bombs);

        while (lvl_coins > 0 || lvl_bombs > 0) {

            Debug.Log("Game not over yet.");

            if(GameObject.FindGameObjectsWithTag("Coin").Length + GameObject.FindGameObjectsWithTag("Bomb").Length < 1) {

                Debug.Log("New prefab.");

                int type = UnityEngine.Random.Range(0,2); // 0 for coin, 1 for bomb

                if(type == 0 && lvl_coins > 0) {
                    Debug.Log("Instantiating coin.");
                    GameObject coinprefab = InstantiateCoin();
                    await _lowerHandle.SwitchTo(coinprefab, 50.0f);
                    
                } else if ( type == 1 && lvl_bombs > 0) {
                    GameObject bombprefab = InstantiateBomb();
                    await _lowerHandle.SwitchTo(bombprefab, 50.0f);
                }

                if(level.increasingSpeed) {if (speed < maxSpeed) speed -= 0.03f;}

            }

            await Task.Delay(100);

        }

    }

}
