using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;

public class Wiggle : MonoBehaviour
{
    public float speed=0.5; 
    DualPantoSync dps;
    void Awake()
    {
        dps=GameObject.Find("Panto").GetComponent<DualPantoSync>();
    }
    public void wiggle_wiggle_wiggle(bool isUpper){
        StartCoroutine(wiggle(isUpper));
    }
    IEnumerator wiggle(bool isUpper){
        Vector3 direction=new Vector3(1,0,0);
        for(int i=0;i<5;i++){
            dps.ApplyForce(isUpper,direction,speed);
            Debug.Log("Applying Force "+i);
            yield return new WaitForSeconds(0.2f);
            direction*=-1;
        }
    }

}
