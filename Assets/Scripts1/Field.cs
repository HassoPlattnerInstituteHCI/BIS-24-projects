using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public MeshRenderer r;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeColor() {
        Color newColor = new Color(1f,1f,1f,1f);
        r.material.SetColor("_Color", newColor);
    }
}
