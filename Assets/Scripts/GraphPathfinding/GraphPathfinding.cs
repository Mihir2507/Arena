// using System.Collections.Generic;
// using UnityEngine;

// public class GraphPathfinding
// {
//     private readonly Graph graph;

//     public GraphPathfinding(Graph graph)
//     {
//         this.graph = graph;
//     }

//     public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
//     {
//         Node startNode = graph.GetNearestNode(startPos);
//         Node targetNode = graph.GetNearestNode(targetPos);

//         return AStarSearch(startNode, targetNode);
//     }

//     private List<Node> AStarSearch(Node startNode, Node targetNode)
//     {
//         List<Node> openList = new List<Node>();
//         HashSet<Node> closedList = new HashSet<Node>();

//         Dictionary<Node, float> gCosts = new Dictionary<Node, float>();
//         Dictionary<Node, float> fCosts = new Dictionary<Node, float>();
//         Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();

//         openList.Add(startNode);
//         gCosts[startNode] = 0;
//         fCosts[startNode] = Heuristic(startNode, targetNode);

//         while (openList.Count > 0)
//         {
//             Node currentNode = GetLowestFCostNode(openList, fCosts);

//             if (currentNode == targetNode)
//             {
//                 return ReconstructPath(cameFrom, currentNode);
//             }

//             openList.Remove(currentNode);
//             closedList.Add(currentNode);

//             foreach (Node neighbor in currentNode.Neighbors)
//             {
//                 if (closedList.Contains(neighbor))
//                 {
//                     continue;
//                 }

//                 float tentativeGCost = gCosts[currentNode] + Vector3.Distance(currentNode.Position, neighbor.Position);

//                 if (!openList.Contains(neighbor))
//                 {
//                     openList.Add(neighbor);
//                 }
//                 else if (tentativeGCost >= gCosts[neighbor])
//                 {
//                     continue;
//                 }

//                 cameFrom[neighbor] = currentNode;
//                 gCosts[neighbor] = tentativeGCost;
//                 fCosts[neighbor] = gCosts[neighbor] + Heuristic(neighbor, targetNode);
//             }
//         }

//         return new List<Node>(); // Return an empty path if no path is found
//     }

//     private float Heuristic(Node a, Node b)
//     {
//         return Vector3.Distance(a.Position, b.Position);
//     }

//     private Node GetLowestFCostNode(List<Node> openList, Dictionary<Node, float> fCosts)
//     {
//         Node lowestFCostNode = openList[0];
//         float lowestFCost = fCosts[lowestFCostNode];

//         foreach (Node node in openList)
//         {
//             float fCost = fCosts[node];
//             if (fCost < lowestFCost)
//             {
//                 lowestFCost = fCost;
//                 lowestFCostNode = node;
//             }
//         }

//         return lowestFCostNode;
//     }

//     private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node currentNode)
//     {
//         List<Node> path = new List<Node>();
//         while (cameFrom.ContainsKey(currentNode))
//         {
//             path.Add(currentNode);
//             currentNode = cameFrom[currentNode];
//         }
//         path.Reverse();
//         return path;
//     }
// }