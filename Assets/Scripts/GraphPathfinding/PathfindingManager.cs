// using UnityEngine;
// using System.Collections.Generic;

// public class PathfindingManager : MonoBehaviour
// {
//     public Graph graph;

//     private void Start()
//     {
//         // Initialize the graph
//         graph = new Graph();

//         // Example of manually creating nodes and connecting them
//         Node node1 = CreateNode(new Vector3(0, 0, 0));
//         Node node2 = CreateNode(new Vector3(2, 0, 0));
//         Node node3 = CreateNode(new Vector3(1, 0, 2));
//         Node node4 = CreateNode(new Vector3(3, 0, 2));
//         Node node5 = CreateNode(new Vector3(4, 0, 1));
//         Node node6 = CreateNode(new Vector3(5, 0, 3));
//         Node node7 = CreateNode(new Vector3(4, 0, 3));

//         graph.AddNode(node1);
//         graph.AddNode(node2);
//         graph.AddNode(node3);
//         graph.AddNode(node4);
//         graph.AddNode(node5);
//         graph.AddNode(node6);
//         graph.AddNode(node7);

//         graph.ConnectNodes(node1, node2);
//         graph.ConnectNodes(node2, node3);
//         graph.ConnectNodes(node3, node1);
//         graph.ConnectNodes(node3, node4);
//         graph.ConnectNodes(node4, node5);
//         graph.ConnectNodes(node5, node6);
//         graph.ConnectNodes(node6, node7);

//         // Perform pathfinding
//         GraphPathfinding pathfinding = new GraphPathfinding(graph);
//         List<Node> path = pathfinding.FindPath(new Vector3(0, 0, 0), new Vector3(5, 0, 3));
//         List<Node> path2 = pathfinding.FindPath(node1.Position, node5.Position);

//         int i = 1;
//         // Example of using the path
//         foreach (Node node in path)
//         {
//             Debug.Log("Node Position: " + node.Position + " is " +i+ " node");
//             i++;
//         }
//     }

//     private Node CreateNode(Vector3 position)
//     {
//         GameObject nodeObj = new GameObject("Node");
//         nodeObj.transform.position = position;
//         Node node = new Node(position);
//         return node;
//     }

//     // Optional: To visualize the graph in the editor
//     private void OnDrawGizmos()
//     {
//         if (graph != null)
//         {
//             graph.DrawGizmos();
//         }
//     }
// }