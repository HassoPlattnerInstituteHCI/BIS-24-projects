using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 position;
    public List<Edge> edges = new List<Edge>();
    public Dictionary<Vector3, Edge> nearestNode = new Dictionary<Vector3, Edge>();
    
    public Node(Vector3 pos)
    {
        position = pos;
    }
}

public class Edge
{
    public Node target;
    public float distance;
    
    public Edge(Node targetNode, float edgeDistance)
    {
        target = targetNode;
        distance = edgeDistance;
    }
}

public class Dijkstra
{
    public List<Node> nodes = new List<Node>();

    public Dictionary<Node, float> distances;
    public Dictionary<Node, Node> previousNodes;

    public void FindShortestPath(Node startNode, Node endNode)
    {
        distances = new Dictionary<Node, float>();
        previousNodes = new Dictionary<Node, Node>();

        List<Node> unvisitedNodes = new List<Node>(nodes);

        foreach (Node node in nodes)
        {
            distances[node] = float.MaxValue;
            previousNodes[node] = null;
        }

        distances[startNode] = 0;

        while (unvisitedNodes.Count > 0)
        {
            Node currentNode = GetNodeWithSmallestDistance(unvisitedNodes);
            unvisitedNodes.Remove(currentNode);

            if (currentNode == endNode)
                break;

            foreach (Edge edge in currentNode.edges)
            {
                Node neighbor = edge.target;
                float newDist = distances[currentNode] + edge.distance;

                if (newDist < distances[neighbor])
                {
                    distances[neighbor] = newDist;
                    previousNodes[neighbor] = currentNode;
                }
            }
        }
    }

    private Node GetNodeWithSmallestDistance(List<Node> nodes)
    {
        Node smallestDistNode = null;
        float smallestDist = float.MaxValue;

        foreach (Node node in nodes)
        {
            float dist = distances[node];
            if (dist < smallestDist)
            {
                smallestDist = dist;
                smallestDistNode = node;
            }
        }

        return smallestDistNode;
    }

    public List<Node> GetShortestPath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = previousNodes[currentNode];
        }

        path.Reverse();
        return path;
    }
}