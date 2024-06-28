using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFlollower : MonoBehaviour
{
    public BezierCurveBuilder bezierCurve;
    public GameObject monitoredObject;
    public float speed = 5f;
    private int currentSegment = 0;
    private MovementDetector movementDetector;

    // Start is called before the first frame update
    void Start()
    {
        if (monitoredObject != null)
        {
            movementDetector = monitoredObject.GetComponent<MovementDetector>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (movementDetector != null && movementDetector.IsMoving)
        {
            MoveAlongPath();
        }
    }

    void MoveAlongPath()
    {
        if (currentSegment <= bezierCurve.segmentCount)
        {
            Vector3 targetPosition = bezierCurve.GetComponent<LineRenderer>().GetPosition(currentSegment);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                currentSegment++;
            }
        }
    }
}
