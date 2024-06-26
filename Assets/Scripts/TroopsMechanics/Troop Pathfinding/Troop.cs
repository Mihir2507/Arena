// using UnityEngine;
// using UnityEngine.AI;
// using System.Collections.Generic;

// namespace GraphPathfindingTroops
// {
//     public class MyTroop : MonoBehaviour
//     {
//         #region Fields
//         private TroopMovementLogic troopMovementLogic;

//         [SerializeField] private TroopStats troopStats;
//         [SerializeField] private Transform[] waypoints; // Assign these waypoints in the inspector
//         private const bool CLOCKWISE = true;
//         private const bool ANTI_CLOCKWISE = false;
//         public bool moveForward = false; // Choose the direction of traversal

//         private Graph graph;
//         private GraphPathfinding pathfinding;
//         private float detectionRadius = 5f; // Radius to detect enemies
//         private LayerMask enemyLayer;
//         #endregion

//         private void Start()
//         {
//             if (MomentumPresenter.Instance == null)
//             {
//                 Debug.LogError("No MomentumPresenter instance found.");
//                 return;
//             }

//             // Check if we can deploy the troop
//             if (!MomentumPresenter.Instance.CanDeployTroop(troopStats))
//             {
//                 Debug.LogWarning("Not enough momentum to deploy this troop.");
//                 Destroy(gameObject);
//                 return;
//             }

//             // Deduct momentum and deploy troop
//             MomentumPresenter.Instance.DeployTroop(troopStats);

//             NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
//             if (navMeshAgent == null)
//             {
//                 Debug.LogError("No NavMeshAgent component found on this GameObject.");
//                 return;
//             }

//             // Initialize the graph and pathfinding
//             InitializeGraph();
//             pathfinding = new GraphPathfinding(graph);

//             // Find the path from the nearest node to the target node
//             Vector3 deploymentPosition = transform.position;
//             List<Node> pathNodes = pathfinding.FindPath(deploymentPosition, waypoints[0].position);

//             // Convert node path to a list of Vector3 positions
//             List<Vector3> path = new List<Vector3>();
//             foreach (Node node in pathNodes)
//             {
//                 path.Add(node.Position);
//             }

//             // Initialize the troop movement logic with the path
//             troopMovementLogic = new MyTroopMovementLogic();
//             troopMovementLogic.Initialize(path, navMeshAgent, troopStats.speed);

//             // Set the detection radius and enemy layer
//             detectionRadius = troopStats.range;
//             enemyLayer = LayerMask.GetMask("Enemy");

//             // Destroy the troop after a random duration
//             int randDeath = Random.Range(7, 7);
//             // Invoke("DestroyTroop", randDeath);
//         }

//         private void InitializeGraph()
//         {
//             graph = new Graph();

//             // Create and add nodes to the graph (example coordinates for diamond shape)
//             Node[] nodes = new Node[10];
//             nodes[0] = new Node(new Vector3(0, 0, 10));
//             nodes[1] = new Node(new Vector3(5, 0, 5));
//             nodes[2] = new Node(new Vector3(10, 0, 0));
//             nodes[3] = new Node(new Vector3(15, 0, -5));
//             nodes[4] = new Node(new Vector3(10, 0, -10));
//             nodes[5] = new Node(new Vector3(5, 0, -15));
//             nodes[6] = new Node(new Vector3(0, 0, -10));
//             nodes[7] = new Node(new Vector3(-5, 0, -5));
//             nodes[8] = new Node(new Vector3(-10, 0, 0));
//             nodes[9] = new Node(new Vector3(-5, 0, 5));

//             for (int i = 0; i < nodes.Length; i++)
//             {
//                 graph.AddNode(nodes[i]);
//             }

//             // Connect nodes to form a diamond shape
//             for (int i = 0; i < nodes.Length; i++)
//             {
//                 graph.ConnectNodes(nodes[i], nodes[(i + 1) % nodes.Length]);
//             }
//         }

//         private void DestroyTroop()
//         {
//             Destroy(gameObject);
//         }

//         private void Update()
//         {
            
//             if (!troopMovementLogic.IsEngagingEnemy())
//             {
//                 Debug.Log(troopMovementLogic.IsEngagingEnemy() + " Is engaging");
//                 // Check if the enemy is still in range
//                 Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
//                 if (enemiesInRange.Length == 0)
//                 {
//                     // Enemy out of range or destroyed, resume pathfinding
//                     Vector3 currentPosition = transform.position;
//                     List<Node> pathNodes = pathfinding.FindPath(currentPosition, waypoints[0].position);
//                     List<Vector3> path = new List<Vector3>();
//                     foreach (Node node in pathNodes)
//                     {
//                         path.Add(node.Position);
//                     }
//                     troopMovementLogic.ResumePath(path);
//                 }
//                 else
//                 {
//                     // Update the target position to the enemy's position
//                     troopMovementLogic.EngageEnemy(enemiesInRange[0].transform);
//                 }
//             }
//             else
//             {
//                 // Check for enemies in range
//                 Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
//                 if (enemiesInRange.Length > 0)
//                 {
//                     // Engage the first enemy found
//                     troopMovementLogic.EngageEnemy(enemiesInRange[0].transform);
//                 }
//                 else
//                 {
//                     // Continue following the path
//                     troopMovementLogic.MoveTroop();
//                 }
//             }
//         }

//         private void OnDrawGizmosSelected()
//         {
//             // Draw a sphere to visualize the detection radius
//             Gizmos.color = Color.red;
//             Gizmos.DrawWireSphere(transform.position, detectionRadius);
//         }
//     }
// }


// using UnityEngine;
// using UnityEngine.AI;
// using System.Collections.Generic;

// namespace GraphPathfindingTroops
// {
//     public class Troop : MonoBehaviour
//     {
//         private TroopMovementLogic troopMovementLogic;
//         [SerializeField] private TroopStats troopStats;
//         [SerializeField] private LayerMask enemyLayerMask;
//         private Graph graph;
//         private GraphPathfinding pathfinding;
//         private NavMeshAgent navMeshAgent;
//         private bool chasingEnemy = false;
//         private Transform targetEnemy;
//         private DetectionSystem detectionSystem;

//         private void Start()
//         {
//             if (MomentumPresenter.Instance == null)
//             {
//                 Debug.LogError("No MomentumPresenter instance found.");
//                 return;
//             }

//             // Check if we can deploy the troop
//             if (!MomentumPresenter.Instance.CanDeployTroop(troopStats))
//             {
//                 Debug.LogWarning("Not enough momentum to deploy this troop.");
//                 Destroy(gameObject);
//                 return;
//             }

//             // Deduct momentum and deploy troop
//             MomentumPresenter.Instance.DeployTroop(troopStats);

//             navMeshAgent = GetComponent<NavMeshAgent>();
//             if (navMeshAgent == null)
//             {
//                 Debug.LogError("No NavMeshAgent component found on this GameObject.");
//                 return;
//             }

//             // Get the common graph and initialize pathfinding
//             graph = GameObject.FindObjectOfType<GraphManager>().Graph;
//             pathfinding = new GraphPathfinding(graph);

//             // Find the path from the nearest node to the target node
//             Vector3 deploymentPosition = transform.position;
//             List<Vector3> path = pathfinding.FindPath(deploymentPosition, new Vector3(5, 0, -15));

//             // Initialize the troop movement logic with the path
//             troopMovementLogic = new MyTroopMovementLogic();
//             troopMovementLogic.Initialize(path, navMeshAgent, troopStats.speed);

//             // Initialize the detection system
//             // detectionSystem = new DetectionSystem(troopStats.detectionRange, enemyLayerMask);
//         }

//         private void Update()
//         {
//             if (!chasingEnemy)
//             {
//                 troopMovementLogic.MoveTroop();
//                 DetectEnemies();
//             }
//             else if (targetEnemy != null)
//             {
//                 EngageEnemy(targetEnemy.position);
//             }
//             else
//             {
//                 ReturnToPatrol();
//             }
//         }

//         private void DetectEnemies()
//         {
//             // Transform detectedEnemy = detectionSystem.DetectEnemy(transform.position);
//             // if (detectedEnemy != null)
//             // {
//             //     targetEnemy = detectedEnemy;
//             //     chasingEnemy = true;
//             // }
//         }

//         private void EngageEnemy(Vector3 enemyPosition)
//         {
//             navMeshAgent.SetDestination(enemyPosition);
//             if (Vector3.Distance(transform.position, enemyPosition) > troopStats.detectionRange)
//             {
//                 chasingEnemy = false;
//                 targetEnemy = null;
//                 ReturnToPatrol();
//             }
//         }

//         private void ReturnToPatrol()
//         {
//             Vector3 nearestNodePosition = graph.GetNearestNode(transform.position).Position;
//             List<Vector3> path = pathfinding.FindPath(nearestNodePosition, nearestNodePosition);
//             troopMovementLogic.Initialize(path, navMeshAgent, troopStats.speed);
//             chasingEnemy = false;
//         }

//         private void OnDrawGizmos()
//         {
//             if (graph != null)
//             {
//                 graph.DrawGizmos();
//             }
//         }
//     }
// }

using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace GraphPathfindingTroops
{
    public class Troop : MonoBehaviour
    {
        private TroopMovementLogic troopMovementLogic;
        [SerializeField] public TroopStats troopStats;
        [SerializeField] private LayerMask enemyLayerMask;
        [SerializeField] private Transform towerTarget;
        private Graph graph;
        private GraphPathfinding pathfinding;
        private NavMeshAgent navMeshAgent;
        private bool chasingEnemy = false;
        private Transform targetEnemy;

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

            navMeshAgent = GetComponent<NavMeshAgent>();
            if (navMeshAgent == null || towerTarget == null)
            {
                Debug.LogError("No NavMeshAgent component found on this GameObject. || no towertarget found");
                return;
            }

            // Get the common graph and initialize pathfinding
            graph = GameObject.FindObjectOfType<GraphManager>().Graph;
            pathfinding = new GraphPathfinding(graph);

            // Find the path from the nearest node to the target node
            Vector3 deploymentPosition = transform.position;
            List<Vector3> path = pathfinding.FindPath(deploymentPosition, towerTarget.position);
            Debug.Log(path + " path Points");

            // Initialize the troop movement logic with the path
            troopMovementLogic = new MyTroopMovementLogic();
            troopMovementLogic.Initialize(path, navMeshAgent, troopStats.speed);
        }

        private void Update()
        {
            DetectEnemies();
            if (!chasingEnemy)
            {
                troopMovementLogic.MoveTroop();
                
            }
            else if (targetEnemy != null)
            {
                EngageEnemy(targetEnemy.position);
            }
            else
            {
                ReturnToPatrol();
            }
        }

        private void DetectEnemies()
        {
            Debug.Log("started detecting");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, troopStats.detectionRange, enemyLayerMask);//LayerMask.GetMask("Enemy")
            if (hitColliders.Length > 0)
            {
                Debug.Log("enemy found");
                targetEnemy = hitColliders[0].transform;
                chasingEnemy = true;
            }
        }

        private void EngageEnemy(Vector3 enemyPosition)
        {
            navMeshAgent.SetDestination(enemyPosition);
            if (Vector3.Distance(transform.position, enemyPosition) > troopStats.detectionRange)
            {
                chasingEnemy = false;
                targetEnemy = null;
                ReturnToPatrol();
            }
        }

        private void ReturnToPatrol()
        {
            Vector3 nearestNodePosition = graph.GetNearestNode(transform.position).Position;
            List<Vector3> path = pathfinding.FindPath(nearestNodePosition, towerTarget.position);
            troopMovementLogic.Initialize(path, navMeshAgent, troopStats.speed);
            chasingEnemy = false;
        }

        private void OnDrawGizmos()
        {
            if (graph != null)
            {
                graph.DrawGizmos();
            }
        }
    }
}