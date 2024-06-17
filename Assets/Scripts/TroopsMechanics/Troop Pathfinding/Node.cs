using UnityEngine;
using System.Collections.Generic;

public class Node
{
    public Vector3 Position { get; }
    public List<Node> Neighbors { get; }

    public Node(Vector3 position)
    {
        Position = position;
        Neighbors = new List<Node>();
    }

    // Optional: To visualize connections in the editor
    public void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Position, 0.2f);

        Gizmos.color = Color.yellow;
        foreach (var neighbor in Neighbors)
        {
            Gizmos.DrawLine(Position, neighbor.Position);
        }
    }
}