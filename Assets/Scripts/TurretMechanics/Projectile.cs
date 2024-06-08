using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public int groundLayer; // Assign the layer number for ground troops in the inspector
    public int airLayer; // Assign the layer number for air troops in the inspector

    private int targetLayerMask;

    private void Start()
    {
        // Combine the ground and air layer masks
        targetLayerMask = (1 << groundLayer) | (1 << airLayer);
    }

    private void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is in the target layer mask
        if ((targetLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            Debug.Log("Hit " + other.gameObject.name);
            // Here you can apply damage or other effects to the troop
            // other.gameObject.GetComponent<Troop>().TakeDamage(damageAmount);

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}