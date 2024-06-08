using TurretMechanics;
using UnityEngine;


public class TurretBehaviour : MonoBehaviour
{
    #region Fields
    private TurretTargeting turret;

    [SerializeField] private float range = 10f;
    [SerializeField] private float rateOfFire = 1f;
    [SerializeField] private int groundLayer; // Assign the layer number for ground troops in the inspector
    [SerializeField] private int airLayer; // Assign the layer number for air troops in the inspector
    [SerializeField] private Transform rotatingPart; // Assign the rotating part in the inspector
    [SerializeField] private GameObject projectilePrefab; // Assign the projectile prefab in the inspector
    [SerializeField] private float projectileSpeed = 20f; // Speed of the projectile
    [SerializeField] private float projectileAcceleration = 1f;
    #endregion

    private void Start()
    {
        // Initialize the turret with the appropriate subclass and shooting mechanism
        turret = new BasicTurret(range, rateOfFire, groundLayer, airLayer, rotatingPart, projectilePrefab, projectileSpeed, projectileAcceleration);
    }

    private void Update()
    {
        turret.UpdateTurret(transform.position);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(collider.bounds.center, collider.bounds.extents.magnitude);
        }
    }
}



