using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

public class GameManager : MonoBehaviour
{

    //public GameObject ball;
    public GameObject caldron;
    public GameObject potion;

    public Transform caldron_spawn;
    public Transform potion_spawn;

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
        // await RenderObstacle();
        
        await Task.Delay(1000);
        
        Instantiate(caldron, caldron_spawn);
        Instantiate(potion, potion_spawn);
        
        // TODO 3:
        await _upperHandle.SwitchTo(potion, 50.0f);
        _lowerHandle.Free();
    }

    /*async Task RenderObstacle()
    {
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.CreateObstacle();
            collider.Enable();
        }
    }*/
}
