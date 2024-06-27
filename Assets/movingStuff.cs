using UnityEngine;
using DualPantoToolkit;
using System.Threading.Tasks;


public class movingStuff : MonoBehaviour
{
    public GameObject p1;
    

    PantoHandle lowerHandle;
    PantoHandle upperHandle;
    Vector3 pos1 = new Vector3(-2.61999989f,2.95726109f,-10.96f);
    Vector3 pos2 = new Vector3(1.65999997f,2.95726109f,-10.96f);
    Vector3 pos3 = new Vector3(-2.48000002f,1f,-6.07999992f);
    Vector3 pos4 = new Vector3(0.870000005f,2.95726109f,-10.1300001f);
    Vector3 pos5 = new Vector3(0.870000005f,2.95726109f,-8.01000023f);
    Vector3 positionUpper = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 offset;
    Vector3 initialTarget = new Vector3(-2.48000002f,1f,-3.07999992f);
    float difference = 1.0f;
    int currPos = 1;
    float speed = 50f;
    float diffx;
    float diffz;
    // Start is called before the first frame update
    void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        lowerHandle.SwitchTo(p1, speed);
    }

    void FixedUpdate() {
        positionUpper = upperHandle.HandlePosition(positionUpper);
    }

    // Update is called once per frame
    async void Update()
    {

        // float diffx = (positionUpper.x - p1.transform.position.x);
        // float diffz = (positionUpper.z - p1.transform.position.z);

         Debug.Log(diffx);
         Debug.Log(diffz);
        // if (diffx < difference && diffz < difference) Debug.Log("it handle reached");

        switch (currPos) {
            case 1:
                    
                diffx = (positionUpper.x - pos1.x);
                diffz = (positionUpper.z - pos1.z);
                p1.transform.position = pos1;
                if (Mathf.Abs(diffx) < difference && Mathf.Abs(diffz) < difference) {lowerHandle.SwitchTo(p1, speed); currPos++;}
                break;
            case 2:
            diffx = (positionUpper.x - pos2.x);
                diffz = (positionUpper.z - pos2.z);
                p1.transform.position = pos2;
                if (Mathf.Abs(diffx) < difference && Mathf.Abs(diffz) < difference) {lowerHandle.SwitchTo(p1, speed); currPos++;}
                break;
            case 3:
            diffx = (positionUpper.x - pos3.x);
                diffz = (positionUpper.z - pos3.z);
                p1.transform.position = pos3;
                if (Mathf.Abs(diffx) < difference && Mathf.Abs(diffz) < difference) {lowerHandle.SwitchTo(p1, speed); currPos++;}
                break;
            case 4:
            diffx = (positionUpper.x - pos4.x);
                diffz = (positionUpper.z - pos4.z);
                p1.transform.position = pos4;
                if (Mathf.Abs(diffx) < difference && Mathf.Abs(diffz) < difference) {lowerHandle.SwitchTo(p1, speed); currPos++;}
                break;
            case 5:
            diffx = (positionUpper.x - pos5.x);
                diffz = (positionUpper.z - pos5.z);
                p1.transform.position = pos5;
                if (Mathf.Abs(diffx) < difference && Mathf.Abs(diffz) < difference) {lowerHandle.SwitchTo(p1, speed); currPos++;}
                break;
            default:
                break;
        }


        
    }
}
