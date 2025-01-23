using DL.Data.Weapon;
using DL.InterfacesRuntime;
using UnityEngine;

namespace DL.CoreRuntime.Interfaces
{
    public interface IWeapon : IInitialize
    {
        public WeaponConfig GetWeaponConfig { get; }

        public GameObject GetObject { get; }
        
        void UseWeapon();
    }
}