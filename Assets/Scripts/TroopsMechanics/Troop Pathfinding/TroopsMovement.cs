using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

namespace GraphPathfindingTroops{
    public abstract class TroopMovementLogic
    {
        #region Field
        protected NavMeshAgent navMeshAgent;
        protected List<Vector3> path;
        protected int currentWaypointIndex = 0;
        protected float speed;
        protected bool followingPath = false;
        #endregion
        public void Initialize(List<Vector3> path, NavMeshAgent agent, float troopSpeed)
        {
            this.path = path;
            navMeshAgent = agent;
            this.speed = troopSpeed;
            agent.speed = speed;
            MoveToNextWaypoint();
        }
        public abstract void MoveTroop();
        protected void MoveToNextWaypoint()
        {
            if (path.Count == 0)
            {
                Debug.LogWarning("No waypoints assigned.");
                return;
            }
            // Set destination to the current waypoint
            navMeshAgent.SetDestination(path[currentWaypointIndex]);
            followingPath = true;
        }
        protected void UpdateWaypointIndex()
        {
            if (currentWaypointIndex < path.Count - 1)
            {
                currentWaypointIndex++;
            }
            else
            {
                followingPath = false;
            }
        }
        protected void CheckAndMoveToNextWaypoint()
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && followingPath)
            {
                UpdateWaypointIndex();
                MoveToNextWaypoint();
            }
        }
    }
    public class MyTroopMovementLogic : TroopMovementLogic
    {
        public override void MoveTroop()
        {
            CheckAndMoveToNextWaypoint();
        }
    }
}


// using UnityEngine;
// using UnityEngine.AI;
// using System.Collections.Generic;

// namespace GraphPathfindingTroops
// {
    // public abstract class TroopMovementLogic
    // {
    //     protected NavMeshAgent navMeshAgent;
    //     protected List<Node> path;
    //     protected int currentPathIndex = 0;
    //     protected float speed;

    //     public void Initialize(List<Node> path, NavMeshAgent agent, float troopSpeed)
    //     {
    //         this.path = path;
    //         navMeshAgent = agent;
    //         this.speed = troopSpeed;
    //         agent.speed = speed;

    //         MoveAlongPath();
    //     }

    //     public abstract void MoveTroop();

    //     protected void MoveAlongPath()
    //     {
    //         if (path == null || path.Count == 0)
    //         {
    //             Debug.LogWarning("Path is empty or null.");
    //             return;
    //         }

    //         Vector3[] waypoints = new Vector3[path.Count];
    //         for (int i = 0; i < path.Count; i++)
    //         {
    //             waypoints[i] = path[i].Position;
    //         }

    //         navMeshAgent.SetPath(CreateNavMeshPath(waypoints));
    //     }

    //     private NavMeshPath CreateNavMeshPath(Vector3[] waypoints)
    //     {
    //         NavMeshPath navMeshPath = new NavMeshPath();
    //         navMeshAgent.CalculatePath(waypoints[waypoints.Length - 1], navMeshPath);
    //         return navMeshPath;
    //     }

    //     protected void CheckAndMoveToNextPathNode()
    //     {
    //         if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
    //         {
    //             if (currentPathIndex < path.Count - 1)
    //             {
    //                 currentPathIndex++;
    //                 MoveAlongPath();
    //             }
    //         }
    //     }
    // }

    // public class MyTroopMovementLogic : TroopMovementLogic
    // {
    //     public override void MoveTroop()
    //     {
    //         CheckAndMoveToNextPathNode();
    //     }
    // }
//     public abstract class TroopMovementLogic
//     {
//         protected NavMeshAgent navMeshAgent;
//         protected List<Node> path;
//         protected int currentPathIndex = 0;
//         protected float speed;

//         public void Initialize(List<Node> path, NavMeshAgent agent, float troopSpeed)
//         {
//             this.path = path;
//             navMeshAgent = agent;
//             this.speed = troopSpeed;
//             agent.speed = speed;

//             MoveToNextPathNode();
//         }

//         public abstract void MoveTroop();

//         protected void MoveToNextPathNode()
//         {
//             if (path == null || path.Count == 0)
//             {
//                 Debug.LogWarning("Path is empty or null.");
//                 return;
//             }

//             if (currentPathIndex < path.Count)
//             {
//                 navMeshAgent.SetDestination(path[currentPathIndex].Position);
//                 currentPathIndex++;
//             }
//         }

//         protected void CheckAndMoveToNextPathNode()
//         {
//             if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
//             {
//                 MoveToNextPathNode();
//             }
//         }
//     }

//     public class MyTroopMovementLogic : TroopMovementLogic
//     {
//         public override void MoveTroop()
//         {
//             CheckAndMoveToNextPathNode();
//         }
//     }
// }
