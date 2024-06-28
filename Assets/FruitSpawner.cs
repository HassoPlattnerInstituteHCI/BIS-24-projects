using System;
using System.Collections;
using System.Collections.Generic;
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

    public void spawnFruit() {
        Debug.Log(transform.position);
        //Instantiate(fruitPrefab, transform.position, new Quaternion(0, 0, 0, 0), transform);
        Instantiate(fruitPrefab, spawnPosition, new Quaternion(0, 0, 0, 0), transform);
        spawnSound.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;   
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
