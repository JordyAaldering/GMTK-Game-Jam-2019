using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Animator _anim;

        private void Awake()
        {
            _anim = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _anim.SetBool("IsWalking", !_anim.GetBool("IsWalking"));
            }
        }
    }
}
