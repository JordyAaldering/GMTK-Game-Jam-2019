#pragma warning disable 0649
using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private GameObject _waterDrop;
        
        public void Die()
        {
            GameObject drop = Instantiate(_waterDrop, transform.position, Quaternion.identity);
            drop.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.value * 2f - 1f, Random.value * 2f - 1f));

            FindObjectOfType<SpawnManager>()._spawned--;
            
            Destroy(gameObject);
        }
    }
}
