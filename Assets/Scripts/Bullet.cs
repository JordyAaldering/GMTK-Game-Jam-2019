using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public GameObject parent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != parent)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            else if (other.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
            else if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }
}
