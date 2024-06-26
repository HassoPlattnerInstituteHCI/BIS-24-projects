using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Serialization;
using Task = System.Threading.Tasks.Task;

public class PathPlanningGameManager : MonoBehaviour
{

    public GameObject panto;

    public GameObject goalPrefab;
    public GameObject playerPrefab;
    private GameObject _goal;
    private GameObject _player;

    public Transform playerSpawn;
    public Transform goalSpawn;
    
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    
    PantoCollider[] pantoColliders;
    
    // Start is called before the first frame update
    void Start()
    {
        _upperHandle = panto.GetComponent<UpperHandle>();
        _lowerHandle = panto.GetComponent<LowerHandle>();
        
        Introduction();
    }
    
    async void Introduction()
    {
        await StartGame();
    }

    async Task StartGame()
    { 
        _player = Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);
        _goal = Instantiate(goalPrefab, goalSpawn.position, Quaternion.identity);
        
        // TODO 3:
        await _upperHandle.SwitchTo(_player, 1.0f);
        await _lowerHandle.SwitchTo(_goal, 1.0f);
        
        await Task.Delay(1000);
        _upperHandle.Free();
        _lowerHandle.Freeze();
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
}

