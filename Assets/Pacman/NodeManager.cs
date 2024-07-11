using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    GameObjects[] nodes;
    List<Edge> Edges = new List<Edges>();
    // Start is called before the first frame update
    void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("PacmanNode");
        for (int i = 0; i < nodes.Length; i++) {
            for (int j = i+1; j < nodes.Length; j++) {
                if (nodes[i].transform.position.x == nodes[j].transform.position.x || nodes[i].transform.position.z == nodes[j].transform.position.z ) {
                    nodes[i].adjacents.Add(nodes[j]);
                    nodes[j].adjacents.Add(nodes[i]);
                    Edges.Add(nodes[i], nodes[j]);
                }
            }
        }
        foreach (Edge edge in Edges) {
            GameObject object = new GameObject("Edge");
            object.transform.position = edge.getMidpoint;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
