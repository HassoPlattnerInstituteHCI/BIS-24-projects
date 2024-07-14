using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    GameObject[] nodeObjects;
    Dijkstra dijkstra = new Dijkstra();
    DotManager dotManager;
    public List<Node> nodes = new List<Node>();
    // Start is called before the first frame update
    void Start()
    {
        Node node = new Node(new Vector3(0,0,0));
        nodeObjects = GameObject.FindGameObjectsWithTag("PacmanNode");
        dotManager = GameObject.FindGameObjectsWithTag("PacmanDotManager")[0].GetComponent<DotManager>();

        foreach (GameObject nodeObject in nodeObjects) {
            nodes.Add(new Node(nodeObject.transform.position));
            // Debug.Log(nodeObject.transform.position);
        }
        // nodes.Add(new Node(new Vector3(-2.0f, 0.0f, 2.0f)));
        // nodes.Add(new Node(new Vector3(2.0f, 0.0f, 2.0f)));


        for (int i = 0; i < nodes.Count; i++) {
            for (int j = i+1; j < nodes.Count; j++) {
                if (nodes[i].position.x == nodes[j].position.x || nodes[i].position.z == nodes[j].position.z ) {
                    checkNearestNode(nodes[i], nodes[j]);
                    checkNearestNode(nodes[j], nodes[i]);
                }
            }
        }
        // foreach (Node n in nodes) {
        //     // Debug.Log(n.position);
        //     // foreach (Node j in nodes.edges)
        //     Debug.Log(n.edges.Count);
        // }

        dijkstra.nodes = nodes;
        // dijkstra.FindShortestPath(nodes[0], nodes[4]);
        // Debug.Log(nodes[0].position);
        // Debug.Log(nodes[4].position);
        // Debug.Log("_");
        // List<Node> path = dijkstra.GetShortestPath(nodes[0], nodes[4]);
        // foreach (Node n in path) { 
        //     Debug.Log(n.position);
        // }

        Debug.Log(nodes.Count);

        DotPlot();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Node> getPath(Node node1, Node node2) {
        dijkstra.FindShortestPath(node1, node2);
        return dijkstra.GetShortestPath(node1, node2);
    }

    public Node getNearestNode(Vector3 position) {
        float distance = float.PositiveInfinity;
        Node nearest = new Node(new Vector3(42,0,-2));
        foreach(Node n in nodes) {
            if ((n.position - position).magnitude < distance) {
                nearest = n;
                distance = (n.position - position).magnitude;
            } 
        }
        return nearest;
    }
    private void checkNearestNode(Node node1, Node node2) {
        float distance = (node1.position - node2.position).magnitude;
        Vector3 direction = (node1.position - node2.position).normalized;

        Edge value;

        if (node1.nearestNode.TryGetValue(direction, out value)) {
            if (value.distance > distance) {
                Edge edge = new Edge(node2, distance);
                node1.nearestNode[direction] = edge;
                node1.edges.Remove(value);
                node1.edges.Add(edge);
            }
        }
        else {
            Edge edge = new Edge(node2, distance);
            node1.nearestNode[direction] = edge;
            node1.edges.Add(edge);
        }
    }

    void DotPlot() {
        List<Node> finished = new List<Node>();
        foreach (Node node in nodes) {
            foreach(Edge e in node.edges) {
                if (!finished.Contains(e.target)) {
                    dotManager.plotDots(node.position, e.target.position);
                }
            }
            finished.Add(node);
        }
    }
}
