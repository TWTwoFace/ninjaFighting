using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
        [SerializeField] private NavMeshAgent agent; 
        private Transform target;

        public void Init(Transform target)
        {
                this.target = target;
        }

        private void Dispose()
        {
                this.target = null;
                gameObject.SetActive(false);
        }
        
        private void Update()
        {
                agent.SetDestination(target.position);
        }
}