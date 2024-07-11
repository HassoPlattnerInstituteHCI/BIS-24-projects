using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

public class Level1Manager : MonoBehaviour
{

    public GameObject fruit;
    public GameObject knife;
    //public GameObject enemy;

    public Transform fruitSpawn;
    public Transform knifeSpawn;
    //public Transform enemySpawn;

    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;

    PantoCollider[] pantoColliders;

    // Start is called before the first frame update
    void Start()
    {
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();

        //fruit deaktivieren
        fruit.SetActive(false);

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

        // TODO 4: activate playerWall game object at Unity editor, and remove this comment-out
        await RenderObstacle();

        await Task.Delay(1000);

        Instantiate(knife, knifeSpawn);
        GameObject sb = Instantiate(fruit, fruitSpawn);
        //fruit wieder aktivieren
        sb.SetActive(true);

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