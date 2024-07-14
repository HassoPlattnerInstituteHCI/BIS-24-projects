using UnityEngine;
using DualPantoToolkit;

public class ColliderActivator : MonoBehaviour
{
    public bool active = true;
    private void OnTriggerEnter(Collider collider)
    {
        if (!active) return;
        PantoCollider pc;
        pc = collider.GetComponent<PantoCollider>();
        if (pc == null) pc = collider.GetComponentInChildren<PantoCollider>();
        if (pc == null) pc = collider.transform.parent.gameObject.GetComponentInChildren<PantoCollider>();
        
        if (pc != null && pc.enabled)
        {
            if (pc.GetContainingSpheres() == 0)
            {
                pc.CreateObstacle();
                //if (pc.IsEnabled()) pc.Enable();
                pc.Enable();
            }

            pc.IncreaseSpheres();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!active) return;
        PantoCollider pc;
        pc = collider.GetComponent<PantoCollider>();
        if (pc == null) pc = collider.GetComponentInChildren<PantoCollider>();
        if (pc == null) pc = collider.transform.parent.gameObject.GetComponentInChildren<PantoCollider>();
        
        if (pc != null)
        {
            pc.DecreaseSpheres();
            if (pc.GetContainingSpheres() == 0) pc.Remove();
        }
    }
}