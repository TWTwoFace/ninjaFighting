using UnityEngine;

public class PlayerWeaponAttack : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyHealth>(out var enemyHealth))
        {
            enemyHealth.TakeDamage(_damage);
            print("damaged");
        }
    }
}
