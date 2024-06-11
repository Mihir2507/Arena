using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

namespace TroopsMechanics{
    public abstract class TroopMovementLogic
    {
        #region Field
        protected NavMeshAgent navMeshAgent;
        protected Transform[] waypoints;
        protected int currentWaypointIndex = 0;
        protected bool moveForward = true; // Flag to indicate the direction of movement
        protected float speed;
        #endregion
        
        // protected abstract Transform[] DetermineWaypointSequence(Transform[] originalWaypoints);
        public void Initialize(Transform[] originalWaypoints, NavMeshAgent agent, bool forwardDirection, float troopSpeed)
        {
            waypoints = originalWaypoints;//DetermineWaypointSequence(originalWaypoints);
            navMeshAgent = agent;
            moveForward = forwardDirection;
            this.speed = troopSpeed;
            agent.speed = speed;

            // Find the closest waypoint
            currentWaypointIndex = FindClosestWaypointIndex();

            // Skip the closest waypoint to go to the next one
            UpdateWaypointIndex();

            MoveToNextWaypoint();
        }

        public abstract void MoveTroop();

        protected void MoveToNextWaypoint()
        {
            if (waypoints.Length == 0)
            {
                Debug.LogWarning("No waypoints assigned.");
                return;
            }

            // Set destination to the current waypoint
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }

        protected void UpdateWaypointIndex()
        {
            if (moveForward)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
            else
            {
                currentWaypointIndex = (currentWaypointIndex - 1 + waypoints.Length) % waypoints.Length;
            }
        }

        private int FindClosestWaypointIndex()
        {
            int closestIndex = 0;
            float closestDistance = float.MaxValue;

            for (int i = 0; i < waypoints.Length; i++)
            {
                float distance = Vector3.Distance(navMeshAgent.transform.position, waypoints[i].position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }

        protected void CheckAndMoveToNextWaypoint()
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                UpdateWaypointIndex();
                MoveToNextWaypoint();
            }
        }
    }

    public class MyTroopMovementLogic : TroopMovementLogic
    {

        // protected override Transform[] DetermineWaypointSequence(Transform[] originalWaypoints)
        // {
        //     // Implement your logic to determine the sequence of waypoints
        //     // For example, you can shuffle the waypoints or use a specific order
        //     // Here we just return the original waypoints without any changes

        //     // Example logic: return waypoints as is
        //     return originalWaypoints;
        // }

        public override void MoveTroop()
        {
            // Check if reached the current waypoint and move to the next one
            CheckAndMoveToNextWaypoint();
        }
    }
}