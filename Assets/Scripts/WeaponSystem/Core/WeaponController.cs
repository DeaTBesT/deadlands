using System.Linq;
using DL.CoreRuntime;
using DL.CoreRuntime.Interfaces;
using DL.Data.Weapon;
using UnityEngine;

namespace DL.WeaponSystem.Core
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] protected Weapon[] _weaponsContainer;

        protected EntityStats _entityStats;
        
        protected IWeapon _currentWeapon;

        [field: SerializeField] public virtual bool IsEnable { get; set; } = true;
        
        public virtual void Initialize(params object[] objects)
        {
            _entityStats = objects[0] as EntityStats;
                
            foreach (var weapon in _weaponsContainer)
            {
                weapon.Initialize(objects);
            }
        }
        
        public virtual void Deinitialize(params object[] objects) => 
            throw new System.NotImplementedException();

        public virtual void UseWeapon() => 
            _currentWeapon.UseWeapon();

        public virtual void EquipWeapon(WeaponConfig weaponConfig)
        {
            _currentWeapon = _weaponsContainer.FirstOrDefault(weapon => weapon.GetWeaponConfig == weaponConfig);

            if (_currentWeapon == null)
            {
#if UNITY_EDITOR
                Debug.LogError("None weapon in container");
#endif
                return;
            }
            
            _currentWeapon.GetObject.SetActive(true);
        }

        public virtual void UnequipWeapon()
        {
            if (_currentWeapon == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Equipped weapon is null");
#endif
                return;
            }

            _currentWeapon.GetObject.SetActive(false);
        }
    }
}