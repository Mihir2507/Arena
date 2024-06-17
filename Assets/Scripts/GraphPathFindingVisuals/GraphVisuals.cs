using System.Collections.Generic;
using UnityEngine;

public class GraphVisuals
{
    public List<NodeVisuals> Nodes { get; }

    public GraphVisuals()
    {
        Nodes = new List<NodeVisuals>();
    }

    public void AddNode(NodeVisuals node)
    {
        if (!Nodes.Contains(node))
        {
            Nodes.Add(node);
        }
    }

    public void ConnectNodes(NodeVisuals nodeA, NodeVisuals nodeB)
    {
        if (nodeA != nodeB && !nodeA.Neighbors.Contains(nodeB))
        {
            nodeA.Neighbors.Add(nodeB);
            nodeB.Neighbors.Add(nodeA); // For bidirectional graph
        }
    }

    public NodeVisuals GetNearestNode(Vector3 position)
    {
        NodeVisuals nearestNode = null;
        float minDistance = float.MaxValue;

        foreach (NodeVisuals node in Nodes)
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

    // Optional: To visualize the graph in the editor
    public void DrawGizmos()
    {
        foreach (var node in Nodes)
        {
            node.DrawGizmos();
        }
    }
}