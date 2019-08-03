#pragma warning disable 0649
using Extensions;
using UnityEngine;

namespace Player
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _smooth = 0.3f;
 
        private Vector3 _velocity = Vector3.zero;

        private void Update()
        {
            transform.position = Vector3.SmoothDamp(
                transform.position, _target.position.With(z: -10f),
                ref _velocity, _smooth
            );
        }
    }
}
