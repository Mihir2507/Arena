// using UnityEngine;
// using UnityEngine.AI;
// using System.Collections.Generic;

// namespace GraphPathfindingTroops
// {
//     public abstract class TroopMovementLogic
//     {
//         #region Field
//         protected NavMeshAgent navMeshAgent;
//         protected List<Vector3> path;
//         protected int currentWaypointIndex = 0;
//         protected float speed;
//         protected bool followingPath = false;
//         protected Transform enemyTarget;
//         #endregion

//         public void Initialize(List<Vector3> path, NavMeshAgent agent, float troopSpeed)
//         {
//             this.path = path;
//             navMeshAgent = agent;
//             this.speed = troopSpeed;
//             agent.speed = speed;
//             MoveToNextWaypoint();
//         }

//         public abstract void MoveTroop();

//         protected void MoveToNextWaypoint()
//         {
//             if (path.Count == 0)
//             {
//                 Debug.LogWarning("No waypoints assigned.");
//                 return;
//             }

//             if (currentWaypointIndex < path.Count)
//             {
//                 navMeshAgent.SetDestination(path[currentWaypointIndex]);
//                 followingPath = true;
//             }
//             else
//             {
//                 followingPath = false;
//             }
//         }

//         protected void UpdateWaypointIndex()
//         {
//             if (currentWaypointIndex < path.Count - 1)
//             {
//                 currentWaypointIndex++;
//             }
//             else
//             {
//                 followingPath = false;
//             }
//         }

//         protected void CheckAndMoveToNextWaypoint()
//         {
//             if (enemyTarget == null && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && followingPath)
//             {
//                 UpdateWaypointIndex();
//                 MoveToNextWaypoint();
//             }
//         }

//         public void EngageEnemy(Transform enemy)
//         {
//             enemyTarget = enemy;
//             navMeshAgent.SetDestination(enemy.position);
//         }

//         public void ResumePath(List<Vector3> newPath)
//         {
//             enemyTarget = null;
//             path = newPath;
//             currentWaypointIndex = 0;
//             MoveToNextWaypoint();
//         }

//         public bool IsEngagingEnemy()
//         {
//             return enemyTarget != null;
//         }
//     }

//     public class MyTroopMovementLogic : TroopMovementLogic
//     {
//         public override void MoveTroop()
//         {
//             CheckAndMoveToNextWaypoint();
//         }
//     }
// }

using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace GraphPathfindingTroops
{
    public abstract class TroopMovementLogic
    {
        protected NavMeshAgent navMeshAgent;
        protected List<Vector3> path;
        protected int currentPathIndex;
        protected float speed;
        protected bool followingPath;

        public void Initialize(List<Vector3> path, NavMeshAgent agent, float troopSpeed)
        {
            this.path = path;
            navMeshAgent = agent;
            this.speed = troopSpeed;
            agent.speed = speed;
            currentPathIndex = 0;
            MoveAlongPath();
        }

        public abstract void MoveTroop();

        protected void MoveAlongPath()
        {
            if (path.Count == 0)
            {
                Debug.LogWarning("No path assigned.");
                return;
            }

            if (currentPathIndex < path.Count)
            {
                Debug.Log(currentPathIndex + "current Path Index");
                navMeshAgent.SetDestination(path[currentPathIndex]);
                // Vector3 nextposition = path[currentPathIndex + 1];
                followingPath = true;
                Debug.Log(Vector3.Distance(navMeshAgent.transform.position, path[currentPathIndex]));
                if(Vector3.Distance(navMeshAgent.transform.position, path[currentPathIndex]) < 4f){
                    currentPathIndex++;
                    Debug.Log(currentPathIndex+ " index");
                    Debug.Log(navMeshAgent.transform.position);
                    if(currentPathIndex < path.Count){
                        navMeshAgent.SetDestination(path[currentPathIndex]);
                    }
                    
                }
            }
        }

        protected void CheckAndMoveToNextNode()
        {
            if (followingPath && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                currentPathIndex++;
                if (currentPathIndex < path.Count)
                {
                    MoveAlongPath();
                }
                else
                {
                    followingPath = false;
                }
            }
        }
    }
    public class MyTroopMovementLogic : TroopMovementLogic
    {
        public override void MoveTroop()
        {
            // CheckAndMoveToNextNode();
            MoveAlongPath();
        }
    }
}

// using UnityEngine;
// using UnityEngine.AI;
// using System.Collections.Generic;

// namespace GraphPathfindingTroops{
//     public abstract class TroopMovementLogic
//     {
//         #region Field
//         protected NavMeshAgent navMeshAgent;
//         protected List<Vector3> path;
//         protected int currentWaypointIndex = 0;
//         protected float speed;
//         protected bool followingPath = false;
//         #endregion
//         public void Initialize(List<Vector3> path, NavMeshAgent agent, float troopSpeed)
//         {
//             this.path = path;
//             navMeshAgent = agent;
//             this.speed = troopSpeed;
//             agent.speed = speed;
//             MoveToNextWaypoint();
//         }
//         public abstract void MoveTroop();
//         protected void MoveToNextWaypoint()
//         {
//             if (path.Count == 0)
//             {
//                 Debug.LogWarning("No waypoints assigned.");
//                 return;
//             }
//             // Set destination to the current waypoint
//             navMeshAgent.SetDestination(path[currentWaypointIndex]);
//             followingPath = true;
//         }
//         protected void UpdateWaypointIndex()
//         {
//             if (currentWaypointIndex < path.Count - 1)
//             {
//                 currentWaypointIndex++;
//             }
//             else
//             {
//                 followingPath = false;
//             }
//         }
//         protected void CheckAndMoveToNextWaypoint()
//         {
//             if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && followingPath)
//             {
//                 UpdateWaypointIndex();
//                 MoveToNextWaypoint();
//             }
//         }
//     }
//     public class MyTroopMovementLogic : TroopMovementLogic
//     {
//         public override void MoveTroop()
//         {
//             CheckAndMoveToNextWaypoint();
//         }
//     }
// }


