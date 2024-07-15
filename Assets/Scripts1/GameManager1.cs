using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class GameManager1 : MonoBehaviour
{

    public GameObject fieldPrefab;
    private int level = -1;
    private GameObject[,] fields;
    
    private bool debug = false;


    private int startX = -4;
    private int startZ = -14;
    private int fieldLength = 8;
    private int size = 2;
    private float fieldSize;
    
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    private bool started = false;
    private bool notSpeaking = true;
    private string[] colors = {"Erase","Black","White","Red","Green","Blue","Orange","Yellow","Pink","Purple","Lightgreen","Lightblue","Darkgreen","Darkblue","Turquoise"};
    private float[,] colorsRGB = {{0,0,0},{0,0,0},{1,1,1},{1,0,0},{0,0.6f,0},{0,0,1},{1,0.474f,0},{1,1,0},{1,0.753f,0.796f},{0.4f,0.2f,0.6f},{0,1,0},{0.678f,0.847f,0.902f},{0,0.4f,0},{0,0,0.545f},{0.25f,0.88f,0.88f}};
    private int colorSet = 1;
    private string[] sizes = {"One","Two","Three","Four","Five","Six","Seven","Eight","Nine","Ten"};
    private string[] levels = {"One","Two","Three","Four"/*,"Five"*/};

    private bool massSelecting = false;

    private int[] me_HandlePos = {0,0};
    
    private SpeechOut _speechOut;
    private SpeechIn _speechIn;
    private bool level4Intro = true;

    GameObject go;

    private float rotation = 0;
    
    PantoCollider[] pantoColliders;
    
    // Start is called before the first frame update

    private void Awake()
    {
        _speechOut = new SpeechOut();
        _speechIn = new SpeechIn(onRecognized);
        go = new GameObject();
    }

    void Start()
    {
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();
        
        // TODO 1: remove this comment-out
        levelSelection();
    }
    
    async void Update() {
        if(started) {
            int x = Convert.ToInt32( Math.Floor( (_upperHandle.GetPosition().x-startX)/fieldSize ) );
            int z = Convert.ToInt32( Math.Floor( (_upperHandle.GetPosition().z-startZ)/fieldSize ) );
            if(x<0) x=0;
            if(x>=size) x = size-1;
            if(z<0) z=0;
            if(z>=size) z = size-1;

            // Debug.Log(x + "x" + z);

            if(x!=me_HandlePos[0]||z!=me_HandlePos[1]) {
                me_HandlePos[0] = x;
                me_HandlePos[1] = z;

                if(massSelecting&&level>4) {
                    Field fieldScript = (Field) fields[x,z].GetComponent<Field>();
                    fieldScript.setSelected(true);
                }
                moveLowerHandle();
            }

            if(notSpeaking) {

                if(rotation-5>_upperHandle.GetRotation()) { //Upper Handle Right
                    notSpeaking=false;

                    if(massSelecting&&level>4) {
                        for(int x1 = 0; x1 < size; x1++) {
                            for(int z1 = 0; z1 < size; z1++) {
                                Field fieldScript = (Field) fields[x1,z1].GetComponent<Field>();
                                if(fieldScript.getSelected()) {
                                    if(colorSet>0) fieldScript.changeColor(colors[colorSet],colorsRGB[colorSet,0],colorsRGB[colorSet,1],colorsRGB[colorSet,2],1f);
                                    else fieldScript.changeColor("not filled",colorsRGB[colorSet,0],colorsRGB[colorSet,1],colorsRGB[colorSet,2],0f);
                                }
                            }
                        }
                        _upperHandle.Rotate(0);
                        await _speechOut.Speak("Pixels colored!");
                    }
                    else {
                        Field fieldScript = (Field) fields[x,z].GetComponent<Field>();
                        if(colorSet>0) fieldScript.changeColor(colors[colorSet],colorsRGB[colorSet,0],colorsRGB[colorSet,1],colorsRGB[colorSet,2],1f);
                        else fieldScript.changeColor("not filled",colorsRGB[colorSet,0],colorsRGB[colorSet,1],colorsRGB[colorSet,2],0f);
                        _upperHandle.Rotate(0);
                        await _speechOut.Speak("Pixel " + x + "x" + z + " colored!");
                    }

                    notSpeaking=true;
                }

                if(rotation+5<_upperHandle.GetRotation()&&level>2) { //Upper Handle Left
                    notSpeaking=false;

                    Field fieldScript = (Field) fields[x,z].GetComponent<Field>();
                    await _speechOut.Speak("Pixel " + x + "x" + z + " is " + fieldScript.getColor());
                    _upperHandle.Rotate(0);

                    notSpeaking=true;
                }
                /*
                if(rotation-5>_lowerHandle.GetRotation()&&level>4) { //Lower Handle Right
                    notSpeaking=false;

                    massSelecting = true;
                    for(int x1 = 0; x1 < size; x1++) {
                        for(int z1 = 0; z1 < size; z1++) {
                            Field fieldScript = (Field) fields[x1,z1].GetComponent<Field>();
                            fieldScript.setSelected(false);
                        }
                    }
                    _lowerHandle.Rotate(0);
                    _speechOut.Speak("Mass selecting activated");

                    notSpeaking=true;
                }

                if(rotation+5<_upperHandle.GetRotation()&&level>4) { //Lower Handle Left
                    notSpeaking=false;

                    massSelecting = false;
                    _lowerHandle.Rotate(0);
                    _speechOut.Speak("Mass selecting deactivated");

                    notSpeaking=true;
                }*/

            }
            rotation = _upperHandle.GetRotation();
            Debug.Log(rotation);
            
        }
    }
    async void levelSelection() {
        if(debug) {
            level = 4;
            Introduction();
        } else {
            await _speechOut.Speak("Choose level.");
            _speechIn.Listen(levels);
        }
    }
    async void Introduction()
    {
        if(debug) {
            size = 3;
        }
        else {
            if(level>3) {
               if(level == 4) await _speechOut.Speak("Before starting drawing you can now choose how big the Canvas should be.",0.7f);
               if(level==5) await _speechOut.Speak("Now you can rotate the it-Handle to the right to enter the selection mode. Then you can hover over multiple pixels, and then rotate the me-handle to color them all at once. To exit selection mode rotate it-handle to the left.",0.7f);
               await _speechOut.Speak("How big should the Canvas be?");
                _speechIn.Listen(sizes);
                return;
            }
        }

        await StartGame();
    }

    async Task StartGame()
    {
        if(level==2) await _speechOut.Speak("Before setting a pixel, you can say a colour or say erase.",0.7f);
        if(level == 3) await _speechOut.Speak("When on a pixel, rotate me handle left to get the colour of the pixel",0.7f);

        fieldSize = fieldLength/(float) size;


        fields = new GameObject[size,size];
        for(int x = 0; x < size; x++) {
            for(int z = 0; z < size; z++) {
                fields[x,z] = Instantiate(fieldPrefab);
                fields[x,z].transform.position = new Vector3( startX+fieldSize*x + fieldSize/2.0f ,-1.0f, startZ+fieldSize*z+fieldSize/2.0f);
                fields[x,z].transform.localScale = new Vector3(fieldSize,1.0f,fieldSize);
            }
        }
        if(level==1) await _speechOut.Speak("Move the me-handle to move on the grid-shaped canvas. The it-handle shows your current position on the canvas. Starting at Position 0x0",0.7f);
        await _upperHandle.MoveToPosition(new Vector3(startX+fieldSize/2,0,startZ+fieldSize/2), 1.0f, false);
        await _lowerHandle.SwitchTo(go, 50.0f);
        await moveLowerHandle();
        await RenderObstacle();
        if(level>1) {
            _speechIn.Listen(colors);
        }

        _upperHandle.Free();
        rotation = _upperHandle.GetRotation();
        started = true;
    }
    async Task moveLowerHandle() {
        
        // _lowerHandle.Free();
        // await _lowerHandle.MoveToPosition(new Vector3(startX+me_HandlePos[0]*fieldSize+fieldSize/2,0.0f,startZ+me_HandlePos[1]*fieldSize+fieldSize/2), 4.0f, false);

        go.transform.position = new Vector3(startX+me_HandlePos[0]*fieldSize+fieldSize/2,0.0f,startZ+me_HandlePos[1]*fieldSize+fieldSize/2);

        _speechOut.Stop(false);
        await _speechOut.Speak("Moved to Pixel Row:" + me_HandlePos[0] + " Column:" + me_HandlePos[1]);
    }

    async void onRecognized(string message) {
        if(level==-1) {
            for(int i = 0; i < levels.Length; i++) {
                if(message == levels[i]) level = i+1;
            }
            Introduction();
        }
        else if(level>3&&level4Intro) {
            for(int i = 0; i < sizes.Length; i++) {
                if(message == sizes[i]) size = i+1;
            }
            await _speechOut.Speak("Size set to " + message);
            StartGame();
            level4Intro = false;
        } else {
            for(int i = 0; i < colors.Length; i++) {
                if(message == colors[i]) colorSet = i;
            }
            await _speechOut.Speak("Set to " + message);
            await _speechIn.Listen(colors);
        }
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

    public void OnApplicationQuit()
    {
        _speechIn.StopListening(); // [macOS] do not delete this line!
        _speechOut.Stop(); //Windows: do not remove this line.
    }
}
