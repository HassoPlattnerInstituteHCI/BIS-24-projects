using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject empty;

    public GameObject playerPos;
    public GameObject emptyPos;

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

        await StartGame();
    }

    async Task StartGame()
    {
        SpeechOut speechOut = new SpeechOut();
        Instantiate(player, playerPos.transform);
        Instantiate(empty, emptyPos.transform);

        //move upper Handle to start 
        speechOut.Speak("Move the square");
        await _upperHandle.SwitchTo(playerPos, 0.4f);
        _upperHandle.Free();

        // set Lower Handle to empty slot in square
        speechOut.Speak("To the empty slot in the quad");
        await _lowerHandle.SwitchTo(emptyPos, 0.5f);
        await _lowerHandle.SwitchTo(emptyPos, 50f);


        await Task.Delay(1000);

        // TODO 4: activate PlayerWall game object at Unity editor, and remove this comment-out
        await RenderObstacle();

        await Task.Delay(1000);
    }

    async Task RenderObstacle()
    {
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            Debug.Log("Enabled Wall");
            collider.CreateObstacle();
            collider.Enable();
        }
    }
}
