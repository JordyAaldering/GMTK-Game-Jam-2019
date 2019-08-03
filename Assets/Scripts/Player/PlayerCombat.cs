#pragma warning disable 0649
using Extensions;
using Projectile;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private float _crosshairDistance;
        [SerializeField] private float _bulletSpeed;

        [SerializeField] private Transform _weapon;
        [SerializeField] private Transform _crosshair;
        [SerializeField] private GameObject _bulletPrefab;

        private SpriteRenderer _weaponSr;
        private Camera _cam;

        private void Awake()
        {
            _weaponSr = _weapon.GetComponent<SpriteRenderer>();
            _cam = Camera.main;
        }

        private void Update()
        {
            Rotate();
            Aim();
        }

        private void Rotate()
        {
            Vector2 weaponPos = _cam.WorldToScreenPoint(_weapon.position);
            float angle = Mathf.Atan2(Input.mousePosition.y - weaponPos.y, Input.mousePosition.x - weaponPos.x);
            angle *= Mathf.Rad2Deg;
            
            if (angle > -90f && angle < 90f)
            {
                _weaponSr.flipX = false;
                _weapon.transform.rotation = _weapon.rotation.With(z: angle);
            }
            else
            {
                _weaponSr.flipX = true;
                _weapon.transform.rotation = _weapon.rotation.With(z: angle - 180f);
            }
        }

        private void Aim()
        {
            if (Input.GetButton("Fire2"))
            {
                _crosshair.gameObject.SetActive(true);

                Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
                Vector2 weaponPos = _weapon.position;
                Vector2 direction = (mousePos - weaponPos).normalized;
                
                _crosshair.localPosition = direction * _crosshairDistance;
                
                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot(direction);
                }
            }
            else
            {
                _crosshair.gameObject.SetActive(false);
            }
        }

        private void Shoot(Vector2 direction)
        {
            GameObject bullet = Instantiate(_bulletPrefab, _weapon.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * _bulletSpeed;
            bullet.GetComponent<Bullet>().parent = gameObject;

            Vector2 objectPos = _cam.WorldToScreenPoint(_weapon.position);
            float posX = Input.mousePosition.x - objectPos.x;
            float posY = Input.mousePosition.y - objectPos.y;
 
            float angle = Mathf.Atan2(posY, posX) * Mathf.Rad2Deg;

            bullet.transform.Rotate(0f, 0f, angle);
            
            Destroy(bullet, 2f);
        }
    }
}
