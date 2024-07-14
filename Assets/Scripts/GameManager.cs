using System;
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

    //public Transform target;
    //public Transform target2;

    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;

    private SpeechOut _speechOut;

    private GameObject itHandle;

    private System.Random rnd;

    public GameObject target_prefab;
    public GameObject cube_prefab_blue;
    public GameObject cube_prefab_red;

    public GameObject shooting_blue_prefab;
    public GameObject shooting_red_prefab;

    public int bubbles;


    PantoCollider[] pantoColliders;

    // Start is called before the first frame update

    private void Awake()
    {
        _speechOut = new SpeechOut();
        rnd = new System.Random();
    }

    async void Start()
    {
        _upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        _lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        itHandle = GameObject.Find("ItHandle");

        await StartGame();
    }

    void Update()
    {//float rotation = itHandle.transform.rotation.eulerAngles.y;
     //         Debug.Log(rotation);
    }


    async Task StartGame()
    {
        // handle bewegen sich in die Mitte
        await Task.Delay(1000);
        await _upperHandle.SwitchTo(GameObject.Find("Target"), 50f);
        await Task.Delay(1000);
        await _lowerHandle.SwitchTo(GameObject.Find("Target"), 50f);
        await Task.Delay(1000);
        _upperHandle.Free();
        _lowerHandle.Free();

        // Bubbles erstellen
        Instantiate(cube_prefab_blue, new Vector3((float)-2.75, 0f, -6f), Quaternion.identity);
        Instantiate(cube_prefab_blue, new Vector3((float)-1.15, 0f, -6f), Quaternion.identity);
        Instantiate(cube_prefab_red, new Vector3((float)-0.25, 0f, -6f), Quaternion.identity);
        Instantiate(cube_prefab_red, new Vector3((float)1.25, 0f, -6f), Quaternion.identity);
        Instantiate(cube_prefab_blue, new Vector3((float)2.25, 0f, -6f), Quaternion.identity);
        await RenderObstacle();

        //handle bewegt sich zu bubbles

        _speechOut.Speak("Here are the bubbles. Destroy them!");
        await _upperHandle.SwitchTo(GameObject.Find("bubbles_target"), 50f);
        await Task.Delay(1000);
        _upperHandle.Free();

        // Zeit zum Abtasten der bubbles geben
        //vlt wenn man bei letzter bubble angekommen ist gehts weiter??


        // _speechOut.Speak("You've got a red bubble. Turn this handle to aim.");

        float last_rotation = itHandle.transform.rotation.eulerAngles.y;
        await Task.Delay(2000);
        float rotation = itHandle.transform.rotation.eulerAngles.y;

        //     if(Math.Abs(last_rotation - rotation)> 45){
        //         Debug.Log(last_rotation);
        //         Debug.Log(rotation);
        //         _speechOut.Speak("juchuuuuuu");
        //    }


        // destroy bubbles
        int red_bubbles = GameObject.FindGameObjectsWithTag("red").Length;
        int blue_bubbles = GameObject.FindGameObjectsWithTag("blue").Length;
        bubbles = red_bubbles + blue_bubbles;

        int color = 0;
        string color_speech = " ";
        while (bubbles > 0)
        {
            float last_rotation = _lowerHandle.GetRotation();

            color = rnd.Next(2);
            // color = rnd.Next(0, 1);
            //await _speechOut.Speak(obstacle_count.ToString() + " balls left.");
            //spawn new ball
            if (blue_bubbles > 0 color == 0)
            {
                GameObject shooting_bubble = Instantiate(shooting_blue_prefab, GameObject.Find("Target").transform.position, Quaternion.identity);
                color_speech = "blue";
            }
            else if (red_bubbles > 0)
            {
                GameObject shooting_bubble = Instantiate(shooting_red_prefab, GameObject.Find("Target").transform.position, Quaternion.identity);
                color_speech = "red";
            }
            await _speechOut.Speak("Your bubble is " + color_speech);
            color = color % 2 + 1;

            await Task.Delay(1000);

            //schießzeug und so 

            //     while(Math.Abs(last_rotation-_lowerHandle.GetRotation()) < 0.5){//versch. Werte testen
            //         last_rotation = _lowerHandle.GetRotation();//Nutzer hat noch nicht geschossen
            //         await Task.Delay(1500);
            //     }

            //reset to startposition
            // await Task.Delay(1000);
            // await _upperHandle.SwitchTo(GameObject.Find("Target"), 50f);
            // await Task.Delay(1000);
            // await _lowerHandle.SwitchTo(GameObject.Find("Target"), 50f);
            // await Task.Delay(1000);
            // _upperHandle.Free();
            // _lowerHandle.Free();

            bubbles--;
            GameObject.Destroy(GameObject.FindGameObjectsWithTag("shooting_bubble")[0]);

        }
        await _speechOut.Speak("You destroyed all bubbles!");



        // while(obstacle_count>0){
        //     //await _speechOut.Speak(obstacle_count.ToString() + " balls left.");
        //     float last_rotation = _lowerHandle.GetRotation();
        //     /*GameObject target = Instantiate(target_prefab, new Vector3(2f, 0f, -6f), Quaternion.identity);
        //     _lowerHandle.SwitchTo(target, 200f);*/
        //     Debug.Log(last_rotation);
        //     while(Math.Abs(last_rotation-_lowerHandle.GetRotation()) < 0.5){//versch. Werte testen
        //         last_rotation = _lowerHandle.GetRotation();//Nutzer hat noch nicht geschossen
        //         await Task.Delay(1500);
        //     }
        //     //Ball wird abgeschossen
        //     await _speechOut.Speak("shooting");
        //     Vector3 pos = _lowerHandle.GetPosition();
        //     GameObject target = Instantiate(target_prefab, new Vector3(-pos.x, 0f, -6f), Quaternion.identity);
        //     //await _lowerHandle.SwitchTo(target, 200f);
        //     obstacle_count = 0;


        // }



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
