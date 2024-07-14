using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public static void shoot(Vector3 force){
        GameObject.Find("Ball").GetComponent<Rigidbody>().AddForce(force);
    }
}
