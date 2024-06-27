using UnityEngine;
using DualPantoToolkit;

public class ItHandle_FLOW : MonoBehaviour
{
    PantoHandle lowerHandle;
    bool free = true;
    void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
    }

    void FixedUpdate()
    {
        Vector3 currentPosition = lowerHandle.HandlePosition(transform.position);
        currentPosition.y = -5;
        transform.position = currentPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (free)
            {
                lowerHandle.Freeze();
            }
            else
            {
                lowerHandle.Free();
            }
            free = !free;
        }
    }
}
