#pragma warning disable 0649
using System.Collections;
using Extensions;
using UnityEngine;

namespace Enemy
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField] private float _cooldown;
        [SerializeField] private float _viewDistance;
        [SerializeField] private float _projectileSpeed;

        [SerializeField] private Transform _bow;
        [SerializeField] private GameObject _arrowPrefab;

        private bool _canShoot = true;

        private Transform _player;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            Aim();
        }

        private void Aim()
        {
            Vector3 pos = transform.position;
            Vector3 ray = _player.position - pos;
            RaycastHit2D hit = Physics2D.Raycast(pos, ray, _viewDistance, ~(1 << 9));
            
            if (hit && hit.collider.CompareTag("Player"))
            {
                Vector3 direction = _bow.position - _player.position;
                Rotate(direction);
                
                if (_canShoot)
                {
                    Shoot(direction);
                }
            }
        }

        private void Rotate(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _bow.transform.rotation = _bow.rotation.With(z: angle);
        }

        private void Shoot(Vector3 direction)
        {
            _canShoot = false;
            StartCoroutine(SetShoot());
            
            GameObject arrow = Instantiate(_arrowPrefab, _bow.transform.position, Quaternion.identity);
            arrow.GetComponent<Rigidbody2D>().velocity = -direction.normalized * _projectileSpeed;
            arrow.GetComponent<Projectile>().parent = gameObject;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrow.transform.Rotate(0f, 0f, angle);
            
            Destroy(arrow, 2f);
        }

        private IEnumerator SetShoot()
        {
            yield return new WaitForSeconds(_cooldown);
            _canShoot = true;
        }
    }
}
