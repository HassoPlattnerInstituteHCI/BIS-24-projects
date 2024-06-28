using UnityEngine;
using DualPantoToolkit;
using SpeechIO;
using System.Threading.Tasks;

public class LowerPacmanHandle : MonoBehaviour
{
    PantoHandle lowerHandle;
    bool free = true;
    public GameObject target;
    SpeechOut speechOut = new SpeechOut();
    void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        target = GameObject.FindGameObjectsWithTag("PacmanTarget")[0];
    }

    void FixedUpdate()
    {
        transform.position = lowerHandle.HandlePosition(transform.position);
        transform.eulerAngles = new Vector3(0, lowerHandle.GetRotation(), 0);
    }

    void Update()
    {
    }

}
