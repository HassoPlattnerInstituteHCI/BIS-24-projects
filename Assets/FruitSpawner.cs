using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;
    public AudioSource spawnSound;
    public float spawnFrequency = 5;

    private float startTime;
    private float lastSpawn;

    public bool spawnFreuqntly = false;
    public Vector3 spawnPosition;   

    public Player player;

    public Vector3 minPos;
    public Vector3 maxPos;

    PantoHandle lowerHandle;
    public void spawnFruit() {
        float speed;
        Debug.Log(transform.position);
        
        Vector3 spawnPos = new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y), Random.Range(minPos.z, maxPos.z));
        float side = Mathf.Sign(spawnPos.x);
       
        //Instantiate(fruitPrefab, transform.position, new Quaternion(0, 0, 0, 0), transform);
        GameObject fruit = Instantiate(fruitPrefab, spawnPos, new Quaternion(0, 0, 0, 0), transform);
    
        Vector3 direction = new Vector3(-side, 0, 1).normalized * (2 - 10/(player.getScore()+15)) / 1.2f;
        

        fruit.GetComponent<FruitPhysics>().direction = direction;
        spawnSound.Play();
        manageHandle();
    }

    async void manageHandle() {
        lowerHandle.Free();
        int fruitCount = transform.childCount;
        await lowerHandle.SwitchTo(transform.GetChild(fruitCount-1).gameObject, newSpeed: 8f);
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;  
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawn > spawnFrequency && spawnFreuqntly) {
            lastSpawn = Time.time;
            spawnFruit();
        } 
    }
}
