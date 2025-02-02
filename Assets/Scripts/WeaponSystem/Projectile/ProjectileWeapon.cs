using System;
using DL.ManagersRuntime;
using DL.WeaponSystem.Core;
using UnityEngine;

namespace DL.WeaponSystemRuntime.Projectile
{
    public class ProjectileWeapon : Weapon
    {
        [SerializeField] private Transform _spawnPivot;

        private PrefabPool _prefabPool;
        private ProjectileWeaponConfig _projectileConfig;

        public override void Initialize(params object[] objects)
        {
            base.Initialize(objects);

            var prefabPoolManager = objects[1] as PrefabPoolManager;
            
            _projectileConfig = (ProjectileWeaponConfig)_weaponConfig;
            _prefabPool = prefabPoolManager.GetPool(_projectileConfig.TypePool);
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

            bullet.Initialize(_entityStats.TeamId, _projectileConfig.Damage, _projectileConfig.BulletSpeed,
                _projectileConfig.DestroyTime, (Action)OnReachTarget);
            
            return;

            void OnReachTarget() =>
                _prefabPool.Return(bulletObject);
        }
    }
}