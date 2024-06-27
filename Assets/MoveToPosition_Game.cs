using UnityEngine;
using DualPantoToolkit;
using System.Threading.Tasks;

public class MoveToPosition_Game : MonoBehaviour
{
    /* public bool isUpper;
    public bool shouldFreeHandle;
    public float speed = 10f; */
    PantoHandle handle;
    //public Gameobject gameObject;
    async void Start()
    {
        /* await Task.Delay(500);
        handle = isUpper
            ? (PantoHandle)GameObject.Find("Panto").GetComponent<UpperHandle>()
            : (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>(); */

        handle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();

        await handle.SwitchTo(gameObject, 20f);
    }

    /* async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            await handle.MoveToPosition(gameObject.transform.position, speed, shouldFreeHandle);
        }
        
    } */
}