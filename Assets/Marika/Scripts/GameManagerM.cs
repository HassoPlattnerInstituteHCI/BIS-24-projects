using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

public class GameManagerM : MonoBehaviour
{
    public GameObject me;
    public GameObject it;

    public Transform meSpawn;
    public Transform itSpawn;

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

        Instantiate(me, meSpawn);

        Instantiate(it, itSpawn);

  
        _upperHandle.Free();
        _lowerHandle.Free();
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

    void Update()
    {

    }
}
