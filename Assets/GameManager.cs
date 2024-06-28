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

    public Player player;
    public FruitSpawner fruitSpawner;

    PantoHandle handle;
    bool handlePrepared = false;

    void spawnFruit() {
        fruitSpawner.spawnFruit();
        manageHandle();
    }

    async void manageHandle() {
        int fruitCount = fruitSpawner.transform.childCount;
        await handle.SwitchTo(fruitSpawner.transform.GetChild(fruitCount-1).gameObject, newSpeed: 4f);
        
    }

    async void level1() {
        //await handle.MoveToPosition(fruitSpawner.transform.position, newSpeed: 0.1f ,shouldFreeHandle: false);
        await handle.MoveToPosition(fruitSpawner.spawnPosition, newSpeed: 0.5f, shouldFreeHandle: true);
        //await handle.SwitchTo(fruitSpawner.gameObject, newSpeed: 0.5f);
        //handle.Free();
        audioSource.PlayOneShot(sliceTheFruit);
        Invoke("spawnFruit", 3.0f);
    }

    void Start()
    {
        handle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        level1();
    }


    void playDelayed(AudioClip clip, float delay) {
            audioSource.clip = clip;
            audioSource.PlayDelayed(delay);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getScore() == 1) {
            playDelayed(level1Complete, 1.0f);
            player.resetScore();
        }   
    }
}
