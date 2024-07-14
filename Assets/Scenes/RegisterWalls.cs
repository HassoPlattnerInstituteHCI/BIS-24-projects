using System.Threading.Tasks;
using UnityEngine;
using DualPantoToolkit;

public class RegisterWalls : MonoBehaviour
{
    PantoHandle meHandle;

    async void Start()
    {
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        await meHandle.MoveToPosition(new Vector3(2, 0, -5), 20f);
        await Task.Delay(5000);
        PantoCollider[] pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.CreateObstacle();
            collider.Enable();
        }
    }
}
