using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class GameManager_level1 : MonoBehaviour
{

    //public Transform target;
    //public Transform target2;
    
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    
    private SpeechOut _speechOut;
    public GameObject ball_red_prefab;
    
    PantoCollider[] pantoColliders;
    
    // Start is called before the first frame update

    private void Awake()
    {
        _speechOut = new SpeechOut();
    }

    void Start()
    {
        _upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        _lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        // TODO 1: remove this comment-out
        Introduction();
    }

    void update()
    {
        //Debug.Log(target.transform.position);
    }
    
    async void Introduction()
    {
        //Level level = GetComponent<Level>();
        //await level.PlayIntroduction(0.2f, 3000);
        //await Task.Delay(1000);
        
        // TODO 2:
        await StartGame();
    }

    async Task StartGame()
    {
        await Task.Delay(1000);
        await  _upperHandle.SwitchTo(GameObject.Find("Target"), 100f);
        //await  _lowerHandle.SwitchTo(GameObject.Find("Target"), 100f);
        for(double i = -2.75; i<3.1; i+=0.8){

        Instantiate(ball_red_prefab, new Vector3((float)i, 0f, -6f), Quaternion.identity);
        }
        await RenderObstacle();
        await Task.Delay(3000);
        /*_speechOut.Speak("Here is a block, find another one on its right side using this handle");
        await  _upperHandle.SwitchTo(GameObject.Find("ball_prefab(Clone)"), 100f);*/
        _upperHandle.Free();

        // TODO 4: activate PlayerWall game object at Unity editor, and remove this comment-out
        //Handles an richtige Stelle
        //await _upperHandle.MoveToPosition(target.position, 50f, false);//Positionen anpassen
        //await _upperHandle.MoveToPosition(target.position, 50f, false);//Positionen anpassen
        //await _lowerHandle.MoveToPosition(new Vector3(0.35f, 0.0f, -5.64f), 30f, true);//Positionen anpassen
        //_lowerHandle.Freeze();
        //_upperHandle.MoveToPosition(new Vector3(-2.7f, 0.0f, -7f), 12f, true);

        //await RenderObstacle();
        /*
        await Task.Delay(1000);
        //await _upperHandle.MoveToPosition(target2.position, 2f);
        _speechOut.Speak("Here is a block, find another one on its right side using this handle");
        _upperHandle.Free();*/
        //_lowerHandle.Free();
        //await _lowerHandle.MoveToPosition(new Vector3(0.35f, 0.0f, -5.64f), 30f, true);//Positionen anpassen
        //GetComponent<PlayerSoundEffect>().PlayerSoundEffect;

        /*Instantiate(player, playerSpawn);
        Instantiate(enemy, new Vector3(0.35f, 0.0f, -5.64f), Quaternion.identity);
        GameObject sb = Instantiate(ball, ballSpawn);
        
        // TODO 3:
        await _lowerHandle.SwitchTo(sb, 50.0f);
        _upperHandle.Free();*/
    }

    /*async void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("block2")) {
            //soundEffects.PlayPaddleClip();
            _speechOut.Speak("Yay, you completed the first level!");
            
        }
    }*/

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
