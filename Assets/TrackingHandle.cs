using UnityEngine;
using DualPantoToolkit;

public class TrackingHandle : MonoBehaviour
{
    PantoHandle lowerHandle;
    bool free = true;
    public GameObject target;
    public bool isDone = false;
    public float angle;
    public float deltaX;
    public float deltaZ;

    public GameObject follow;

    void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        // target = GameObject.FindGameObjectsWithTag("MapsTarget")[0];
        target = follow;
    }

    void FixedUpdate()
    {
        transform.position = lowerHandle.HandlePosition(transform.position);
        transform.eulerAngles = new Vector3(0, lowerHandle.GetRotation(), 0);
    }

    void Update()
    {
        // angle = getAngle(this.transform.position, target.transform.position);
        deltaX = target.transform.position.x - this.transform.position.x;
        deltaZ = target.transform.position.z - this.transform.position.z;
        angle = (360 + Vector3.SignedAngle(Vector3.forward, (target.transform.position - this.transform.position) , Vector3.up))%360;
        lowerHandle.Rotate(angle);
    }

    float getAngle(Vector3 point1, Vector3 point2) {
        return Mathf.Atan2(point1.z - point2.z , point1.x - point2.x) * Mathf.Rad2Deg;
    }
}
