#pragma warning disable 0649
using Extensions;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private float _crosshairDistance;
        [SerializeField] private float _projectileSpeed;

        [SerializeField] private Transform _crosshair;
        [SerializeField] private Transform _rainbowPos;
        [SerializeField] private GameObject _rainbowPrefab;

        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            Aim();
        }

        private void Aim()
        {
            if (Input.GetButton("Fire2"))
            {
                _crosshair.gameObject.SetActive(true);

                Vector2 pos = _rainbowPos.position;
                Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (mousePos - pos).normalized;
                
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
            Vector2 pos = _rainbowPos.position;
            Vector2 mouthPos = _cam.WorldToScreenPoint(pos);
            
            float x = Input.mousePosition.x - mouthPos.x;
            float y = Input.mousePosition.y - mouthPos.y;
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            
            GameObject rainbow = Instantiate(_rainbowPrefab, pos, Quaternion.identity.With(z: angle));
            rainbow.GetComponent<Rigidbody2D>().velocity = direction * _projectileSpeed;
            rainbow.GetComponent<Projectile>().parent = gameObject;

            Destroy(rainbow, 2f);
        }
    }
}
