// using System.Collections.Generic;
// using UnityEngine;

// public class Graph
// {
//     public List<Node> Nodes { get; }

//     public Graph()
//     {
//         Nodes = new List<Node>();
//     }

//     public void AddNode(Node node)
//     {
//         if (!Nodes.Contains(node))
//         {
//             Nodes.Add(node);
//         }
//     }

//     public void ConnectNodes(Node nodeA, Node nodeB)
//     {
//         if (nodeA != nodeB && !nodeA.Neighbors.Contains(nodeB))
//         {
//             nodeA.Neighbors.Add(nodeB);
//             nodeB.Neighbors.Add(nodeA); // For bidirectional graph
//         }
//     }

//     public Node GetNearestNode(Vector3 position)
//     {
//         Node nearestNode = null;
//         float minDistance = float.MaxValue;

//         foreach (Node node in Nodes)
//         {
//             float distance = Vector3.Distance(position, node.Position);
//             if (distance < minDistance)
//             {
//                 minDistance = distance;
//                 nearestNode = node;
//             }
//         }

//         return nearestNode;
//     }

//     // Optional: To visualize the graph in the editor
//     public void DrawGizmos()
//     {
//         foreach (var node in Nodes)
//         {
//             node.DrawGizmos();
//         }
//     }
// }