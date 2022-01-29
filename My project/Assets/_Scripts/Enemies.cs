using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace GGJ {
    
    public class Enemies : MonoBehaviour {
        // Start is called before the first frame update
        [MinMaxSlider(0f,20f)]public Vector2 forceApplied;
        private NavMeshAgent agent;
        public float wanderRadius;
        public float wanderTimer;
        private Transform target;
        private float elapsedTime;

        private void Start() {
            agent = GetComponent<NavMeshAgent>();
            Vector3 wanderPosition = FindRandomWanderPosition(transform.position, wanderRadius);
            agent.SetDestination(wanderPosition);
        }

        private void Update() 
        {
            elapsedTime += Time.deltaTime;
 
            if (elapsedTime >= wanderTimer) 
            {
                Vector3 wanderPosition = FindRandomWanderPosition(transform.position, wanderRadius);
                agent.SetDestination(wanderPosition);
                elapsedTime = 0;
            }
        }

        private static Vector3 FindRandomWanderPosition(Vector3 origin, float distance) 
        {
            Vector3 randomDirection = Random.insideUnitSphere * distance;
            randomDirection += origin;
            NavMeshHit navHit;
            NavMesh.SamplePosition (randomDirection, out navHit, distance, -1);
            return navHit.position;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Throwable"))
            {
                var rb = other.GetComponent<Rigidbody>();
                Vector3 forceToApply = (transform.forward * forceApplied) + (Vector3.up * forceApplied);
                rb.AddForce(forceToApply,ForceMode.Impulse);

            }
        }

    }

}