using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBuilder : MonoBehaviour
{
    public string selectedIngredient { get; private set; }
    public GameObject selector;
    // Start is called before the first frame update
    void Start()
    {
        if (selector == null)
        {
            Debug.LogError("No selector set");
        }
        selectedIngredient = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
