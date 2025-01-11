using Interfaces;
using UnityEngine;

namespace Core
{
    public abstract class EntityStats : MonoBehaviour, IInitialize
    {
        [SerializeField] protected float _health;
        
        public abstract int TeamId { get; }

        public virtual bool IsEnable { get; set; } = true;
        
        public virtual void Initialize(params object[] objects)
        {
            
        }
        
        public abstract void TakeDamage(int teamId, float amount);

        //Уничтожение сущности
        public virtual void DestroyEntity()
        {
            
        }
    }
}