using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
public class placeCircles : MonoBehaviour
{
    // Start is called before the first frame update
    PantoHandle upperHandle;
    public GameObject placeSquare;
    private Vector3 positionUpper = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        positionUpper = upperHandle.HandlePosition(positionUpper);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(placeSquare, positionUpper, Quaternion.identity);
            Debug.Log("Key Pressed");
        }
    }
}
