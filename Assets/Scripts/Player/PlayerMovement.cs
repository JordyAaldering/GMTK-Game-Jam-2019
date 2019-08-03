using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private SpriteRenderer _sr;
        private Animator _anim;

        private void Awake()
        {
            _sr = GetComponentInChildren<SpriteRenderer>();
            _anim = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            Vector3 movement = new Vector3(horizontal, vertical, 0f);
            transform.Translate(movement * Time.deltaTime);

            _sr.flipX = horizontal > 0f;
            _anim.SetBool("IsWalking", movement != Vector3.zero);
        }
    }
}
