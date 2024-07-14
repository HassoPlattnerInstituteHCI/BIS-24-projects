using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;

    public AudioClip sliceTheFruit;
    public AudioClip level1Complete;
    public AudioClip grabSword;

    public Player player;
    public FruitSpawner fruitSpawner;

    PantoHandle lowerHandle;
    PantoHandle upperHandle;
    bool handlePrepared = false;




    async void level1() {
        //await handle.MoveToPosition(fruitSpawner.transform.position, newSpeed: 0.1f ,shouldFreeHandle: false);
        audioSource.PlayOneShot(grabSword);
        await upperHandle.MoveToPosition(new Vector3(-6, 0, -8), newSpeed: 0.7f, shouldFreeHandle: true);
        

        await lowerHandle.MoveToPosition(fruitSpawner.spawnPosition, newSpeed: 0.7f, shouldFreeHandle: false);
        //await handle.SwitchTo(fruitSpawner.gameObject, newSpeed: 0.5f);
        //handle.Free();
        audioSource.PlayOneShot(sliceTheFruit);
        Invoke("spawnFruit", 2.0f);
    }

    void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();

        upperHandle.ApplyForce(new Vector3(0, 0, -4), 1.0f);
        level1();
    }


    void playDelayed(AudioClip clip, float delay) {
            audioSource.clip = clip;
            audioSource.PlayDelayed(delay);
    }

    // Update is called once per frame
    void Update()
    {
/*         if (player.getScore() == 1) {
            playDelayed(level1Complete, 1.0f);
            player.resetScore();
        }    */
    }
}
