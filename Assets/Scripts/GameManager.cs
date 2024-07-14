using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using Unity.VisualScripting;
using SpeechIO;

public class GameManager : MonoBehaviour
{


    SpeechOut speechOut = new SpeechOut();
    public GameObject ball;

    private GameObject currentBallInstance;
    public GameObject player;

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;

    public int currentLevelNumber = 1;
    private const int maxLevelNumber = 5;

    public Transform ballSpawn;
    public Transform playerSpawn;
    
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    
    PantoCollider[] pantoColliders;
    
    // Start is called before the first frame update
    void Start()
    {
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();
        
        level1.SetActive(true);
        level2.SetActive(false);
        Introduction();
    }

    GameObject FindLevel(int levelNumber){
        switch(levelNumber){
            case 1: 
                return level1;
            case 2: 
                return level2;
            case 3: 
                return level3;
            case 4: 
                return level4;
            case 5: 
                return level5;
            default:
                return level1;
        }
    }

    async void SwitchToLevel(int levelNumber){
        await Task.Delay(1000);
        speechOut.Speak("Level successfully completed");
        Ball ballComp = currentBallInstance.GetComponent<Ball>();
        ballComp.Reset();
        GameObject level = FindLevel(levelNumber);
        GameObject currentLevel = FindLevel(currentLevelNumber);
        switch (levelNumber){
            case 1:
                speechOut.Speak("");
                break;
            case 2:
                speechOut.Speak("Destroy 3 Bricks with Ball using paddle");
                break;
            case 3:
                speechOut.Speak("Destroy 3 Bricks, look out for unbreakable brick");
                break;
            case 4:
                speechOut.Speak("Destory 3 Bricks, look out for unbreakable bricks");
                break;
            case 5:
                speechOut.Speak("Collect the speed power up and destory 5 bricks use 2 unbreakable bricks");
                break;
            default: break;
        }
        currentLevel.SetActive(false);
        level.SetActive(true);
        currentLevelNumber = levelNumber;
    }

    void Update(){
        GameObject level = FindLevel(currentLevelNumber);
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Block");
        if(objects.Length == 0 && currentLevelNumber < maxLevelNumber){
            SwitchToLevel(currentLevelNumber + 1);
        }

    }
    
    async Task Introduction()
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
        
        Instantiate(player, playerSpawn);
        currentBallInstance = Instantiate(ball, ballSpawn);
        
        // TODO 3:
        await _lowerHandle.SwitchTo(currentBallInstance, 50.0f);
        _upperHandle.Free();
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
