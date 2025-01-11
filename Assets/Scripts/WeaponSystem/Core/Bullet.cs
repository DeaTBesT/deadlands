using System;
using Core;
using Interfaces;
using UnityEngine;

namespace WeaponSystem.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IInitialize
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        private int _teamId;
        private float _damage;
        private float _bulletSpeed;
        private Action _onReachTarget;

        public bool IsEnable { get; set; }

        private void OnValidate()
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody2D>();
            }
        }

        public void Initialize(params object[] objects)
        {
            _teamId = (int)objects[0];
            _damage = (float)objects[1];
            _bulletSpeed = (float)objects[2];
            _onReachTarget = objects[4] as Action;

            DestroyDelay((int)objects[3]);
        }

        private void FixedUpdate() =>
            _rigidbody.MovePosition(transform.position + transform.up * _bulletSpeed * Time.fixedDeltaTime);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EntityStats entityStats))
            {
                entityStats.TakeDamage(_teamId, _damage);
            }

            ForceDestroy();
        }

        private void ForceDestroy()
        {
            if (!isActiveAndEnabled)
            {
                return;
            }

            DestroyBullet();
        }

        private void DestroyDelay(int destroyTime) =>
            Invoke(nameof(ForceDestroy), destroyTime);

        private void DestroyBullet()
        {
            _onReachTarget();
            CancelInvoke();
        }
    }
}