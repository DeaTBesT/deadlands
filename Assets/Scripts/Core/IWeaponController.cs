using DL.Data.Weapon;
using DL.InterfacesRuntime;

namespace DL.CoreRuntime
{
    public interface IWeaponController : IInitialize, IDeinitialize
    {
        void UseWeapon();

        void EquipWeapon(WeaponConfig weaponConfig);

        void UnequipWeapon();
    }
}