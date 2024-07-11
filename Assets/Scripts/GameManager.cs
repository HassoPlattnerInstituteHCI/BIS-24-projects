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
    public GameObject ball_red_prefab;
    public GameObject ball_blue_prefab;
    public GameObject target_prefab;
    public GameObject shooting_red_prefab;

    PantoCollider[] pantoColliders;
    
    // Start is called before the first frame update

    private void Awake()
    {
        _speechOut = new SpeechOut();
    }

    void Start()
    {
        _upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        _lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        // TODO 1: remove this comment-out
        Introduction();
    }

    void update()
    {
        //Debug.Log(target.transform.position);
    }
    
    async void Introduction()
    {
        //Level level = GetComponent<Level>();
        //await level.PlayIntroduction(0.2f, 3000);
        //await Task.Delay(1000);
        
        // TODO 2:
        await StartGame();
    }

    async Task StartGame()
    {
        await Task.Delay(1000);
        await  _upperHandle.SwitchTo(GameObject.Find("Target"), 200f);
        await  _lowerHandle.SwitchTo(GameObject.Find("Target"), 200f);
        _upperHandle.Free();
        //_lowerHandle.Free();
        GameObject ball1 = Instantiate(ball_blue_prefab, new Vector3((float)-2.75, 0f, -6f), Quaternion.identity);
        Instantiate(ball_blue_prefab, new Vector3((float)-1.15, 0f, -6f), Quaternion.identity);
        Instantiate(ball_red_prefab, new Vector3((float)-0.35, 0f, -6f), Quaternion.identity);
        Instantiate(ball_red_prefab, new Vector3((float)1.25, 0f, -6f), Quaternion.identity);
        Instantiate(ball_red_prefab, new Vector3((float)2.05, 0f, -6f), Quaternion.identity);
        await RenderObstacle();
        await Task.Delay(3000);
        _speechOut.Speak("Here are the balls. Destroy them!");
        await  _upperHandle.SwitchTo(ball1, 100f);
        _upperHandle.Free();
       // _speechOut.Speak("You've got a red ball. Pull this handle to aim.");

        int obstacle_count = 5, red = 3, blue = 2;
        while(obstacle_count>0){
            //await _speechOut.Speak(obstacle_count.ToString() + " balls left.");
            Instantiate(shooting_red_prefab, GameObject.Find("Target").transform.position, Quaternion.identity);
            float last_rotation = _lowerHandle.GetRotation();
            /*GameObject target = Instantiate(target_prefab, new Vector3(2f, 0f, -6f), Quaternion.identity);
            _lowerHandle.SwitchTo(target, 200f);*/
            await _speechOut.Speak("we moved to target");
            while(Math.Abs(last_rotation-_lowerHandle.GetRotation()) < 0.5){//versch. Werte testen
                last_rotation = _lowerHandle.GetRotation();//Nutzer hat noch nicht geschossen
                Task.Delay(1500);
            }
            //Ball wird abgeschossen
            await _speechOut.Speak("shooting");
            Vector3 pos = _lowerHandle.GetPosition();
            GameObject target = Instantiate(target_prefab, new Vector3(-pos.x, 0f, -6f), Quaternion.identity);
            _lowerHandle.SwitchTo(target, 200f);
            
            
        }
        


    }

    /*async void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("block2")) {
            //soundEffects.PlayPaddleClip();
            _speechOut.Speak("Yay, you completed the first level!");
            
        }
    }*/

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
