#pragma warning disable 0649
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private Animator _anim;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponentInChildren<SpriteRenderer>();
            _anim = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 movement = new Vector2(
                Input.GetAxis("Horizontal"), 
                Input.GetAxis("Vertical")
            );
            
            _rb.velocity = movement * _moveSpeed;

            Animate(movement);
        }

        private void Animate(Vector2 movement)
        {
            if (movement != Vector2.zero)
            {
                _anim.SetBool("IsWalking", true);
                if (Mathf.Abs(movement.x) > 0f)
                {
                    _sr.flipX = movement.x > 0f;
                }
            }
            else
            {
                _anim.SetBool("IsWalking", false);
            }
        }
    }
}
