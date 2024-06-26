using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    public Graph Graph { get; private set; }

    private void Awake()
    {
        Graph = new Graph();

        // Initialize graph here
        InitializeGraph();
    }

    private void InitializeGraph()
    {
        // Node[] nodes = new Node[10];
        // nodes[0] = new Node(new Vector3(0, 0, 10));
        // nodes[1] = new Node(new Vector3(5, 0, 5));
        // nodes[2] = new Node(new Vector3(10, 0, 0));
        // nodes[3] = new Node(new Vector3(15, 0, -5));
        // nodes[4] = new Node(new Vector3(10, 0, -10));
        // nodes[5] = new Node(new Vector3(5, 0, -15));
        // nodes[6] = new Node(new Vector3(0, 0, -10));
        // nodes[7] = new Node(new Vector3(-5, 0, -5));
        // nodes[8] = new Node(new Vector3(-10, 0, 0));
        // nodes[9] = new Node(new Vector3(-5, 0, 5));

        // for (int i = 0; i < nodes.Length; i++)
        // {
        //     Graph.AddNode(nodes[i]);
        // }

        // for (int i = 0; i < nodes.Length; i++)
        // {
        //     Graph.ConnectNodes(nodes[i], nodes[(i + 1) % nodes.Length]);
        // }

        GameObject nodesParent = GameObject.Find("Nodes");

        if (nodesParent == null)
        {
            Debug.LogError("No GameObject named 'Nodes' found in the scene.");
            return;
        }

        Transform[] nodeTransforms = nodesParent.GetComponentsInChildren<Transform>();
        List<Node> nodes = new List<Node>();

        // Skip the first element since it's the parent itself
        for (int i = 1; i < nodeTransforms.Length; i++)
        {
            Node node = new Node(nodeTransforms[i].position);
            nodes.Add(node);
            Graph.AddNode(node);
        }

        // Connect nodes in a circular manner
        for (int i = 0; i < nodes.Count; i++)
        {
            Graph.ConnectNodes(nodes[i], nodes[(i + 1) % nodes.Count]);
        }
    }

    private void OnDrawGizmos()
    {
        if (Graph != null)
        {
            Graph.DrawGizmos();
        }
    }
}