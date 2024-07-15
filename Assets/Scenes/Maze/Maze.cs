// Initiliaze tutorial
// Set Player to starting point
// Set enemy to starting point
// Explain sound
using System.Threading.Tasks;
using UnityEngine;
using System;
using DualPantoToolkit;
using SpeechIO;
using Unity.VisualScripting;
using Object = System.Object;

public class Maze : MonoBehaviour
{
    float handleSpeed = 10f;
    private PantoHandle upperHandle;
    private int currentLevel = 1;
    public GameObject WallLevel1;
    public GameObject WallLevel2;
    private SpeechOut _speechOut;
    public AudioSource audio;

    void Awake()
    {
        // Add Audio
    }

    async void Start(){
        //_speechOut.SetLanguage(SpeechBase.LANGUAGE.GERMAN);
        //await _speechOut.Speak("Willkommen im MAZE, das Ziel des Spiels ist es, es wieder zu verlassen und IT zuentkommen ");
        OnEnable();
    }

    void OnEnable()
    {
        // Play starting audio
        StartLevel(1);
    }

    void OnUpdate()
    {
        // Joa
    }

    async public void StartLevel(int level)
    {
        Reset();
        if( level == 1)
        {
            //_speechOut.SetLanguage(SpeechBase.LANGUAGE.GERMAN);
            //await _speechOut.Speak("Level 1 beginnt");
            //await _speechOut.Speak("Versuche den Ausgang zu finden!");
            Vector3 spawnPosition = new Vector3(3,0,-11);
            Quaternion myRotation = Quaternion.identity;
            myRotation.eulerAngles = new Vector3(0, 45, 0);
            WallLevel1 = Instantiate(WallLevel1, spawnPosition, myRotation);
            PantoCollider pc = WallLevel1.GetComponent<PantoCollider>();
            pc.CreateObstacle();
            pc.Enable();
        } else if( level == 2){
            Vector3 spawnPosition = new Vector3(2.3f,0,-12);
            Quaternion myRotation = Quaternion.identity;
            myRotation.eulerAngles = new Vector3(0, 80, 0);
            Instantiate(WallLevel2, spawnPosition, myRotation);
        } else if( level == 3){
            // ADD WALLS
            // ADD IT
            // ADD slowwer

        } else if( level == 4){
            // ADD COMPLEXER WALLS
            // ADD IT
            // ADD slower

        } else if( level == 5){
            // ADD COMPLEXER WALLS
            // ADD IT
            // ADD slower
            // ADD KEY
        } else if( level == 6){
            // ADD COMPLEX WALLS
            // ADD IT
            // ADD slower
            // ADD KEY
        }

    }
    void Reset(){
        // SET PLAYER TO START
        // DELETE INNER WALL
        
        // Vector3 start = GameObject.Find("Start").transform.position;
        
        GameObject[] Innerwalls = GameObject.FindGameObjectsWithTag("InnerWall");
        foreach (GameObject wall in Innerwalls)
        {
            PantoCollider pc = wall.GetComponent<PantoCollider>();
            pc.Disable();
            pc.Remove();
            Destroy(wall);
        }
        Vector3 start = new Vector3(0,0,-12);
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        upperHandle.MoveToPosition(start, handleSpeed);
    }

    public void EnteredArea(string areaName)
    {
        if (areaName == "End")
        {
            currentLevel++;
            //_speechOut.SetLanguage(SpeechBase.LANGUAGE.GERMAN);
            //_speechOut.Speak("Du bist den Maze entkommen");
            //Application.Quit();
            StartLevel(currentLevel);
            
        }
        if (areaName == "Start")
        {
            // Play soun
        }
        
    }
    public void DisableWalls()
    {
        PantoCollider[] pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.Disable();
            collider.Remove();
        }
    }
    public void EnableWalls()
    {
        PantoCollider[] pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.CreateObstacle();
            collider.Enable();
        }
    }
}
