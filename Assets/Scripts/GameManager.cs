using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
//using UnityEditor.VersionControl;
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

    public GameObject sphereLvl3;
    public GameObject wall;
    public GameObject instantiatedWall;
    public GameObject dummy;

    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    private GameObject MeHandle;

    PantoCollider[] pantoColliders;

    //lvl 1
    private int level = 1;
    private bool lvl2once = true;

    //lvl 2
    private int pointsReached = 0;
    private bool onPoint = false;
    private Vector3[] points = { new Vector3(0.05f, 0, -6.75f), new Vector3(3.65f, 0, -6.75f), new Vector3(3.65f, 0, -10), new Vector3(3.65f, 0, -13f), new Vector3(0.05f, 0, -13f), new Vector3(-3.75f, 0, -13), new Vector3(-3.75f, 0, -10) };
    public Vector3 lvl2targetPos = new Vector3(-0.5f, 0, -9.5f);

    //lvl 3
    private int lvl3Counter = 0;
    private bool lvl3once1 = true;
    private bool lvl3once2 = true;

    //lvl 4
    private int placedSquares = 0;
    private bool lvl4open = true;

    //Speech IO
    private SpeechIn speechIn;
    private SpeechOut speechOut;

    private void RenderObstacleDelay()
    {
        Task.Delay(1000);

        RenderObstacle();

        Task.Delay(1000);
    }

    // Start is called before the first frame update
    void Start()
    {
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();
        MeHandle = GameObject.Find("MeHandle");

        speechIn = new SpeechIn(onRecognized);
        speechIn.StartListening(new string[] { "place"});

        speechOut = new SpeechOut();

        // TODO 1: remove this comment-out
        Introduction();
        lvl2Listener();

    }

    async void Introduction()
    {
        Level level = GetComponent<Level>();
        await level.PlayIntroduction(0.2f, 3000);
        await Task.Delay(1000);

        await StartGame();
    }

    void onRecognized(string message)
    {
        Debug.Log("[" + this.GetType() + "]: " + message);
    }

    async void introducelvl1()
    {
        speechOut.Speak("Move around the rectangle");
        await _upperHandle.SwitchTo(playerPos, 0.4f);
        _upperHandle.Free();

        // set Lower Handle to empty slot in square
        speechOut.Speak("while following the it handle. I will tell you the number if you reach a point");
        emptyPos.transform.position = points[0];
        await _lowerHandle.SwitchTo(emptyPos, 50f);
    }

    async void introducelvl2()
    {
        lvl2Listener();
        //move upper Handle to start 
        speechOut.Speak("Move the square");
        await _upperHandle.SwitchTo(playerPos, 0.4f);
        _upperHandle.Free();

        // set Lower Handle to empty slot in square
        speechOut.Speak("To the empty slot in the square");
        _lowerHandle.Free();
        emptyPos.transform.position = lvl2targetPos;
        await _lowerHandle.SwitchTo(emptyPos, 0.5f);
        await _lowerHandle.SwitchTo(emptyPos, 50f);
    }

    async void introducelvl3()
    {
        //move upper Handle to start 
        speechOut.Speak("Say pick up to pick up the square you just placed");
        await _upperHandle.SwitchTo(playerPos, 50f);
        _upperHandle.Free();
        //lvl3Listener();
            
        _upperHandle.SwitchTo(playerPos, 0.4f);
        _upperHandle.Free();

        speechOut.Speak("And place it somewhere else");
        _lowerHandle.Free();
        placeListener();
    }

    async void placeListener()
    {
        string input = await speechIn.Listen(new Dictionary<string, KeyCode>() {
            {"place", KeyCode.P }
            });

        if (input == "place" && lvl3once2)
        {
            lvl3once2 = false;
            Instantiate(wall, MeHandle.transform.position, Quaternion.identity);

            //RenderObstacleDelay();

            introducelvl4();
        }
    }

    async void introducelvl4()
    {
        speechOut.Speak("Great you made it to level 4, place 3 squares ontop of each other");
    }

    void level1()
    {
        if (Vector3.Distance(player.transform.position, points[pointsReached]) < 0.2)
        {
            onPoint = true;
            string speakString = ""+(pointsReached + 1);

            speechOut.Speak(speakString);
            if (pointsReached >= 6)
            {
                speechOut.Speak("Wonderfull, You finished level 1");
                level++;
                introducelvl2();
            }

            emptyPos.transform.position = points[++pointsReached];
            _lowerHandle.SwitchTo(emptyPos, 30f);
        } else
        {
            onPoint = false;
        }
    }

    async void lvl2Listener()
    {
        var input = await speechIn.Listen(new Dictionary<string, KeyCode>() {
            {"place", KeyCode.P }
        });
        //string input = task.Result;
        if (level == 2)
        {
            if (input == "place" && (Vector3.Distance(player.transform.position, lvl2targetPos) < 0.1))
            {
                speechOut.Speak("Nice you finished level 2");

                instantiatedWall = Instantiate(wall, MeHandle.transform.position, Quaternion.identity);
                instantiatedWall.transform.rotation = Quaternion.Euler(0, 0, 0); 

                // reset so not stuck in wall
                await _upperHandle.SwitchTo(playerPos, 50f);
                _upperHandle.Free();

                RenderObstacleDelay();

                level++;
                introducelvl3();
            }
        }
    }

    async void lvl3Listener()
    {
        if(lvl3Counter == 0)
        {
            string input = await speechIn.Listen(new Dictionary<string, KeyCode>() {
            {"pick up", KeyCode.U }, {"get", KeyCode.G}
            });

            if (input == "pick up" || input == "get")
            {
                Destroy(instantiatedWall);
                Task.Delay(500);
                //RenderObstacleDelay();

                lvl3Counter++;
            }

        } else
        {
            string input = await speechIn.Listen(new Dictionary<string, KeyCode>() {
            {"place", KeyCode.P }
            });

            if (input == "place" && lvl3once1)
            {
                instantiatedWall = Instantiate(wall, MeHandle.transform.position, Quaternion.identity);
                instantiatedWall.transform.rotation = Quaternion.Euler(0,0,0);

                lvl3once1 = false;

                // reset so not stuck in wall
                await _upperHandle.SwitchTo(playerPos, 50f);
                _upperHandle.Free();

                await Task.Delay(1000);
                RenderObstacleDelay();

                level++;
                introducelvl4();
            }

        }
        

    }

    async void level4()
    {
        if (lvl4open)
        {
            lvl4open = false;
            string input = await speechIn.Listen(new Dictionary<string, KeyCode>() {
            {"place", KeyCode.P }
            });

            if (input == "place" && placedSquares < 3)
            {
                instantiatedWall = Instantiate(wall, MeHandle.transform.position, Quaternion.identity);
                instantiatedWall.transform.rotation = Quaternion.Euler(0, 0, 0);

                // reset so not stuck in wall
                await _upperHandle.MoveToPosition(instantiatedWall.transform.position + new Vector3(0, 0, 1), 50f);
                _upperHandle.Free();

                await Task.Delay(1000);
                RenderObstacleDelay();

                
                placedSquares++;
                lvl4open = true;
                if (placedSquares >= 3)
                {
                    speechOut.Speak("Great you finished level 4");
                }
            }
        }
    }

    void level2()
    {
        if (Vector3.Distance(player.transform.position, lvl2targetPos) < 0.1)
        {
            if (lvl2once)
            {
                speechOut.Speak("Now say 'place' to place the game object");
                lvl2once = !lvl2once;
            }
        }
    }

    void level3()
    {
        lvl3Listener();
    }

    void Update()
    {
        dummy.transform.position = player.transform.position;
        switch (level)
        {
            case 1:
                level1();
                break;
            case 2:
                level2();       
                break;
            case 3:
                level3();
                break;
            case 4:
                level4();
                break;
        }
    }

    async Task StartGame()
    {
        Instantiate(player, playerPos.transform);
        Instantiate(empty, emptyPos.transform);

        RenderObstacleDelay();

        introducelvl1();
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
