using DL.CoreRuntime;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

namespace DL.StructureRuntime.UIPanels.Core
{
    public class EnemyInitializer : EntityInitializer
    {
        [SerializeField] private EntityStats _entityStats;
        [SerializeField] private EntityController _entityController;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        [ShowNonSerializedField] private Transform _player;
        
        private void OnValidate()
        {
            if (_entityController == null)
            {
                _entityController = GetComponent<EnemyController>();
            }  
            
            if (_entityStats == null)
            {
                _entityStats = GetComponent<EntityStats>();
            }  
            
            if (_navMeshAgent == null)
            {
                _navMeshAgent = GetComponent<NavMeshAgent>();
            } 
        }
        
        public override void Initialize(params object[] objects)
        {
            _player = objects[0] as Transform;
            
            _entityStats.Initialize();
            _entityController.Initialize(_player, _navMeshAgent);
        }
    }
}