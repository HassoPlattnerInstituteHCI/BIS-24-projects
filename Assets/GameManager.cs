using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Reflection;

public class GameManager : MonoBehaviour
{
    Ball ball;
    GameObject hole;
    MeCollider meCollider;
    GameObject rail;
    PantoHandle upperHandle;
    PantoHandle lowerHandle;
    SpeechOut speechOut;
    SpeechIn speechIn;
    PantoCollider[] pantoColliders;
    public AudioClip failSound;
    AudioSource audioSource;
    GameObject wallPrefab;
    GameObject sandPrefab;

    // Start is called before the first frame update
    async void Start()
    {
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        hole = GameObject.Find("Hole");
        meCollider = GameObject.Find("MeCollider").GetComponent<MeCollider>();
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        audioSource = GameObject.Find("SoundSource").GetComponent<AudioSource>();
        rail = GameObject.Find("Rail");
        wallPrefab = Resources.Load<GameObject>("Wall");
        sandPrefab = Resources.Load<GameObject>("Sand");

        speechOut = new SpeechOut();
        speechOut.SetLanguage(SpeechBase.LANGUAGE.ENGLISH); // alternatively GERMAN or JAPANESE

        speechIn = new SpeechIn(onRecognized: (string recognized) => {
            Debug.Log("Recognized: " + recognized);
        });

        Level1();
    }

    async void Level1 () {
        await upperHandle.MoveToPosition(ball.transform.position, 0.3f, true);
        createObstacles();
        await speechOut.Speak("Use this handle to explore the minigolf field. Rails can help you to find the hole.");

        await meCollider.WaitForHoleFound();
        await speechOut.Speak("You've found the whole!");
        
        Level2();
    }

    async void Level2 () {
        await lowerHandle.MoveToPosition(ball.transform.position, 0.3f, false);
        await speechOut.Speak("This is the ball.");

        await speechOut.Speak("Say 'shoot' to shoot the ball towards your upper handle position.");

        await playLevel(ball.transform.position);

        await Task.Delay(1000);
        await speechOut.Speak("Congratulations! Now you can play a full course own your own.");

        ball.ResetBallInsideHole();
        Level3();
    }

    async void Level3 () {
        speechOut.Speak("Building Level 3");
        //hole
        hole.transform.position = new Vector3(2, 1, -7);
        //rail
        rail.transform.position = new Vector3(2.21f, -4, -7.09f);
        rail.transform.localScale = new Vector3(5, 9, 0.2f);
        rail.transform.eulerAngles = new Vector3(0, 20, 0);
        //wall
        GameObject wall = Instantiate(wallPrefab, new Vector3(0, -3, -8.5f), Quaternion.Euler(0, 0, 0));
        //handles
        ball.transform.position = new Vector3(-2, 1.5f, -8);
        lowerHandle.Free();
        await lowerHandle.MoveToPosition(ball.transform.position, 0.3f, false);
        upperHandle.Free();
        await upperHandle.MoveToPosition(ball.transform.position, 0.3f, true);
        await speechOut.Speak("let's go!");

        await playLevel(ball.transform.position);

        await Task.Delay(1000);
        await speechOut.Speak("Congratulations! You've completed the level!");

        ball.ResetBallInsideHole();
        Destroy(wall);
        Level4();
    }

    async void Level4 () {
        speechOut.Speak("Building Level 4. Watch out for the sand. If the ball stops on the sand you will have to start again.");
        //hole
        hole.transform.position = new Vector3(2, 1, -7);
        //rail
        rail.transform.position = new Vector3(2.21f, -4, -7.09f);
        rail.transform.localScale = new Vector3(5, 9, 0.2f);
        rail.transform.eulerAngles = new Vector3(0, 20, 0);
        //wall
        GameObject wall = Instantiate(wallPrefab, new Vector3(0, -3, -7f), Quaternion.Euler(0, 0, 0));
        //handles
        ball.transform.position = new Vector3(-2, 1.5f, -8);
        lowerHandle.Free();
        await lowerHandle.MoveToPosition(ball.transform.position, 0.3f, false);
        upperHandle.Free();
        await upperHandle.MoveToPosition(ball.transform.position, 0.3f, true);
        //sand
        GameObject sand1 = Instantiate(sandPrefab, new Vector3(-3.3f, 0, -11), Quaternion.Euler(0, -30, 0));
        GameObject sand2 = Instantiate(sandPrefab, new Vector3(2.3f, 0, -14), Quaternion.Euler(0, 30, 0));
        
        await speechOut.Speak("let's go!");
        await playLevel(ball.transform.position);

        await Task.Delay(1000);
        await speechOut.Speak("Congratulations! You've completed the level!");

        ball.ResetBallInsideHole();
        Destroy(wall);
        Destroy(sand1);
        Destroy(sand2);

        Level5();
    }

    async void Level5 () {
        speechOut.Speak("Building Level 5");
        //hole
        hole.transform.position = new Vector3(-3.5f, 1, -10);
        //rail
        rail.transform.position = new Vector3(-3.24f, -4, -10f);
        rail.transform.localScale = new Vector3(5, 12, 0.2f);
        rail.transform.eulerAngles = new Vector3(0, -20, 0);
        //wall
        GameObject wall1 = Instantiate(wallPrefab, new Vector3(-1, -3, -7), Quaternion.Euler(0, -40, 0));
        GameObject wall2 = Instantiate(wallPrefab, new Vector3(-1, -3, -15), Quaternion.Euler(0, 20, 0));
        //handles
        ball.transform.position = new Vector3(2, 1.5f, -7);
        lowerHandle.Free();
        await lowerHandle.MoveToPosition(ball.transform.position, 0.3f, false);
        upperHandle.Free();
        await upperHandle.MoveToPosition(ball.transform.position, 0.3f, true);
        //sand
        GameObject sand = Instantiate(sandPrefab, new Vector3(2, 0, -14), Quaternion.Euler(0, 90, 0));
        


        await speechOut.Speak("let's go!");
        await playLevel(ball.transform.position);

        await Task.Delay(1000);
        await speechOut.Speak("Congratulations! You've completed the level!");

        ball.ResetBallInsideHole();
        Destroy(wall1);
        Destroy(wall2);
        Destroy(sand);

        Level6();
    }

    async void Level6() {
        speechOut.Speak("Building Level 6");
        //hole
        hole.transform.position = new Vector3(-3, 1, -8.5f);
        //rail
        rail.transform.position = new Vector3(-3.24f, -4, -10f);
        rail.transform.localScale = new Vector3(5, 5, 0.2f);
        rail.transform.eulerAngles = new Vector3(0, -20, 0);
        //wall
        GameObject wall1 = Instantiate(wallPrefab, new Vector3(-2, -3, -6), Quaternion.Euler(0, 0, 0));
        GameObject wall2 = Instantiate(wallPrefab, new Vector3(2, -3, -6), Quaternion.Euler(0, 0, 0));
        GameObject wall3 = Instantiate(wallPrefab, new Vector3(0, -3, -13), Quaternion.Euler(0, 0, 0));
        //handles
        ball.transform.position = new Vector3(3, 2, -8.2f);
        lowerHandle.Free();
        await lowerHandle.MoveToPosition(ball.transform.position, 0.3f, false);
        upperHandle.Free();
        await upperHandle.MoveToPosition(ball.transform.position, 0.3f, true);
        //sand
        //GameObject sand1 = Instantiate(sandPrefab, new Vector3(-2, 0, -14), Quaternion.Euler(0, 90, 0));
        GameObject sand2 = Instantiate(sandPrefab, new Vector3(2, 0, -14), Quaternion.Euler(0, 90, 0));


        await speechOut.Speak("let's go!");
        await playLevel(ball.transform.position);

        await Task.Delay(1000);
        await speechOut.Speak("Congratulations! You've completed the level!");

        ball.ResetBallInsideHole();
        Destroy(wall1);
        Destroy(wall2);
        Destroy(wall3);
        //Destroy(sand1);
        Destroy(sand2);
        Done();
    }

    async void Done()
    {
        await speechOut.Speak("Amazing! You've completed all levels!");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (PantoCollider collider in pantoColliders)
            {
                collider.Enable();
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (PantoCollider collider in pantoColliders)
            {
                collider.Disable();
            }
        }*/
    }

    private async Task playLevel(Vector3 resetBallPosition) {
        bool isBallInsideHole = false;
        bool isBallOnSand = false;

        do {
            if (isBallOnSand) {
                audioSource.PlayOneShot(failSound);
                ball.transform.position = resetBallPosition;
                await Task.Delay(2000);
                speechOut.Speak("You hit the sand. Try again");
                lowerHandle.Free();
                await lowerHandle.MoveToPosition(ball.transform.position, 0.3f, false);
                isBallOnSand = false;
            }
            while (!isBallInsideHole && !isBallOnSand)
            {
                string result = await speechIn.Listen(new Dictionary<string, KeyCode>() {
                    { "shoot", KeyCode.S }
                });

                lowerHandle.Free();
                await lowerHandle.SwitchTo(ball.gameObject, 10f);

                await ball.Shoot(meCollider.transform.position);
                await lowerHandle.MoveToPosition(ball.transform.position, 0.3f, false);

                isBallInsideHole = ball.CheckBallInsideHole();
                isBallOnSand = ball.CheckBallOnSand();
            }
        } while (isBallOnSand);

        return;
    }

    private void createObstacles()
    {
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.CreateObstacle();
            // collider.Enable();
        }
    }
}
