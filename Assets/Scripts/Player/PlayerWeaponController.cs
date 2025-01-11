using System.Linq;
using Core;
using Interfaces;
using UnityEngine;
using WeaponSystem.Core;

namespace Player
{
    public class PlayerWeaponController : EntityWeaponController
    {
        [Header("Enemy search")]
        [SerializeField] private float _detectionRadius;
        [SerializeField] private LayerMask _obstacleLayer;
        
        [SerializeField] private WeaponConfig _startWeapon;

        [SerializeField] private bool _isDebug;
        
        public Transform CurrentEnemy { get; private set; }

        public override void Initialize(params object[] objects)
        {
            var entityStats = objects[0] as EntityStats;

            base.Initialize(entityStats);

            EquipWeapon(_startWeapon);
        }

        public override void Deinitialize(params object[] objects)
        {
            foreach (var weapon in _weaponsContainer)
            {
                weapon.Deinitialize();
            }
        }

        public override void UseWeapon()
        {
            if (_currentWeapon == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Weapon is null");
#endif
                return;
            }

            if (!IsEnable)
            {
                return;
            }

            _currentWeapon.UseWeapon();
        }

        private void Update()
        {
            CurrentEnemy = GetNearestEnemy();

            if (CurrentEnemy != null)
            {
                UseWeapon();
            }
        }

        private Transform GetNearestEnemy()
        {
            var collidersInRange = Physics.OverlapSphere(transform.position, _detectionRadius);

            var nearestEnemy = collidersInRange
                .Select(collider => collider.TryGetComponent<EntityStats>(out var entityStats)
                    ? _entityStats.TeamId != entityStats.TeamId ? entityStats : null
                    : null)
                .Where(enemy => enemy != null && !IsObstructed(enemy.transform))
                .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))
                .Select(enemy => enemy.transform)
                .FirstOrDefault();

            return nearestEnemy;
        }
        
        private bool IsObstructed(Transform target)
        {
            var directionToTarget = (target.position - transform.position).normalized;
            var distanceToTarget = Vector3.Distance(transform.position, target.position);

            return Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstacleLayer);
        }
        
        private void OnDrawGizmosSelected()
        {
            if (!_isDebug)
            {
                return;
            }
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        }
    }
}