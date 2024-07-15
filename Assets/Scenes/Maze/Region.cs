using UnityEngine;

public class Region : MonoBehaviour
{
    public AudioClip ambientSound;
    public AudioClip successSound;
    private AudioSource audioSource;
    private Maze maze;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        audioSource.playOnAwake = false;
        audioSource.clip = ambientSound;
        audioSource.volume = 0.5f;
        maze = GameObject.Find("Game").GetComponent<Maze>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            audioSource.Play();
            maze.EnteredArea(gameObject.name);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            audioSource.Stop();
        }
    }
}