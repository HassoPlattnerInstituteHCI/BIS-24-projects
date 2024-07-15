using System;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEngine;
using SpeechIO;

public class PathPlanningGameManager : MonoBehaviour
{

    public GameObject panto;
    public Transform playerSpawn;
    public GameObject goalPrefab;
    public ColliderActivator lowerCollider;
    public List<GameObject> destinations;

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
        destinations = new List<GameObject>();
        foreach (var pathObject in pathObjects)
        {
            destinations.Add(Instantiate(goalPrefab, pathObject.position.position, Quaternion.identity));
        }
        _inputManager = new InputManager(RunningInputCallback, destinations, StartGame, uiInput, goalPrefab, pathObjects, _so);
        
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
            _upperHandle.Free();
            _lowerHandle.Free();
            _inputManager.MainMenu();
            return;
        }
        
        await _upperHandle.SwitchTo(nextDestination, 10.0f);
    }
    
    async void StartGame(Dictionary<string, Tuple<PathObject, GameObject>> selectedPathObjects)
    {
        // Tuple<PathObject, GameObject>[] pathObjects = selectedPathObjects.Values.ToArray();
        
        await _so.Speak("You started the application.");
        await _lowerHandle.SwitchTo(_inputManager.Start, 3f);
        await _so.Speak("The Me-Handle is now at the starting location.");
        await _upperHandle.SwitchTo(pathObjects[0].position.gameObject, 10f);
        await _so.Speak("The It-Handle is now at the first destination.");
        _lowerHandle.Free();
        await _so.Speak("Explore the area and move to all destinations.");
        _inputManager.RunningMenu(RunningInputCallback);
    }

    async void Introduction()
    {
        // await _so.Speak("Welcome to the HPI path planning application. You can use it to explore the HPI grounds and find a route to your locations.");
        _so.Speak("You can use Voice Input to select a path of destinations.");
        
        await _upperHandle.SwitchTo(playerSpawn.gameObject, 3.0f);
        _upperHandle.Free();
        await _lowerHandle.SwitchTo(playerSpawn.gameObject, 3.0f);
        _lowerHandle.Free();
        
        _inputManager.MainMenu();
    }
}

