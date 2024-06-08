using System.Collections.Generic;
using UnityEngine;

namespace TurretMechanics{
    public abstract class TurretTargeting 
    {   
        #region Fields
        protected float range;
        protected float rateOfFire;
        protected float nextFireTime = 0f;
        protected Transform currentTarget;
        protected List<Transform> targetsInRange = new List<Transform>();

        private int groundLayer;
        private int airLayer;
        #endregion

        public TurretTargeting(float range, float rateOfFire, int groundLayer, int airLayer)
        {
            this.range = range;
            this.rateOfFire = rateOfFire;
            this.groundLayer = 1 << groundLayer; // Convert layer to layer mask
            this.airLayer = 1 << airLayer; // Convert layer to layer mask
        }

        public void UpdateTurret(Vector3 position)
        {
            if (currentTarget == null)
            {
                DetectTargets(position);
            }

            if (currentTarget != null && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / rateOfFire;
            }

            UpdateTargets(position);
        }

        protected void DetectTargets(Vector3 position)
        {
            Collider[] hitColliders = Physics.OverlapSphere(position, range, groundLayer | airLayer);
            foreach (var hitCollider in hitColliders)
            {
                currentTarget = hitCollider.transform;
                Debug.Log(currentTarget.name + " name of target");
                break; // Lock onto the first detected target
            }
        }

        protected void UpdateTargets(Vector3 position)
        {
            if (currentTarget == null || Vector3.Distance(position, currentTarget.position) > range)
            {
                currentTarget = null; // Clear the target if it's out of range or destroyed
            }
        }

        protected abstract void Shoot();
    }


    // Concrete C# class which inherits the TurretTargeting abstract class 
    public class BasicTurret : TurretTargeting
    {
        #region Fields
        // private Transform turretTransform;
        private Transform rotatingPart;
        private GameObject projectilePrefab;
        private float projectileSpeed;
        private float projectileAcceleration;
        #endregion
        
        public BasicTurret(float range, float rateOfFire, int groundLayer, int airLayer, Transform rotatingPart, GameObject projectilePrefab, float projectileSpeed, float projectileAcceleration)
            : base(range, rateOfFire, groundLayer, airLayer)
        {
            this.rotatingPart = rotatingPart;
            this.projectilePrefab = projectilePrefab;
            this.projectileSpeed = projectileSpeed;
            this.projectileAcceleration = projectileAcceleration;
        }

        // protected override void Shoot()
        // {
        //     if (currentTarget != null)
        //     {
        //         // // Rotate the turret to face the target
        //         targetDirection = currentTarget.position - rotatingPart.position;
        //         Quaternion targetRotation = Quaternion.LookRotation(targetDirection); //empty.position
        //         Vector3 rotation = Quaternion.Slerp(rotatingPart.rotation, targetRotation,120f).eulerAngles;
        //         // Smoothly rotate turret towards target
        //         rotatingPart.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);


        //         // Shoot a projectile
        //         GameObject projectile = GameObject.Instantiate(projectilePrefab, rotatingPart.position, rotatingPart.rotation);
        //         Rigidbody rb = projectile.GetComponent<Rigidbody>();
        //         if (rb != null)
        //         {
        //             rb.velocity = targetDirection.normalized * projectileSpeed;
        //         }
        //     }
        // }

        protected override void Shoot()
        {
            if (currentTarget != null)
            {
                // Rotate the turret to face the target
                Vector3 direction = (currentTarget.position - rotatingPart.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                rotatingPart.rotation = Quaternion.Slerp(rotatingPart.rotation, lookRotation, Time.deltaTime * 5f);

                // Shoot a homing projectile
                GameObject projectile = GameObject.Instantiate(projectilePrefab, rotatingPart.position, rotatingPart.rotation);
                HomingProjectile homingProjectile = projectile.GetComponent<HomingProjectile>();
                // Null check for homingprojectile
                homingProjectile?.Initialize(currentTarget, projectileSpeed, projectileAcceleration);
            }
        }
    }

}
