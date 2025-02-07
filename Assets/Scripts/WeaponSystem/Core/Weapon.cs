using DL.CoreRuntime;
using DL.CoreRuntime.Interfaces;
using DL.Data.Weapon;
using DL.InterfacesRuntime;
using UnityEngine;

namespace DL.WeaponSystem.Core
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] protected WeaponConfig _weaponConfig;

        protected float _nextAttackTime;

        protected EntityStats _entityStats;

        public WeaponConfig GetWeaponConfig => _weaponConfig;
        public GameObject GetObject => gameObject;

        public bool IsEnable { get; set; }

        public virtual void Initialize(params object[] objects) =>
            _entityStats = objects[0] as EntityStats;

        public virtual void Deinitialize()
        {
        }

        public abstract void UseWeapon();
    }
}