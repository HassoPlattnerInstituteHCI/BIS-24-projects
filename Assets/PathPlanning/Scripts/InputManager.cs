using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using SpeechIO;
using UnityEngine.UI;

public enum InputEvent
{
    Back,
    Help,
    AddDestination,
    RemoveDestination,
    NextDestination,
    PreviousDestination,
    Start,
    Quit,
    Mikadoplatz,
    MainBuilding,
    LectureHalls,
    HouseA,
    HouseB,
    HouseC,
    SetStart,
    House144,
    HouseK,
    Library
}

public struct MenuOption
{
    public InputEvent EventName;
    public Action Action;

    public MenuOption(InputEvent eventName, Action action)
    {
        EventName = eventName;
        Action = action;
    }
}

public class InputManager
{
    private TaskCompletionSource<string> _buttonTaskCompletionSource;
    
    private SpeechIn _si;
    private SpeechOut _so;

    private Dictionary<string, MenuOption> _mainMenu;
    private Dictionary<string, MenuOption> _addLocationMenu;
    private Dictionary<string, MenuOption> _setStartMenu;


    private List<PathObject> _pathObjects;
    public GameObject Start ;
    private int _currentDestination = 0;
    private List<GameObject> _selectedPathObjects;
    private Action<Dictionary<string, Tuple<PathObject, GameObject>>> _startAction;

    private GameObject _uiMainMenu;
    private GameObject _uiLocations;
    private GameObject _uiRunningMenu;

    private GameObject _goalPrefab;
    
    // Start is called before the first frame update
    public InputManager(Action<GameObject> callback, List<GameObject> destinations, Action<Dictionary<string, Tuple<PathObject, GameObject>>> startAction, GameObject uiInput, GameObject goalPrefab, List<PathObject> pathObjects, SpeechOut so)
    {
        _startAction = startAction;
        _so = so;
        _si = new SpeechIn((string msg) => { });
        _goalPrefab = goalPrefab;
        _selectedPathObjects = new List<GameObject>(destinations);
        
        _uiMainMenu = uiInput.transform.Find("MainMenu").gameObject;
        _uiLocations = uiInput.transform.Find("Locations").gameObject;
        _uiRunningMenu = uiInput.transform.Find("RunningMenu").gameObject;
        
        _uiRunningMenu.transform.Find("Next").GetComponent<Button>().onClick.AddListener(() =>
        {
            _currentDestination = (_currentDestination + 1) % _selectedPathObjects.Count;
            callback(_selectedPathObjects[_currentDestination]);
        });
        
        _uiRunningMenu.transform.Find("Previous").GetComponent<Button>().onClick.AddListener(() =>
        {
            _currentDestination = (_currentDestination - 1) % _selectedPathObjects.Count;
            if (_currentDestination < 0)
            {
                _currentDestination += _selectedPathObjects.Count;
            }
            callback(_selectedPathObjects[_currentDestination]);
        });
        
        foreach (var buttonGroup in new [] { _uiMainMenu, _uiLocations, _uiRunningMenu })
        {
            foreach (var button in buttonGroup.GetComponentsInChildren<Button>())
            {
                Debug.Log("Added Button " + button.name);
                button.onClick.AddListener(() => OnButtonClick(button.name));
            }

        }
        
        // Main Menu Options
        _mainMenu = new Dictionary<string, MenuOption>
        {
            { "Set Start", new MenuOption(InputEvent.SetStart, SetStartMenu) },
            // { "Add Destination", new MenuOption(InputEvent.AddDestination, AddLocationMenu) },
            // { "Remove Destination", new MenuOption(InputEvent.RemoveDestination, RemoveLocationMenu) },
            { "Start", new MenuOption(InputEvent.Start, StartMenu) },
            { "Help", new MenuOption(InputEvent.Help, MainMenu) },
        };
        
        // Location Menu Options
        // _addLocationMenu = new Dictionary<string, MenuOption>();
        // foreach (var pathObject in pathObjects)
        // {
        //     _addLocationMenu[pathObject.name] = new MenuOption(pathObject.inputEvent, () =>
        //     {
        //         _selectedPathObjects[pathObject.name] = new Tuple<PathObject, GameObject>(pathObject, GameObject.Instantiate(_goalPrefab, pathObject.position.position, Quaternion.identity));
        //     });
        // }
        // _addLocationMenu.Add("Back", new MenuOption(InputEvent.Back, MainMenu));
        // _addLocationMenu.Add("Help", new MenuOption(InputEvent.Help, AddLocationMenu));
        
        // Set Start Menu Options
        _setStartMenu = new Dictionary<string, MenuOption>();
        foreach (var pathObject in pathObjects)
        {
            _setStartMenu[pathObject.name] = new MenuOption(pathObject.inputEvent, () =>
            {
                if (Start != null) GameObject.Destroy(Start);
                Start = GameObject.Instantiate(goalPrefab, pathObject.position.position, Quaternion.identity);
            });
        }
    }


    public async void MainMenu()
    {
        _uiMainMenu.SetActive(true);
        _uiLocations.SetActive(false);
        _uiRunningMenu.SetActive(false);
        
        _so.Speak("You are in the main menu.");
        MenuOption input = await GetInput(_mainMenu);
        input.Action();
        _uiMainMenu.SetActive(false);
    }

    public async void SetStartMenu()
    {
        _uiMainMenu.SetActive(false);
        _uiLocations.SetActive(true);
        _uiRunningMenu.SetActive(false);
        
        _so.Speak("You are in the menu to set your starting location.");
        MenuOption input = await GetInput(_setStartMenu);
        await _so.Speak("You set your starting location to " + input.EventName);
        input.Action();
        MainMenu();
    }

    // public async void AddLocationMenu()
    // {
    //     _uiMainMenu.SetActive(false);
    //     _uiLocations.SetActive(true);
    //     _uiRunningMenu.SetActive(false);
    //
    //     _so.Speak("You are in the menu to add a destination.");
    //     
    //     MenuOption input = await GetInput(_addLocationMenu);
    //     await _so.Speak("You added the destination " + input.EventName);
    //     input.Action();
    //     AddLocationMenu();
    // }

    // public async void RemoveLocationMenu()
    // {
    //     _uiMainMenu.SetActive(false);
    //     _uiLocations.SetActive(true);
    //     _uiRunningMenu.SetActive(false);
    //
    //     _so.Speak("You are in the menu to remove a destination.");
    //     
    //     // Remove Location Menu Options
    //     Dictionary<string, MenuOption> commands = new Dictionary<string, MenuOption>();
    //     foreach (var pathObject in _selectedPathObjects.Keys)
    //     {
    //         commands[_selectedPathObjects[pathObject].Item1.name] = new MenuOption(_selectedPathObjects[pathObject].Item1.inputEvent, () =>
    //         {
    //             GameObject.Destroy(_selectedPathObjects[pathObject].Item2);
    //             _selectedPathObjects.Remove(pathObject);
    //         });
    //     }
    //     commands.Add("Back", new MenuOption(InputEvent.Back, MainMenu));
    //     commands.Add("Help", new MenuOption(InputEvent.Help, RemoveLocationMenu));
    //     MenuOption input = await GetInput(commands);
    //     await _so.Speak("Successfully removed destination " + input.EventName);
    //     RemoveLocationMenu();
    // }
    
    public async void StartMenu()
    {
        _startAction(new Dictionary<string, Tuple<PathObject, GameObject>>());
    }

    public async void RunningMenu(Action<GameObject>  callback)
    {
        _uiMainMenu.SetActive(false);
        _uiLocations.SetActive(false);
        _uiRunningMenu.SetActive(true);

        // _so.Speak("You are in the menu to select the next goal location.");
        
        Dictionary<string, MenuOption> commands = new Dictionary<string, MenuOption>
        {
            {
                "Next", new MenuOption(InputEvent.NextDestination, () =>
                {
                    _currentDestination = (_currentDestination + 1) % _selectedPathObjects.Count;
                })
            },
            {
                "Previous", new MenuOption(InputEvent.PreviousDestination, () =>
                {
                    _currentDestination = (_currentDestination - 1) % _selectedPathObjects.Count;
                })
            },
            { "Quit", new MenuOption(InputEvent.Quit, () => { callback(null); }) },
            { "Help", new MenuOption(InputEvent.Help, () => { RunningMenu(callback); }) },
        };
        
        MenuOption input = await GetInput(commands);
        input.Action();
        callback(_selectedPathObjects[_currentDestination]);
        RunningMenu(callback);
    }

    private void OnButtonClick(string name)
    {
        _buttonTaskCompletionSource?.TrySetResult(name);
    }

    public Task<string> ButtonInput()
    {
        _buttonTaskCompletionSource = new TaskCompletionSource<string>();
        return _buttonTaskCompletionSource.Task;
    }

    public async Task<MenuOption> GetInput(Dictionary<string, MenuOption> commands)
    {
        // _so.Speak("You may say one of the following options: " + string.Join(", ", commands.Keys));
        Task<string> buttonTask = ButtonInput();
        Task<string> speechTask = _si.Listen(commands.Keys.ToArray());
        
        Task<string> completedTask = await Task.WhenAny(buttonTask, speechTask);
        _so.Stop();
        _si = new SpeechIn((string msg) => { });

        string result = await completedTask;
        return commands[result];
    }
}
