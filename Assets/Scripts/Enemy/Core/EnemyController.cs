using DL.CoreRuntime;
using UnityEngine;
using UnityEngine.AI;

namespace DL.StructureRuntime.UIPanels.Core
{
    public class EnemyController : EntityController
    {
        private Transform _player;
        private NavMeshAgent _navMeshAgent;
        
        public override void Initialize(params object[] objects)
        {
            _player = objects[0] as Transform;
            _navMeshAgent = objects[1] as NavMeshAgent;
        }

        public override void ActivateEntity()
        {
            throw new System.NotImplementedException();
        }

        public override void DiactivateEntity()
        {
            throw new System.NotImplementedException();
        }

        private void Update() => 
            _navMeshAgent.SetDestination(_player.position);
    }
}