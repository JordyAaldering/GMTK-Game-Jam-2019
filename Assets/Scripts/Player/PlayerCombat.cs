#pragma warning disable 0649
using Extensions;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private Transform _weapon;
        [SerializeField] private Transform _crosshair;
        [SerializeField] private GameObject _bulletPrefab;

        [SerializeField] private float _crosshairDistance;
        [SerializeField] private float _bulletSpeed;

        private SpriteRenderer _weaponSr;
        private Camera _cam;

        private void Awake()
        {
            _weaponSr = _weapon.GetComponent<SpriteRenderer>();
            _cam = Camera.main;
        }

        private void Update()
        {
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;
            
            Rotate(mouseX, mouseY);
            Aim(mouseX, mouseY);
        }

        private void Rotate(float mouseX, float mouseY)
        {
            Vector3 objectPos = _cam.WorldToScreenPoint(_weapon.position);
            float posX = mouseX - objectPos.x;
            float posY = mouseY - objectPos.y;
 
            float angle = Mathf.Atan2(posY, posX) * Mathf.Rad2Deg;

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

        private void Aim(float mouseX, float mouseY)
        {
            if (Input.GetButton("Fire2"))
            {
                _crosshair.gameObject.SetActive(true);
                
                float posX = mouseX / Screen.width;
                float posY = mouseY / Screen.height;

                Vector3 aim = new Vector3(posX - 0.5f, posY - 0.5f, 0f).normalized;
                
                _crosshair.localPosition = aim * _crosshairDistance;
                
                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot(aim);
                }
            }
            else
            {
                _crosshair.gameObject.SetActive(false);
            }
        }

        private void Shoot(Vector3 direction)
        {
            GameObject bullet = Instantiate(_bulletPrefab, _weapon.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * _bulletSpeed;
            bullet.GetComponent<Bullet>().parent = gameObject;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.Rotate(0f, 0f, angle);
            
            Destroy(bullet, 2f);
        }
    }
}
