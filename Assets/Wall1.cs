using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class Wall1 : MonoBehaviour
{
    // Start is called before the first frame update
    PantoCollider[] pantoColliders;
    void Start()
    {
         StartGame();
    }

    // Update is called once per frame

     async Task StartGame()
    {
        await Task.Delay(500);

        // TODO 4: activate PlayerWall game object at Unity editor, and remove this comment-out
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
