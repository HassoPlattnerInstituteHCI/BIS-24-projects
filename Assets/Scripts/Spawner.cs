using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject speedboostPrefab;
    private Vector3[] spawnBox = {new Vector3(-3, 0, 2), new Vector3(3, 0, -2)};

    void Start() {
        
    }

    void Update() {
        int numSpeedboosts = GameObject.FindGameObjectsWithTag("Speedboost").Length;
        if (numSpeedboosts < 1) {
            SpawnSpeedboost();
        }
    }

    void SpawnSpeedboost() {
        Vector3 spawnPosition = new Vector3(Random.Range(spawnBox[0].x, spawnBox[1].x), 0, Random.Range(spawnBox[0].z, spawnBox[1].z));
        Instantiate(speedboostPrefab, spawnPosition, Quaternion.identity);
    }
}
