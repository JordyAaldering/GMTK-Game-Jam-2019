using Enemy;
using Player;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public GameObject parent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != parent)
        {
            if (other.CompareTag("Player"))
            {
                FindObjectOfType<PlayerManager>().Die();
                Destroy(gameObject);
            }
            else if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyManager>().Die();
                Destroy(gameObject);
            }
            else if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }
}
