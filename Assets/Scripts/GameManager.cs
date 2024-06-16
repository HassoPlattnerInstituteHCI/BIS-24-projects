using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

public class GameManager : MonoBehaviour
{

    public GameObject ball;
    public GameObject player;
    public GameObject enemy;

    public Transform ballSpawn;
    public Transform playerSpawn;
    public Transform enemySpawn;
    
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    
    PantoCollider[] pantoColliders;
    
    // Start is called before the first frame update
    void Start()
    {
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();
        
        // TODO 1: remove this comment-out
        Introduction();
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
        
        Instantiate(player, playerSpawn);
        Instantiate(enemy, new Vector3(0.35f, 0.0f, -5.64f), Quaternion.identity);
        GameObject sb = Instantiate(ball, ballSpawn);
        
        // TODO 3:
        await _lowerHandle.SwitchTo(sb, 50.0f);
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
