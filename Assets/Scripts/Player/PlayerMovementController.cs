using Core;
using Player;
using UnityEngine;

namespace PlayerModule
{
    public class PlayerMovementController : EntityMovementController
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private Transform _playerBody;

        private PlayerWeaponController _playerWeaponController;

        public override void Initialize(params object[] objects)
        {
            base.Initialize(objects);

            _playerWeaponController = objects[2] as PlayerWeaponController;
        }

        protected override void OnMove(Vector2 moveInput)
        {
            if (!IsEnable)
            {
                return;
            }

            var direction = new Vector3(moveInput.x, 0, moveInput.y);
            _rigidbody.MovePosition(_rigidbody.position + direction * _speed * Time.fixedDeltaTime);

            if (_playerWeaponController.CurrentEnemy == null)
            {
                RotateToMovementDirection(moveInput);
            }
            else
            {
                RotateToPoint(_playerWeaponController.CurrentEnemy);
            }
        }

        private void RotateToPoint(Transform target)
        {
            var direction = (target.position - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            _playerBody.rotation = Quaternion.Slerp(_playerBody.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
        }
        
        private void RotateToMovementDirection(Vector2 moveInput)
        {
            var movementDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            
            if (!(movementDirection.magnitude > 0.1f))
            {
                return;
            }
            
            var lookRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}