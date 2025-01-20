using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public float rotU;
    public float rotL;
    public string selectedObjectName;
    private ObjectHandler objectHandler;
    private int selectedObjectId = 0;
    private string[] objectNames = {"Selector", "Deletor","Connector", "Textbox", "Circle", "Text"}; // todo //move und zoom ist für Blinde eher ungeeignet/verwirrend
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;
    public float upperZeroRotation;
    public float lowerZeroRotation;
    public bool upperTurned;
    public bool lowerTurned;

    public bool objectsSelectable = false;
    public bool objectsPlaceable = false;
    public bool removeToolActivated = false;

    private SpeechOut speechOut;
    public bool soundLocked = false;
    public SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        speechOut = new SpeechOut();
        objectHandler = GameObject.FindObjectsOfType<ObjectHandler>()[0];
        soundManager = GameObject.FindObjectsOfType<SoundManager>()[0];
        Debug.Log(GameObject.FindObjectsOfType<SoundManager>().Length);

        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();

        _upperHandle.Rotate(0);
        _lowerHandle.Rotate(0);

        selectedObjectName = objectNames[selectedObjectId];

        Invoke("SetInitialHandleRotation", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        rotU = _upperHandle.GetRotation();
        rotL = _lowerHandle.GetRotation();

        //object placement - bug fix
        if(!upperTurned && Mathf.Abs(upperZeroRotation-rotU) >= 30){
            upperTurned = true;
            switch (selectedObjectName){
                case "Textbox": 
                    objectHandler.placeBox(); break;
                case "Circle":
                    objectHandler.placeCircle(); break;
                case "Selector":
                    objectHandler.editBox(); break;
                case "Deletor":
                    objectHandler.destroyHoveredObject();break;
                case "Connector":
                    objectHandler.connectObjects();break;


            }
        }

        //object selection
        if(!lowerTurned && Mathf.Abs(lowerZeroRotation-rotL) >= 30){
            upperTurned = true;
            selectedObjectId++;
            objectHandler.resetPlacementStarted();
            if(selectedObjectId == objectNames.Length){
                selectedObjectId = 0;
            }
            selectedObjectName = objectNames[selectedObjectId]
            speechOut.Speak(selectedObjectName + " ausgewählt.");
        }


        if (Mathf.Abs(lowerZeroRotation-rotL) <= 5)
        {
            lowerTurned = false;
        }

        if (Mathf.Abs(upperZeroRotation-rotU) <= 5)
        {
            upperTurned = false;
        }
    }
}
