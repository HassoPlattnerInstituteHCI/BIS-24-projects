using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GameObject player;
    public NodeManager nodeManager;
    public float speed = 0.03f;
    public Vector3 target;
    public Vector3 direction;
    public AudioClip deathSound;
    GameManager gameManager;
    AudioSource audioSource;

    private void setTarget(Vector3 t) {
        target = t;
        direction = (t - transform.position).normalized;
    }
    // Start is called before the first frame update
    void Start()
    {
        nodeManager = GameObject.FindGameObjectsWithTag("PacmanNodeManager")[0].GetComponent<NodeManager>();
        player = GameObject.FindGameObjectsWithTag("MeHandle")[0];
        // setTarget(nodeManager.getNearestNode(transform.position).position);
        setTarget(transform.position);
        
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        gameManager = GameObject.FindGameObjectsWithTag("PacmanGameManager")[0].GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = (transform.position - player.transform.position).magnitude;
        // playerDistance = 1 - (playerDistance / 10f);
        // playerDistance = playerDistance < 0 ? 0 : playerDistance;

        if (playerDistance > 10) playerDistance = 10;
        playerDistance = Mathf.Pow(playerDistance-10f, 2)/100f;
        // playerDistance = 1+ Mathf.Pow(playerDistance, 2)/(-100f);
        audioSource.volume = playerDistance;
    }

    void FixedUpdate() {

        if ((target - transform.position).magnitude > speed) {
            transform.position += direction * speed;
        }
        else if (target != transform.position) {
            transform.position = target;
        }
        else {
            getNextTarget();
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "UpperHandle") {
            GameOver();
        }
    }

    private void getNextTarget() {
        Node nearestNode = nodeManager.getNearestNode(transform.position);
        Node playerNearestNode = nodeManager.getNearestNode(player.transform.position);
        if (nearestNode == playerNearestNode) {
            Edge value;
            Vector3 direction = roundVector((transform.position - player.transform.position).normalized);
            if (nearestNode.nearestNode.TryGetValue(direction, out value)) setTarget(value.target.position);
            else Debug.Log("ERROR");
        }
        else {
            Debug.Log(nearestNode.position);
            Debug.Log(playerNearestNode.position);
            setTarget(nodeManager.getPath(nearestNode, playerNearestNode)[1].position);
        }
    }

    Vector3 roundVector(Vector3 vector) {
        return new Vector3(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));
    } 
    async void GameOver(){
        // audioSource.PlayOneShot(deathSound, 1f);
        StartCoroutine(PlayClipAndWait(deathSound));
        Debug.Log("DONE");
        gameManager.GameOver();
    }

    private IEnumerator PlayClipAndWait(AudioClip clip)
    {
        // Play the audio clip
        audioSource.PlayOneShot(clip);

        // Wait for the duration of the clip
        yield return new WaitForSeconds(clip.length);
    }
}
