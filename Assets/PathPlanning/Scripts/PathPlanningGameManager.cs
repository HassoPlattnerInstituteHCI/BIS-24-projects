using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Serialization;
using SpeechIO;
using Task = System.Threading.Tasks.Task;

public class PathPlanningGameManager : MonoBehaviour
{

    public GameObject panto;
    public Transform playerSpawn;
    public GameObject goalPrefab;
    public ColliderActivator lowerCollider;

    public GameObject uiInput;
    
    public List<PathObject> pathObjects;
    
    private GameObject _goal;
    private GameObject _player;

    private GameObject _upperHandleObject;
    private GameObject _lowerHandleObject;
    
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    
    private SpeechOut _so;

    private InputManager _inputManager;

    
    // PantoCollider[] pantoColliders;
    
    // Start is called before the first frame update
    void Start()
    {
        _so = new SpeechOut();
        _inputManager = new InputManager(StartGame, uiInput, goalPrefab, pathObjects, _so);
        
        _upperHandle = panto.GetComponent<UpperHandle>();
        _lowerHandle = panto.GetComponent<LowerHandle>();
        
        _upperHandleObject = GameObject.Find("MeHandle");
        _lowerHandleObject = GameObject.Find("ItHandle");
        
        Introduction();
    }

    void Update()
    {
        // _lowerHandle.Freeze();
    }

    async void RunningInputCallback(GameObject nextDestination)
    {
        if (nextDestination == null)
        {
            _inputManager.MainMenu();
            return;
        }
        
        nextDestination.transform.Rotate(_upperHandle.transform.rotation.eulerAngles); // prevent handle spinning
        await _upperHandle.SwitchTo(nextDestination, 3.0f);
    }
    
    async void StartGame(Dictionary<string, Tuple<PathObject, GameObject>> selectedPathObjects)
    {
        Tuple<PathObject, GameObject>[] pathObjects = selectedPathObjects.Values.ToArray();
        
        await _so.Speak("You started the application.");
        await _lowerHandle.SwitchTo(_inputManager.Start, 3f);
        await _so.Speak("The Me-Handle is now at the starting location.");
        await _upperHandle.SwitchTo(pathObjects[0].Item2, 3f);
        await _so.Speak("The It-Handle is now at the first destination.");
        _lowerHandle.Free();
        await _so.Speak("Explore the area and move to all destinations.");
        _inputManager.RunningMenu(RunningInputCallback);
    }

    async void Introduction()
    {
        // await _so.Speak("Welcome to the HPI path planning application. You can use it to explore the HPI grounds and find a route to your locations.");
        _so.Speak("You can use Voice Input to select a path of destinations.");
        
        playerSpawn.transform.Rotate(_upperHandle.transform.rotation.eulerAngles); // prevent handle spinning
        await _upperHandle.SwitchTo(playerSpawn.gameObject, 3.0f);
        _upperHandle.Free();
        playerSpawn.transform.Rotate(_lowerHandle.transform.rotation.eulerAngles); // prevent handle spinning
        await _lowerHandle.SwitchTo(playerSpawn.gameObject, 3.0f);
        _lowerHandle.Free();
        
        _inputManager.MainMenu();
    }
}

