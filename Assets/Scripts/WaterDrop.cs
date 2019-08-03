using Player;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerManager>().waterDrops++;
            Destroy(gameObject);
        }
    }
}
