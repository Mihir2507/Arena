// using System.Collections.Generic;
// using UnityEngine;

// public class GraphPathfinding
// {
//     private Graph graph;

//     public GraphPathfinding(Graph graph)
//     {
//         this.graph = graph;
//     }

//     public List<Node> FindPath(Vector3 start, Vector3 goal)
//     {
//         Node startNode = graph.GetNearestNode(start);
//         Node goalNode = graph.GetNearestNode(goal);

//         List<Node> openSet = new List<Node> { startNode };
//         Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
//         Dictionary<Node, float> gScore = new Dictionary<Node, float>();
//         Dictionary<Node, float> fScore = new Dictionary<Node, float>();

//         foreach (Node node in graph.Nodes)
//         {
//             gScore[node] = float.MaxValue;
//             fScore[node] = float.MaxValue;
//         }

//         gScore[startNode] = 0;
//         fScore[startNode] = Heuristic(startNode, goalNode);

//         while (openSet.Count > 0)
//         {
//             Node current = GetNodeWithLowestFScore(openSet, fScore);
//             if (current == goalNode)
//             {
//                 return ReconstructPath(cameFrom, current);
//             }

//             openSet.Remove(current);

//             foreach (Node neighbor in current.Neighbors)
//             {
//                 float tentativeGScore = gScore[current] + Vector3.Distance(current.Position, neighbor.Position);

//                 if (tentativeGScore < gScore[neighbor])
//                 {
//                     cameFrom[neighbor] = current;
//                     gScore[neighbor] = tentativeGScore;
//                     fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, goalNode);

//                     if (!openSet.Contains(neighbor))
//                     {
//                         openSet.Add(neighbor);
//                     }
//                 }
//             }
//         }

//         return new List<Node>();
//     }

//     private float Heuristic(Node a, Node b)
//     {
//         return Vector3.Distance(a.Position, b.Position);
//     }

//     private Node GetNodeWithLowestFScore(List<Node> openSet, Dictionary<Node, float> fScore)
//     {
//         Node lowest = openSet[0];
//         foreach (Node node in openSet)
//         {
//             if (fScore[node] < fScore[lowest])
//             {
//                 lowest = node;
//             }
//         }
//         return lowest;
//     }

//     private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
//     {
//         List<Node> totalPath = new List<Node> { current };
//         while (cameFrom.ContainsKey(current))
//         {
//             current = cameFrom[current];
//             totalPath.Add(current);
//         }
//         totalPath.Reverse();
//         return totalPath;
//     }
// }

// using System.Collections.Generic;
// using UnityEngine;

// public class GraphPathfinding
// {
//     private Graph graph;

//     public GraphPathfinding(Graph graph)
//     {
//         this.graph = graph;
//     }

//     public List<Node> FindPath(Vector3 start, Vector3 goal)
//     {
//         Node startNode = graph.GetNearestNode(start);
//         Node goalNode = graph.GetNearestNode(goal);

//         List<Node> openSet = new List<Node> { startNode };
//         HashSet<Node> closedSet = new HashSet<Node>();
//         Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
//         Dictionary<Node, float> gScore = new Dictionary<Node, float>();
//         Dictionary<Node, float> fScore = new Dictionary<Node, float>();

//         foreach (Node node in graph.Nodes)
//         {
//             gScore[node] = float.MaxValue;
//             fScore[node] = float.MaxValue;
//         }

//         gScore[startNode] = 0;
//         fScore[startNode] = Heuristic(startNode, goalNode);

//         while (openSet.Count > 0)
//         {
//             Node current = GetNodeWithLowestFScore(openSet, fScore);
//             if (current == goalNode)
//             {
//                 return ReconstructPath(cameFrom, current);
//             }

//             openSet.Remove(current);
//             closedSet.Add(current);

//             foreach (Node neighbor in current.Neighbors)
//             {
//                 if (closedSet.Contains(neighbor))
//                 {
//                     continue;
//                 }

//                 float tentativeGScore = gScore[current] + Vector3.Distance(current.Position, neighbor.Position);
//                 if (!openSet.Contains(neighbor))
//                 {
//                     openSet.Add(neighbor);
//                 }
//                 else if (tentativeGScore >= gScore[neighbor])
//                 {
//                     continue;
//                 }

//                 cameFrom[neighbor] = current;
//                 gScore[neighbor] = tentativeGScore;
//                 fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, goalNode);
//             }
//         }

//         return new List<Node>();
//     }

//     private float Heuristic(Node a, Node b)
//     {
//         return Vector3.Distance(a.Position, b.Position);
//     }

//     private Node GetNodeWithLowestFScore(List<Node> openSet, Dictionary<Node, float> fScore)
//     {
//         Node lowest = openSet[0];
//         foreach (Node node in openSet)
//         {
//             if (fScore[node] < fScore[lowest])
//             {
//                 lowest = node;
//             }
//         }
//         return lowest;
//     }

//     private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
//     {
//         List<Node> path = new List<Node> { current };
//         while (cameFrom.ContainsKey(current))
//         {
//             current = cameFrom[current];
//             path.Add(current);
//         }
//         path.Reverse();
//         return path;
//     }
// }

using System.Collections.Generic;
using UnityEngine;

public class GraphPathfinding
{
    private readonly Graph graph;

    public GraphPathfinding(Graph graph)
    {
        this.graph = graph;
    }

    public List<Vector3> FindPath(Vector3 start, Vector3 goal)
    {
        Node startNode = graph.GetNearestNode(start);
        Node goalNode = graph.GetNearestNode(goal);

        List<Node> openSet = new() { startNode };
        HashSet<Node> closedSet = new();
        Dictionary<Node, Node> cameFrom = new();
        Dictionary<Node, float> gScore = new();
        Dictionary<Node, float> fScore = new();

        foreach (Node node in graph.Nodes)
        {
            gScore[node] = float.MaxValue;
            fScore[node] = float.MaxValue;
        }

        gScore[startNode] = 0;
        fScore[startNode] = Heuristic(startNode, goalNode);

        while (openSet.Count > 0)
        {
            Node current = GetNodeWithLowestFScore(openSet, fScore);
            if (current == goalNode)
            {
                return ReconstructPath(cameFrom, current);
            }

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Node neighbor in current.Neighbors)
            {
                if (closedSet.Contains(neighbor))
                {
                    continue;
                }

                float tentativeGScore = gScore[current] + Vector3.Distance(current.Position, neighbor.Position);
                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                }
                else if (tentativeGScore >= gScore[neighbor])
                {
                    continue;
                }

                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, goalNode);
            }
        }
        return new List<Vector3>();
    }

    private float Heuristic(Node a, Node b)
    {
        return Vector3.Distance(a.Position, b.Position);
    }

    private Node GetNodeWithLowestFScore(List<Node> openSet, Dictionary<Node, float> fScore)
    {
        Node lowest = openSet[0];
        foreach (Node node in openSet)
        {
            if (fScore[node] < fScore[lowest])
            {
                lowest = node;
            }
        }
        return lowest;
    }

    private List<Vector3> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
    {
        List<Vector3> path = new() { current.Position };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current.Position);
        }
        path.Reverse();
        return path;
    }
}