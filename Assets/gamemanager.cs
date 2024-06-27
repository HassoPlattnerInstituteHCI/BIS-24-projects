using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public GameObject[] pixelObjects;// = new GameObject[9];
    public GameObject playerObject;

    void Start()
    {
        /*
        foreach(GameObject gameObj in GameObject.FindGameObjectsWithTag("pixel"))
        {
            int x = (int) gameObj.name[1];
            int y = (int) gameObj.name[3];
            pixelObjects[x + 3 * y] = gameObj;
            Debug.LogError(gameObj.name);
        }
        foreach(GameObject gameObj in GameObject.FindGameObjectsWithTag("Player"))
        {
            playerObject = gameObj;
            break;
        }
        */
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float nearestDist = float.PositiveInfinity;
            GameObject nearestPixel = pixelObjects[0];
            foreach(GameObject pixelObject in pixelObjects)
            {
                float distance = (playerObject.transform.position - pixelObject.transform.position).magnitude;
                if (distance < nearestDist) {
                    nearestDist = distance;
                    nearestPixel = pixelObject;
                }
            }
            nearestPixel.GetComponent<Renderer>().material.color = new Color(0, 205, 102);
        }
    }
}
