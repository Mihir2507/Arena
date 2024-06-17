using UnityEngine;
using System.Collections.Generic;

public class Graph
{
    public List<Node> Nodes { get; private set; }

    public Graph()
    {
        Nodes = new List<Node>();
    }

    public void AddNode(Node node)
    {
        Nodes.Add(node);
    }

    public void ConnectNodes(Node nodeA, Node nodeB)
    {
        if (!nodeA.Neighbors.Contains(nodeB))
        {
            nodeA.Neighbors.Add(nodeB);
        }

        if (!nodeB.Neighbors.Contains(nodeA))
        {
            nodeB.Neighbors.Add(nodeA);
        }
    }

    public Node GetNearestNode(Vector3 position)
    {
        Node nearestNode = null;
        float minDistance = float.MaxValue;

        foreach (Node node in Nodes)
        {
            float distance = Vector3.Distance(position, node.Position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNode = node;
            }
        }

        return nearestNode;
    }

    public void DrawGizmos()
    {
        foreach (var node in Nodes)
        {
            node.DrawGizmos();
        }
    }
}

