using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace GGJ {
    
    public class GhostController : MonoBehaviour {
        // Start is called before the first frame update
        [MinMaxSlider(0f,20f)]public Vector2 forceToApply;
        private NavMeshAgent agent;
        public float ghostSpeed=14;
        public float ghostAngularSpeed=360;
        private Transform target;
        private float elapsedTime;

        private void Start() 
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = ghostSpeed;
            agent.angularSpeed = ghostAngularSpeed;
            Vector3 wanderPosition = FindRandomWanderPosition();
            agent.SetDestination(wanderPosition);
        }

        private void Update() 
        {
            if (agent.remainingDistance <= 0.5f) 
            {
                Vector3 wanderPosition = FindRandomWanderPosition();
                agent.SetDestination(wanderPosition);
            }
        }

        private static Vector3 FindRandomWanderPosition() 
        {
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
 
            int maxIndices = navMeshData.indices.Length - 3;
            // Pick the first indice of a random triangle in the nav mesh
            int firstVertexSelected = Random.Range(0, maxIndices);
            int secondVertexSelected = Random.Range(0, maxIndices);
            //Spawn on Verticies
            Vector3 point = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
 
            Vector3 firstVertexPosition = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
            Vector3 secondVertexPosition = navMeshData.vertices[navMeshData.indices[secondVertexSelected]];
            //Eliminate points that share a similar X or Z position to stop spawining in square grid line formations
            if ((int)firstVertexPosition.x == (int)secondVertexPosition.x || (int)firstVertexPosition.z == (int)secondVertexPosition.z)
            {
                point = FindRandomWanderPosition();
            }
            else
            {
                // Select a random point on it
                point = Vector3.Lerp(firstVertexPosition, secondVertexPosition,Random.Range(0.05f, 0.95f));
            }
            return point;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Throwable"))
            {
                var rb = other.GetComponent<Rigidbody>();
                Vector3 forceToApply = (transform.forward * this.forceToApply) + (Vector3.up * this.forceToApply);
                rb.AddForce(forceToApply,ForceMode.Impulse);

            }
        }

    }

}