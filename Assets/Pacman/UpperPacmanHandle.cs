using UnityEngine;
using DualPantoToolkit;

public class UpperPacmanHandle : MonoBehaviour
{
    bool free = true;
    PantoHandle upperHandle;
    void Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
    }

    void FixedUpdate()
    {
        transform.position = (upperHandle.HandlePosition(transform.position));
        transform.eulerAngles = new Vector3(0, upperHandle.GetRotation(), 0);
    }

    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "PacmanPortal") {
            Debug.Log("Colliding");
            upperHandle.MoveToPosition(collision.gameObject.GetComponent<Portal>().target, 10f ,true);
        }
    }
}
