using UnityEngine;
using UnityEngine.AI;
using GraphPathfindingTroops;
using System.Collections.Generic;

namespace GraphPathfindingTroops{
    public class MyTroop : MonoBehaviour
    {
        #region Fields
        private TroopMovementLogic troopMovementLogic;

        [SerializeField] private TroopStats troopStats;
        [SerializeField] private Transform[] waypoints; // Assign these waypoints in the inspector
        private const bool CLOCKWISE = true;
        private const bool ANTI_CLOCKWISE = false;
        public bool moveForward = false; // Choose the direction of traversal

        private Graph graph;
        private GraphPathfinding pathfinding;
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

            // Initialize the graph and pathfinding
            InitializeGraph();
            pathfinding = new GraphPathfinding(graph);

            // Find the path from the nearest node to the target node
            Vector3 deploymentPosition = transform.position;
            List<Node> pathNodes = pathfinding.FindPath(deploymentPosition, waypoints[0].position);

            // Convert node path to a list of Vector3 positions
            List<Vector3> path = new List<Vector3>();
            foreach (Node node in pathNodes)
            {
                path.Add(node.Position);
            }

            // Initialize the troop movement logic with the path
            troopMovementLogic = new MyTroopMovementLogic();
            troopMovementLogic.Initialize(path, navMeshAgent, troopStats.speed);

            // Destroy the troop after a random duration
            int randDeath = Random.Range(7, 7);
            // Invoke("DestroyTroop", randDeath);
        }

        private void InitializeGraph()
        {
            graph = new Graph();

            // Create and add nodes to the graph (example coordinates for diamond shape)
            Node[] nodes = new Node[10];
            nodes[0] = new Node(new Vector3(0, 0, 10));
            nodes[1] = new Node(new Vector3(5, 0, 5));
            nodes[2] = new Node(new Vector3(10, 0, 0));
            nodes[3] = new Node(new Vector3(15, 0, -5));
            nodes[4] = new Node(new Vector3(10, 0, -10));
            nodes[5] = new Node(new Vector3(5, 0, -15));
            nodes[6] = new Node(new Vector3(0, 0, -10));
            nodes[7] = new Node(new Vector3(-5, 0, -5));
            nodes[8] = new Node(new Vector3(-10, 0, 0));
            nodes[9] = new Node(new Vector3(-5, 0, 5));

            for (int i = 0; i < nodes.Length; i++)
            {
                graph.AddNode(nodes[i]);
            }

            // Connect nodes to form a diamond shape
            for (int i = 0; i < nodes.Length; i++)
            {
                graph.ConnectNodes(nodes[i], nodes[(i + 1) % nodes.Length]);
            }
        }

        private void DestroyTroop()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            troopMovementLogic.MoveTroop();
        }
    }
}


// using UnityEngine;
// using UnityEngine.AI;
// using TroopsMechanics;
// using System.Collections.Generic;

// namespace GraphPathfindingTroops{
//     public class MyTroop : MonoBehaviour
//     {
//         private TroopMovementLogic troopMovementLogic;

//         [SerializeField] private TroopStats troopStats;
//         private const bool CLOCKWISE = true;
//         private const bool ANTI_CLOCKWISE = false;
//         private Graph graph;
//         private GraphPathfinding pathfinding;

//         private void Start()
//         {
//             if (MomentumPresenter.Instance == null)
//             {
//                 Debug.LogError("No MomentumPresenter instance found.");
//                 return;
//             }

//             if (!MomentumPresenter.Instance.CanDeployTroop(troopStats))
//             {
//                 Debug.LogWarning("Not enough momentum to deploy this troop.");
//                 Destroy(gameObject);
//                 return;
//             }

//             MomentumPresenter.Instance.DeployTroop(troopStats);

//             NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
//             if (navMeshAgent == null)
//             {
//                 Debug.LogError("No NavMeshAgent component found on this GameObject.");
//                 return;
//             }

//             graph = new Graph();
//             pathfinding = new GraphPathfinding(graph);

//             // Generate graph nodes and connections here (omitted for brevity)

//             // Initialize the graph and pathfinding
//             InitializeGraph();
//             pathfinding = new GraphPathfinding(graph);

//             // Find the path from the nearest node to the target node
//             Vector3 deploymentPosition = transform.position;
//             // List<Node> pathNodes = pathfinding.FindPath(deploymentPosition, waypoints[0].position);

//             // Convert node path to a list of Vector3 positions
//             // List<Vector3> path = new List<Vector3>();
//             // foreach (Node node in pathNodes)
//             // {
//             //     path.Add(node.Position);
//             // }
//             // Find the path from the current position to the target position
//             Vector3 targetPosition = new Vector3(10,0,-10); // Set the desired target position
//             List<Node> path = pathfinding.FindPath(transform.position, targetPosition);

//             troopMovementLogic = new MyTroopMovementLogic();
//             troopMovementLogic.Initialize(path, navMeshAgent, troopStats.speed);

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

//         private void OnKill()
//         {
//             if (OverdrivePresenter.Instance != null)
//             {
//                 OverdrivePresenter.Instance.IncreaseOverdrive(3f);
//             }
//         }

//         private void DestroyTroop()
//         {
//             OnKill();
//             Destroy(gameObject);
//         }

//         private void Update()
//         {
//             troopMovementLogic.MoveTroop();
//         }
//     }
// }
