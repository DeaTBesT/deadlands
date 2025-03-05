using System;
using DL.InterfacesRuntime;
using NaughtyAttributes;
using UnityEngine;

namespace DL.CoreRuntime
{
    public abstract class EntityStats : MonoBehaviour, IInitialize
    {
        [SerializeField] protected float _startHealth;

        [ProgressBar("Health", nameof(_startHealth), EColor.Red), SerializeField]
        protected float _currentHealth;

        public Action OnDeath { get; set; }

        public abstract int TeamId { get; }

        public virtual bool IsEnable { get; set; } = true;

        public virtual void Initialize(params object[] objects) =>
            _currentHealth = _startHealth;

        public abstract bool TryApplyDamage(int teamId, float amount);

        //Уничтожение сущности
        public virtual void DestroyEntity() =>
            OnDeath?.Invoke();
    }
}