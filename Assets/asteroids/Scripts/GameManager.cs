using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

public class GameManager : MonoBehaviour
{

    public GameObject projectile;
    public GameObject player;
    public GameObject enemy;

    public Transform projectileSpawn;
    public Transform playerSpawn;
    public Transform enemySpawn;
    
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    
    PantoCollider[] pantoColliders;
    
    void Start()
    {
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();

        Introduction();
    }
    
    async void Introduction()
    {
        Level level = GetComponent<Level>();
        await level.PlayIntroduction(0.2f, 3000);
        await Task.Delay(1000);
        
        await StartGame();
    }

    async Task StartGame()
    {
        await Task.Delay(1000);

        await RenderObstacle();
        
        await Task.Delay(1000);
        
        GameObject me = Instantiate(player, playerSpawn.position, playerSpawn.rotation);
        GameObject it = Instantiate(enemy, enemySpawn.position, enemySpawn.rotation);

        await _upperHandle.SwitchTo(me, 5.0f);
        await _lowerHandle.SwitchTo(it, 5.0f);
        //_upperHandle.Free();
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
