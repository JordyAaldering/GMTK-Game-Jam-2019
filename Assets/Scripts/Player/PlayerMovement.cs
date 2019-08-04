#pragma warning disable 0649
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _runSpeed;
        
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private Animator _anim;
        
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

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

            bool isRunning = Input.GetAxisRaw("Run") > 0f;
            _rb.velocity = movement * (isRunning ? _runSpeed : _moveSpeed);

            Animate(movement, isRunning);
        }

        private void Animate(Vector2 movement, bool isRunning)
        {
            if (movement != Vector2.zero)
            {
                _anim.SetBool(IsWalking, true);
                _anim.SetBool(IsRunning, isRunning);
                
                if (Mathf.Abs(movement.x) > 0f)
                {
                    _sr.flipX = movement.x > 0f;
                }
            }
            else
            {
                _anim.SetBool(IsWalking, false);
                _anim.SetBool(IsRunning, false);
            }
        }
    }
}
