using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureScript : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject player;
    public GameObject treasure;
    public float maxDistance = 20f;
    public float minPitch = 0.5f;
    public float maxPitch = 3f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            Debug.LogError("Keine Audio Source gefunden");
            return;
        }
        if (player == null) {
            Debug.LogError("Player nicht gefunden");
            return;
        }
        if (treasure == null) {
            Debug.LogError("Treasure nicht gefunden");
            return;
        }
        audioSource.loop = true;
        audioSource.Play();
        updateAudioProperties();
    }

    // Update is called once per frame
    void Update()
    {
        updateAudioProperties();
    }

    void updateAudioProperties() 
    {
        if (player == null || treasure == null) {
            return;
        }

        

        float distance = Vector3.Distance(player.transform.position, treasure.transform.position);
        Debug.Log("Distance to treasure: " + distance);

        if (distance < maxDistance) {
            audioSource.volume = 1 - (distance / maxDistance);
            audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, 1 - (distance / maxDistance));
        } else {
            audioSource.volume = 0;
            audioSource.pitch = minPitch; // Optional: Set to min pitch instead of 0
        }
    }
}
