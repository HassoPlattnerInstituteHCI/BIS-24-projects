using System.Threading.Tasks;
using UnityEngine;
using DualPantoToolkit;

public class ColliderHandler : MonoBehaviour
{
    async void Start()
    {
        await Task.Delay(4000);
        PantoCollider[] pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.CreateObstacle();
            collider.Enable();
        }
    }
}
