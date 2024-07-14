// Initiliaze tutorial
// Set Player to starting point
// Set enemy to starting point
// Explain sound
using System.Threading.Tasks;
using UnityEngine;
using System;
using DualPantoToolkit;
using Unity.VisualScripting;
using Object = System.Object;

public class Maze : MonoBehaviour
{
    float handleSpeed = 10f;
    private PantoHandle upperHandle;
    private int currentLevel = 1;
    public GameObject WallLevel1;
    public GameObject WallLevel2;

    void Awake()
    {
        // Add Audio
    }

    async void Start(){
        // Explain what is going to happen
        // Move Handle to start position
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

    public void StartLevel(int level)
    {
        Reset();
        if( level == 1)
        {
            Vector3 spawnPosition = new Vector3(3,0,-11);
            Quaternion myRotation = Quaternion.identity;
            myRotation.eulerAngles = new Vector3(0, 45, 0);
            Instantiate(WallLevel1, spawnPosition, myRotation);
        } else if( level == 2){
            Vector3 spawnPosition = new Vector3(2.3f,0,-12);
            Quaternion myRotation = Quaternion.identity;
            myRotation.eulerAngles = new Vector3(0, 80, 0);
            Instantiate(WallLevel2, spawnPosition, myRotation);
            // ADD IT
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
    async void Reset(){
        // SET PLAYER TO START
        // DELETE INNER WALL
        DisableWalls();
        Vector3 start = GameObject.Find("Start").transform.position;
        upperHandle =  GameObject.Find("Panto").GetComponent<UpperHandle>();
        GameObject[] Innerwalls = GameObject.FindGameObjectsWithTag("InnerWall");
        foreach (GameObject wall in Innerwalls)
        {
            Destroy(wall);
        }
        Console.WriteLine(start);
        upperHandle.MoveToPosition(start, handleSpeed);
        EnableWalls();
    }

    public void EnteredArea(string areaName)
    {
        if (areaName == "End")
        {
            currentLevel++;
            StartLevel(currentLevel);
        }
        if (areaName == "Start")
        {
            // Play sound
        }
    }
    public void DisableWalls()
    {
        PantoCollider[] pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.Disable();
        }
    }
    public void EnableWalls()
    {
        PantoCollider[] pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.Enable();
        }
    }
}
