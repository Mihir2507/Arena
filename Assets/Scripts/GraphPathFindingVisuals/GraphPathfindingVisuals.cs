using System.Collections.Generic;
using UnityEngine;

public class GraphPathfindingVisuals
{
    private readonly GraphVisuals graph;

    public GraphPathfindingVisuals(GraphVisuals graph)
    {
        this.graph = graph;
    }

    public List<NodeVisuals> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        NodeVisuals startNodeVisuals = graph.GetNearestNode(startPos);
        NodeVisuals targetNodeVisuals = graph.GetNearestNode(targetPos);

        return AStarSearch(startNodeVisuals, targetNodeVisuals);
    }

    private List<NodeVisuals> AStarSearch(NodeVisuals startNodeVisuals, NodeVisuals targetNodeVisuals)
    {
        List<NodeVisuals> openList = new List<NodeVisuals>();
        HashSet<NodeVisuals> closedList = new HashSet<NodeVisuals>();

        Dictionary<NodeVisuals, float> gCosts = new Dictionary<NodeVisuals, float>();
        Dictionary<NodeVisuals, float> fCosts = new Dictionary<NodeVisuals, float>();
        Dictionary<NodeVisuals, NodeVisuals> cameFrom = new Dictionary<NodeVisuals, NodeVisuals>();

        openList.Add(startNodeVisuals);
        gCosts[startNodeVisuals] = 0;
        fCosts[startNodeVisuals] = Heuristic(startNodeVisuals, targetNodeVisuals);

        while (openList.Count > 0)
        {
            NodeVisuals currentNodeVisuals = GetLowestFCostNodeVisuals(openList, fCosts);

            if (currentNodeVisuals == targetNodeVisuals)
            {
                return ReconstructPath(cameFrom, currentNodeVisuals);
            }

            openList.Remove(currentNodeVisuals);
            closedList.Add(currentNodeVisuals);

            foreach (NodeVisuals neighbor in currentNodeVisuals.Neighbors)
            {
                if (closedList.Contains(neighbor))
                {
                    continue;
                }

                float tentativeGCost = gCosts[currentNodeVisuals] + Vector3.Distance(currentNodeVisuals.Position, neighbor.Position);

                if (!openList.Contains(neighbor))
                {
                    openList.Add(neighbor);
                }
                else if (tentativeGCost >= gCosts[neighbor])
                {
                    continue;
                }

                cameFrom[neighbor] = currentNodeVisuals;
                gCosts[neighbor] = tentativeGCost;
                fCosts[neighbor] = gCosts[neighbor] + Heuristic(neighbor, targetNodeVisuals);
            }
        }

        return new List<NodeVisuals>(); // Return an empty path if no path is found
    }

    private float Heuristic(NodeVisuals a, NodeVisuals b)
    {
        return Vector3.Distance(a.Position, b.Position);
    }

    private NodeVisuals GetLowestFCostNodeVisuals(List<NodeVisuals> openList, Dictionary<NodeVisuals, float> fCosts)
    {
        NodeVisuals lowestFCostNodeVisuals = openList[0];
        float lowestFCost = fCosts[lowestFCostNodeVisuals];

        foreach (NodeVisuals nodeVisualsNodeVisuals in openList)
        {
            float fCost = fCosts[nodeVisualsNodeVisuals];
            if (fCost < lowestFCost)
            {
                lowestFCost = fCost;
                lowestFCostNodeVisuals = nodeVisualsNodeVisuals;
            }
        }

        return lowestFCostNodeVisuals;
    }

    private List<NodeVisuals> ReconstructPath(Dictionary<NodeVisuals, NodeVisuals> cameFrom, NodeVisuals currentNodeVisuals)
    {
        List<NodeVisuals> path = new List<NodeVisuals>();
        while (cameFrom.ContainsKey(currentNodeVisuals))
        {
            path.Add(currentNodeVisuals);
            currentNodeVisuals = cameFrom[currentNodeVisuals];
        }
        path.Reverse();

        // Mark the path edges in green
        for (int i = 0; i < path.Count - 1; i++)
        {
            path[i].EdgeColor = Color.green;
        }

        return path;
    }
}