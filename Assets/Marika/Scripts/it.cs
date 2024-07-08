using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;

public class it : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject current;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    async void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Text"))
        {

            //other.gameObject.readOut();
        }

    }
}

