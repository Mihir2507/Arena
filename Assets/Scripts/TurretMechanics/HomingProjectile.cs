using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    #region Fields
    private Transform target;
    private float speed;
    [SerializeField] private float damage = 10f;

    private float acceleration;
    #endregion

    public void Initialize(Transform target, float speed, float acceleration)
    {
        this.target = target;
        this.speed = speed;
        this.acceleration = acceleration;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        speed += acceleration * Time.deltaTime;

        // Move towards the target
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Rotate to face the target
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            // Apply damage to the target
            // Example: other.GetComponent<Troop>().TakeDamage(damage);

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
