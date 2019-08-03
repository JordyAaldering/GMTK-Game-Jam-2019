#pragma warning disable 0649
using Extensions;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private GameObject _weapon;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _crosshair;

        [SerializeField] private float _crosshairDistance;
        [SerializeField] private float _bulletSpeed;

        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;
            
            RotateWeapon(mouseX, mouseY);
            AimCrosshair(mouseX, mouseY);
        }

        private void RotateWeapon(float mouseX, float mouseY)
        {
            Vector3 objectPos = _cam.WorldToScreenPoint (transform.position);
            float posX = mouseX - objectPos.x;
            float posY = mouseY - objectPos.y;
 
            float angle = Mathf.Atan2(posY, posX) * Mathf.Rad2Deg;
            _weapon.transform.rotation = _weapon.transform.rotation.With(z: angle);
        }

        private void AimCrosshair(float mouseX, float mouseY)
        {
            if (Input.GetButton("Fire2"))
            {
                _crosshair.SetActive(true);
                
                float posX = mouseX / Screen.width;
                float posY = mouseY / Screen.height;
                Vector3 aim = new Vector3(posX - 0.5f, posY - 0.5f, 0f).normalized;
                
                _crosshair.transform.localPosition = aim * _crosshairDistance;
                
                if (Input.GetButtonDown("Fire1"))
                {
                    ShootBullet(aim);
                }
            }
            else
            {
                _crosshair.SetActive(false);
            }
        }

        private void ShootBullet(Vector3 direction)
        {
            GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * _bulletSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.Rotate(0f, 0f, angle);
            
            Destroy(bullet, 2f);
        }
    }
}
