using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    public Node[] nodes ;
    public void Edge(Node node1, Node node2) {
        nodes = {node1, node2};
    }

    public Vector3 getMidpoint() {
        return (nodes[0].transform.position - nodes[1].transform.position)/2;
    }
}
