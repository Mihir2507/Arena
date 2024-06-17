
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

    [SerializeField] private TroopStats troopStats;
    [SerializeField] private Transform[] waypoints; // Assign these waypoints in the inspector
    private const bool CLOCKWISE = true;
    private const bool ANTI_CLOCKWISE = false;
    public bool moveForward = false; // Choose the direction of traversal
    #endregion
    
    private void Start()
    {
        if (MomentumPresenter.Instance == null)
        {
            Debug.LogError("No MomentumPresenter instance found.");
            return;
        }

        // Check if we can deploy the troop
        if (!MomentumPresenter.Instance.CanDeployTroop(troopStats))
        {
            Debug.LogWarning("Not enough momentum to deploy this troop.");
            Destroy(gameObject);
            return;
        }

        // Deduct momentum and deploy troop
        MomentumPresenter.Instance.DeployTroop(troopStats);

        
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("No NavMeshAgent component found on this GameObject.");
            return;
        }

        // Initialize the troop movement logic with specific logic
        troopMovementLogic = new MyTroopMovementLogic();
        troopMovementLogic.Initialize(waypoints, navMeshAgent, moveForward, troopStats.speed);
        int randDeath = Random.Range(7,7);
        Invoke("DestroyTroop", randDeath);
    }

    private void OnKill()
    {
        if (OverdrivePresenter.Instance != null)
        {
            OverdrivePresenter.Instance.IncreaseOverdrive(3f); // Adjust the amount as needed
        }
    }

    private void DestroyTroop(){
        OnKill();
        Destroy(gameObject);
    }
    private void Update()
    {
        // Call the MoveTroop method from the troop movement logic
        troopMovementLogic.MoveTroop();
    }
}
