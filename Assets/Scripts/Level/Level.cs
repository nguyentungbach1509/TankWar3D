using Tank;
using UnityEngine;
using UnityEngine.AI;

namespace Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform minPoint,maxPoint;
        
        
        public Vector3 GetRandomPosition()
        {   
            Vector3 randPoint = Random.Range(minPoint.position.x, maxPoint.position.x) * Vector3.right + Random.Range(minPoint.position.z, maxPoint.position.z) * Vector3.forward;

            NavMeshHit hit;

            NavMesh.SamplePosition(randPoint, out hit, float.PositiveInfinity, 1);

            return hit.position;
        }
    }
}
