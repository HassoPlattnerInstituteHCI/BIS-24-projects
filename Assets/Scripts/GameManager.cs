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
        
        Introduction();
    }
    
    async void Introduction()
    {
        await Task.Delay(1000);
        Level level = GetComponent<Level>();
        await level.PlayIntroduction(0.2f, 3000);
        await Task.Delay(1000);
        await ResetGame();
    }

    async Task ResetGame()
    {
        
        // await Task.Delay(1000);
        await RenderObstacle();
        await Task.Delay(1000);
        
        Instantiate(player, playerSpawn);
        Instantiate(enemy, new Vector3(0.35f, 0.0f, -5.64f), Quaternion.identity);
        GameObject sb = Instantiate(ball, ballSpawn);
        
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
