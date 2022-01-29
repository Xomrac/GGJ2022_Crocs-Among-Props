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
        public Vector3 path;
        private NavMeshAgent agent;
        public GameObject wallDx;
        public GameObject wallSx;
        public GameObject wallUp;
        public GameObject wallDown;

        private void Start() {
            agent = GetComponent<NavMeshAgent>();
            MovetoLocation();
        }

        private void Update() {
            CheckLocation();
        }

        private void CheckLocation() {
            if (Vector3.Distance(transform.position, path) < 0.5f) {
                MovetoLocation();

            }
        }

        private void MovetoLocation() {
            path= new Vector3(Random.Range(wallSx.transform.position.x,wallDx.transform.position.x),transform.position.y,Random.Range(wallDown.transform.position.z,wallUp.transform.position.z));
            agent.destination = path;
            agent.isStopped = false;

        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Throwable")) {
                other.GetComponent<Rigidbody>().AddForce((transform.forward * Random.Range(forceMax,forceMax+10) )+ (Vector3.up*Random.Range(forceMax, forceMax+10)),ForceMode.Impulse);

            }
        }

    }

}