// using UnityEngine;
// using System.Collections.Generic;

// public class PathfindingManagerVisuals : MonoBehaviour
// {
//     public GraphVisuals graph;
//     [SerializeField] private int nodeCount = 10;
//     [SerializeField] private float scatterRadius = 10f;

//     private void Start()
//     {
//         // Initialize the graph
//         graph = new GraphVisuals();

//         // Create nodes
//         for (int i = 0; i < nodeCount; i++)
//         {
//             Vector3 position = new Vector3(
//                 Random.Range(-scatterRadius, scatterRadius),
//                 0,
//                 Random.Range(-scatterRadius, scatterRadius)
//             );
//             NodeVisuals node = new NodeVisuals(position);
//             graph.AddNode(node);
//         }

//         // Connect nodes
//         float connectionDistance = 10f; // Adjust as needed
//         foreach (NodeVisuals nodeA in graph.Nodes)
//         {
//             foreach (NodeVisuals nodeB in graph.Nodes)
//             {
//                 if (nodeA != nodeB && Vector3.Distance(nodeA.Position, nodeB.Position) <= connectionDistance)
//                 {
//                     graph.ConnectNodes(nodeA, nodeB);
//                 }
//             }
//         }

//         // Perform pathfinding
//         GraphPathfindingVisuals pathfinding = new GraphPathfindingVisuals(graph);
//         List<NodeVisuals> path = pathfinding.FindPath(new Vector3(0, 0, 0), new Vector3(10, 0, 10));

//         // Example of using the path
//         foreach (NodeVisuals node in path)
//         {
//             Debug.Log("NodeVisuals Position: " + node.Position);
//         }
//     }

//     private NodeVisuals CreateNode(Vector3 position)
//     {
//         GameObject nodeObj = new GameObject("NodeVisuals");
//         nodeObj.transform.position = position;
//         NodeVisuals node = new NodeVisuals(position);
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
using UnityEngine;
using System.Collections.Generic;

public class PathfindingManagerVisuals : MonoBehaviour
{
    public GraphVisuals graph;
    [SerializeField] private int nodeCount = 70;
    [SerializeField] private float scatterRadius = 50f;

    private void Start()
    {
        // Initialize the graph
        graph = new GraphVisuals();

        // Create nodes
        for (int i = 0; i < nodeCount; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(-scatterRadius, scatterRadius),
                0,
                Random.Range(-scatterRadius, scatterRadius)
            );
            NodeVisuals node = new NodeVisuals(position);
            graph.AddNode(node);
        }

        // Connect nodes
        float connectionDistance = 10f; // Adjust as needed
        foreach (NodeVisuals nodeA in graph.Nodes)
        {
            foreach (NodeVisuals nodeB in graph.Nodes)
            {
                if (nodeA != nodeB && Vector3.Distance(nodeA.Position, nodeB.Position) <= connectionDistance)
                {
                    graph.ConnectNodes(nodeA, nodeB);
                }
            }
        }

        // Perform pathfinding
        GraphPathfindingVisuals pathfinding = new GraphPathfindingVisuals(graph);
        List<NodeVisuals> path = pathfinding.FindPath(graph.Nodes[0].Position, graph.Nodes[graph.Nodes.Count - 1].Position);

        // Example of using the path
        foreach (NodeVisuals node in path)
        {
            Debug.Log("NodeVisuals Position: " + node.Position);
        }
    }

    // Optional: To visualize the graph in the editor
    private void OnDrawGizmos()
    {
        if (graph != null)
        {
            graph.DrawGizmos();
        }
    }
}