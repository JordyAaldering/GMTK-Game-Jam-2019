#pragma warning disable 0649
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _viewDistance;
        [SerializeField] private float _minDistance;

        private Transform _player;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _rb = GetComponent<Rigidbody2D>();
        }
        
        private void Update()
        {
            Look();
        }

        private void Look()
        {
            Vector3 pos = transform.position;
            Vector3 playerPos = _player.position;
            
            if (Vector2.Distance(pos, playerPos) > _minDistance)
            {
                Vector3 ray = playerPos - pos;
                RaycastHit2D hit = Physics2D.Raycast(pos, ray, _viewDistance, ~(1 << 9));

                if (hit && hit.collider.CompareTag("Player"))
                {
                    Move();
                }
            }
            else
            {
                _rb.velocity = Vector2.zero;
            }
        }

        private void Move()
        {
            Vector2 direction = (_player.position - transform.position).normalized;
            _rb.velocity = direction * _moveSpeed;
        }
    }
}
