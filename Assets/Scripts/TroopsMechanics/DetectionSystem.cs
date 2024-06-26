using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask enemyLayer;

    // public DetectionSystem(float detectionRange, LayerMask enemyLayer)
    // {
    //     this.detectionRange = detectionRange;
    //     this.enemyLayer = enemyLayer;
    // }

    private void Update(){
        DetectEnemy();
    }
    public Transform DetectEnemy()
    {
        Debug.Log("detecting");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, enemyLayer);
        foreach (var colliders in hitColliders)
        {   
            Debug.Log(colliders + " 1");
        }
        if (hitColliders.Length > 0)
        {
            Debug.Log(hitColliders + " detected");
            return hitColliders[0].transform;
        }
        return null;
    }
}