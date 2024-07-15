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

    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;

    private SpeechOut _speechOut;
    private SpeechIn speechIn;

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
    void onRecognized(string message)
    {
        return;
    }
    private void Awake()
    {
        _speechOut = new SpeechOut();
        speechIn = new SpeechIn(onRecognized);
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
        float test = 0;

        while (true)
        {
            Debug.Log(test);
            Task.Delay(1000);
            test = itHandle.transform.rotation.eulerAngles.y;
        }


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




        // destroy bubbles
        int red_bubbles = GameObject.FindGameObjectsWithTag("red").Length;
        int blue_bubbles = GameObject.FindGameObjectsWithTag("blue").Length;
        bubbles = red_bubbles + blue_bubbles;
        int last_bubbles = bubbles;

        int color = 0;
        string color_speech = " ";
        bool firstTime = true;
        int counter = 0;
        while (bubbles > 0)
        {
            last_bubbles = bubbles;
            color = rnd.Next(2);
            // color = rnd.Next(0, 1);
            //await _speechOut.Speak(obstacle_count.ToString() + " balls left.");
            //spawn new ball
            if (blue_bubbles > 0 && color == 0)
            {
                GameObject shooting_bubble = Instantiate(shooting_blue_prefab, GameObject.Find("Target").transform.position, Quaternion.identity);
                color_speech = "blue";
            }
            else if (red_bubbles > 0 && color == 1)
            {
                GameObject shooting_bubble = Instantiate(shooting_red_prefab, GameObject.Find("Target").transform.position, Quaternion.identity);
                color_speech = "red";
            }
            await _speechOut.Speak("Your bubble is " + color_speech);

            //schießzeug und so 


            if (firstTime)
            {
                await _speechOut.Speak("Turn the handle to aim. Say Go to shoot the bubble.");
                firstTime = false;
            }
            await Task.Delay(1500);
            float rotation = itHandle.transform.rotation.eulerAngles.y;
            Debug.Log(rotation);
            Debug.Log(itHandle.transform.rotation.y);

            speechIn.StartListening();
            await speechIn.Listen(new Dictionary<string, KeyCode>() {
                 { "shoot", KeyCode.Y},
                 { "go", KeyCode.N}
                  });
            if (rotation > 270) rotation = rotation - 360;
            double block_position = (rotation + 90) / 25;

            double x = -2.25 + (0.9 * block_position);
            // Debug.Log(rotation);
            // Debug.Log(block_position);
            // Debug.Log(x);


            GameObject target = Instantiate(target_prefab, new Vector3((float)x, 0f, -6.5f), Quaternion.identity);
            await _lowerHandle.SwitchTo(target, 50f);
            await Task.Delay(1000);
            rotation = 0;


            red_bubbles = GameObject.FindGameObjectsWithTag("red").Length;
            blue_bubbles = GameObject.FindGameObjectsWithTag("blue").Length;
            bubbles = red_bubbles + blue_bubbles;
            if (last_bubbles == bubbles)
            {
                _speechOut.Speak("Try again!");
            }
            // _lowerHandle.Freeze();




            GameObject.Destroy(GameObject.FindGameObjectsWithTag("shooting_bubble")[0]);


            //reset to startposition
            await Task.Delay(1000);
            await _lowerHandle.SwitchTo(GameObject.Find("Target"), 50f);
            await Task.Delay(1000);
            _lowerHandle.Free();


            counter++;
        }
        await _speechOut.Speak("You destroyed all bubbles! You used " + counter + " bubbles.");





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

