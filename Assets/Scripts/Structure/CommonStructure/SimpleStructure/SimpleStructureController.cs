using Data;
using DL.CoreRuntime;
using DL.StructureRuntime.Core;
using UnityEngine;

namespace DL.CommonStructure.SimpleStructureRuntime
{
    public class SimpleStructureController : StructureController
    {       
        private StructureControllerUI _structureControllerUI;

        public override void Initialize(params object[] objects) => 
            _structureControllerUI = objects[0] as StructureControllerUI;

        public override void ActivateEntity()
        {
            throw new System.NotImplementedException();
        }

        public override void DiactivateEntity()
        {
            throw new System.NotImplementedException();
        }

        public override bool TryInteract(Transform interactor)
        {
            if (!interactor.TryGetComponent(out EntityStats entityStats))
            {
                return false;
            }

            if (entityStats.TeamId != Teams.PlayerTeamId)
            {
                return false;
            }
            
            _structureControllerUI.TryInteract(interactor);

            return true;
        }

        public override void FinishInteract() => 
            _structureControllerUI.FinishInteract();
    }
}