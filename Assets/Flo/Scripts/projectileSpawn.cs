using UnityEngine;

public class projectileSpawn : MonoBehaviour {

    float timer = 10;
    public GameObject projectile;

    void Start() {
        GameObject player = GameObject.Find("Player");
    }

    void Update() {
        GameObject player = GameObject.Find("Player");
        
        MoveToPlayer(player);
        RotateAsPlayer(player);

        timer -= Time.deltaTime;
        if (timer < 0){
            SpawnProjectile();
            timer = 10;
        }
    }
    
    void MoveToPlayer(GameObject player) {
        Vector3 newPosition = player.transform.position;
        this.transform.position = newPosition;
    }

    void RotateAsPlayer(GameObject player) {
        Vector3 newRotation = new Vector3(player.transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z);
        this.transform.eulerAngles = newRotation; 
    }

    void SpawnProjectile() {
        Instantiate(projectile, transform.position, transform.rotation);
    }
}
     

