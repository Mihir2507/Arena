
#region Previous Commented code
// using UnityEngine;
// using UnityEngine.AI;
// using TroopsMechanics;

// public class MyTroop : TroopsMovement
// {
//     // protected NavMeshAgent agent;

//     // private void Start()
//     // {
//     //     navMeshAgent = GetComponent<NavMeshAgent>();

//     //     // Initialize current waypoint to the first one
//     //     if (waypoints.Count > 0)
//     //     {
//     //         MoveToNextWaypoint();
//     //     }
//     // }

//     private void Update() {
//         MoveTroop();
//     }
//     // Example usage of MoveTroop method
//     // void MoveTroopManually()
//     // {
//     //     MoveTroop(); // Call the MoveTroop method to move the troop
//     // }
// }
#endregion

using UnityEngine;
using UnityEngine.AI;
using TroopsMechanics;

public class MyTroop : MonoBehaviour
{
    #region Fields
    private TroopMovementLogic troopMovementLogic;
    [SerializeField] private Transform[] waypoints; // Assign these waypoints in the inspector
    private const bool CLOCKWISE = true;
    private const bool ANTI_CLOCKWISE = false;
    public bool moveForward = false; // Choose the direction of traversal
    #endregion
    
    private void Start()
    {
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("No NavMeshAgent component found on this GameObject.");
            return;
        }

        // Initialize the troop movement logic with specific logic
        troopMovementLogic = new MyTroopMovementLogic();
        troopMovementLogic.Initialize(waypoints, navMeshAgent, moveForward);
        int randDeath = Random.Range(25,35);
        Invoke("DestroyTroop", randDeath);
    }

    private void DestroyTroop(){
        Destroy(gameObject);
    }
    private void Update()
    {
        // Call the MoveTroop method from the troop movement logic
        troopMovementLogic.MoveTroop();
    }
}
