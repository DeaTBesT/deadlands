using System;
using Managers;
using UnityEngine;
using WeaponSystem.Core;

namespace WeaponSystem.Projectile
{
    public class ProjectileWeapon : Weapon
    {
        [SerializeField] private Transform _spawnPivot;

        private PrefabPool _prefabPool;
        private ProjectileWeaponConfig _projectileConfig;

        public override void Initialize(params object[] objects)
        {
            base.Initialize(objects);

            _projectileConfig = (ProjectileWeaponConfig)_weaponConfig;
            _prefabPool = PrefabPoolManager.Instance.GetPool(_projectileConfig.TypePool);
        }

        public override void UseWeapon()
        {
            if (Time.time < _nextAttackTime)
            {
                return;
            }

            _nextAttackTime = Time.time + 1f / _weaponConfig.FireRate;

            var bulletObject = _prefabPool.Get(_spawnPivot);
            bulletObject.transform.position = _spawnPivot.position;
            bulletObject.transform.rotation = _spawnPivot.rotation;

            if (!bulletObject.TryGetComponent(out Bullet bullet))
            {
                return;
            }

            void OnReachTarget() => 
                _prefabPool.Return(bulletObject.gameObject);

            bullet.Initialize(_entityStats.TeamId, _projectileConfig.Damage, _projectileConfig.BulletSpeed,
                _projectileConfig.DestroyTime, (Action)OnReachTarget);
        }
    }
}