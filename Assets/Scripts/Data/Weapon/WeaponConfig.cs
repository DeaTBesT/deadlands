using DL.Data.Core;
using UnityEngine;

namespace DL.Data.Weapon
{
    [CreateAssetMenu(menuName = "Weapon/New weapon config", fileName = "Weapon config")]
    public class WeaponConfig : ItemConfig
    {
        [Header("Stats")]
        [SerializeField] private float _damage = 1f;
        [SerializeField] private float _fireRate = 1f;

        public float Damage => _damage;
        public float FireRate => _fireRate;
    }
}