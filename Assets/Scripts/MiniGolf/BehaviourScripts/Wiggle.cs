using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;

public class Wiggle : MonoBehaviour
{
    float strength=0.26f;
    float time=0.04f;
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
        for(int i=0;i<10;i++){
            dps.ApplyForce(isUpper,direction,strength);
            yield return new WaitForSeconds(time);
            dps.FreeHandle(isUpper);
            direction*=-1;
            yield return new WaitForSeconds(time);
        }
    }

}
