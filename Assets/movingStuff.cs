using UnityEngine;
using DualPantoToolkit;
using System.Threading.Tasks;

public class movingStuff : MonoBehaviour
{
    public GameObject p1;
    public AudioClip failedSound;
    public AudioClip song;
    public AudioClip levelIntroducer;
    //public AudioSource audioSource;
    public AudioSource twinkle;
    const float BLINDTIME = 2.45f;

    PantoHandle lowerHandle;
    PantoHandle upperHandle;
    
    
    
    
    Vector3[] positionArray = new [] {  new Vector3(-2.5f,0f,-11f), 
                                        new Vector3(1.5f,0f,-11f),
                                        new Vector3(-2.5f,0f,-6.0f),
                                        new Vector3(1f,0f,-11f),
                                        new Vector3(0.5f,0f,-9f),
                                        new Vector3(-0.5f,0f,-9.5f),
                                        new Vector3(-2.5f,0f,-7f),
                                        new Vector3(-1.5f,0f,-9f),
                                        new Vector3(0.5f,0f,-11f),
                                        new Vector3(-1.5f,0f,-9f),
                                        new Vector3(1.5f,0f,-7f),
                                        new Vector3(-1.7f,0f,-7f),
                                        new Vector3(0f,0f,-8f)};


    Vector3 positionUpper = new Vector3(0.0f, 0.0f, 0.0f);

    float targetTime = 2.8f;

    float difference = 1.0f;
    int currPos = 0;
    float speed = 100f;
    float diffx;
    float diffz;
    bool madeit = false;
    bool canStart = false;

    // Start is called before the first frame update
    void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        p1.transform.position = positionArray[currPos];
        lowerHandle.SwitchTo(p1, speed);
        //audioSource = GetComponent<AudioSource>();
        twinkle = GetComponent<AudioSource>();
        //audioSource.Play();
        twinkle.PlayOneShot(levelIntroducer, 1.0f);
    }

    void GameStart(){
        canStart = true;
        twinkle.PlayOneShot(song, 1.0f);
        targetTime = 2.8f;
    }

    void FixedUpdate() {
        positionUpper = upperHandle.HandlePosition(positionUpper);
    }

    // Update is called once per frame
    async void Update() //async needed???
    {
        //audioSource.Play();
        if(!canStart && Input.GetKeyDown(KeyCode.P)){
            GameStart();
        }
        if(canStart){
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f){
            TimerEnded();
        }
        if(currPos > 12) return;
        diffx = (positionUpper.x - positionArray[currPos].x);
        diffz = (positionUpper.z - positionArray[currPos].z);
        if (Mathf.Abs(diffx) < difference && Mathf.Abs(diffz) < difference && Input.GetKeyDown(KeyCode.Space)) {
            madeit = true;
        }
        Debug.Log(diffx);
        Debug.Log(diffz);}
    }

    void TimerEnded(){
        Debug.Log("TIMERTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDEDTIMER ENDED ENDED");
        if (!madeit) {
            //do some fail shit
            //audioSource.Play();
            twinkle.PlayOneShot(failedSound, 1.0f);
            //AudioClip.PlayOneShot(failedSound, 1F);
            Debug.Log("FAILED FAILED FAILED");
        }
        madeit = false;
        NextPos();
        targetTime = BLINDTIME;
    }

    void NextPos(){
        currPos++;
        p1.transform.position = positionArray[currPos];
                // lowerHandle.SwitchTo(p1, speed); //re SwitchTo otherwise tracking is whack
    }
}