using UnityEngine;
using System.Collections.Generic;

public class NodeVisuals
{
    public Vector3 Position { get; }
    public List<NodeVisuals> Neighbors { get; }
    public Color NodeColor { get; set; } = Color.red;
    public Color EdgeColor { get; set; } = Color.yellow;

    public NodeVisuals(Vector3 position)
    {
        Position = position;
        Neighbors = new List<NodeVisuals>();
    }

    // Optional: To visualize connections in the editor
    public void DrawGizmos()
    {
        Gizmos.color = NodeColor;
        Gizmos.DrawSphere(Position, 0.2f);

        Gizmos.color = EdgeColor;
        foreach (var neighbor in Neighbors)
        {
            Gizmos.DrawLine(Position, neighbor.Position);
        }
    }
}