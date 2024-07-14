using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

public class CubixGameManager : MonoBehaviour
{

    public GameObject player;
    public Transform playerSpawn;
    
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    
    PantoCollider[] pantoColliders;
    
    // Start is called before the first frame update
    void Start()
    {
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();
        
        StartGame();
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
        
        GameObject gb = Instantiate(player, playerSpawn);
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
