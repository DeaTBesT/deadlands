using Core;
using UnityEngine;

namespace PlayerModule
{
    public class PlayerMovementController : EntityMovementController
    {
        private const float ADDED_ANGLE = 90f;

        [SerializeField] private float _speed = 5f;
        [SerializeField] private Transform _playerBody;
        
        protected override void OnMove(Vector2 moveInput)
        {
            if (!IsEnable)
            {
                return;
            }

            var vectorInput = new Vector3(moveInput.x, 0, moveInput.y);
            _rigidbody.MovePosition(_rigidbody.position + vectorInput * _speed * Time.fixedDeltaTime);
        }

        protected override void OnMousePosition(Ray ray)
        {
            if (!IsEnable)
            {
                return;
            }

            var groundPlane = new Plane(Vector3.up, Vector3.zero);

            if (groundPlane.Raycast(ray, out float distance))
            {
                var targetPoint = ray.GetPoint(distance);

                var direction = targetPoint - transform.position;
                direction.y = 0;

                if (direction.magnitude > 0.1f)
                {
                    var targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
                }
            }
        }
    }
}

