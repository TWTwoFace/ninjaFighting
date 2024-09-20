using System;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private int poolCapacity;
    [SerializeField] private Enemy prefab;
    [SerializeField] private Transform target;
    
    private PoolMono<Enemy> pool;

    private void Awake()
    {
        pool = new PoolMono<Enemy>(prefab, poolCapacity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var enemyObject = pool.GetFreeElement();
            enemyObject.Init(target);
        }
    }
}
