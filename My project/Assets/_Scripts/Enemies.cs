using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace GGJ {
    
    public class Enemies : MonoBehaviour {
        // Start is called before the first frame update
        public float forceMax;
        public List<Transform> path;
        private NavMeshAgent agent;
        [SerializeField]private int index;

        private void Start() {
            agent = GetComponent<NavMeshAgent>();
            index = 0;
            MovetoLocation();
        }

        private void Update() {
            CheckLocation();
        }

        private void CheckLocation() {
            if (Vector3.Distance(transform.position, path[index].position) < 0.5f) {
                if (index == path.Count-1) {
                    Destroy(gameObject);
                }
                else {
                    index++;
                    MovetoLocation();

                }

            }
        }

        private void MovetoLocation() {
            agent.destination = path[index].position;
            agent.isStopped = false;

        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Throwable")) {
                other.GetComponent<Rigidbody>().AddForce((transform.forward * Random.Range(forceMax,forceMax+10) )+ (Vector3.up*Random.Range(forceMax, forceMax+10)),ForceMode.Impulse);

            }
        }

    }

}