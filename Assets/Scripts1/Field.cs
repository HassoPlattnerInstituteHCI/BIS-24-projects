using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public MeshRenderer r;
    private string color = "not filled";
    private bool massSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getSelected() {
        return massSelected;
    }
    public void setSelected(bool s) {
        massSelected = s;
    }

    public string getColor() {
        return color;
    }

    public void changeColor(string color2, float red, float green,float blue, float alpha) {
        color = color2;
        Color newColor = new Color(red,green,blue,alpha);
        r.material.SetColor("_Color", newColor);
    }
}
