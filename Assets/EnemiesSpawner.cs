using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private Transform _normalWorld;
    [SerializeField] private Transform _shadowWorld;

    [SerializeField] private List<Transform> _normalWorldsSpawnPoints;
    [SerializeField] private List<Transform> _shadowWorldsSpawnPoints;

    [SerializeField] private EnemyMovement _normalEnemy;
    [SerializeField] private EnemyMovement _shadowEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out var player))
        {
            var playerTransform = player.GetComponent<Transform>();

            foreach (Transform t in _normalWorldsSpawnPoints)
            {
                var obj = Instantiate(_normalEnemy, t);
                obj.transform.SetParent(_normalWorld);
                obj.SetTarget(playerTransform);
            }
            foreach (Transform t in _shadowWorldsSpawnPoints)
            {
                var obj = Instantiate(_shadowEnemy, t);
                obj.transform.SetParent(_shadowWorld);
                obj.SetTarget(playerTransform);
            }

            Destroy(gameObject);
        }
    }
}
