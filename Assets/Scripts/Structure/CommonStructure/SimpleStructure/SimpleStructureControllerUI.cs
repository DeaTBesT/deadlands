using DL.StructureRuntime.Core;
using UnityEngine;

namespace DL.CommonStructure.SimpleStructureRuntime
{
    public class SimpleStructureControllerUI : StructureControllerUI
    {
        public override void Initialize(params object[] objects) => 
            _generalPanel.Hide();

        public override bool TryInteract(Transform interactor)
        {
            _generalPanel.Show();
            return true;
        }

        public override void FinishInteract() => 
            _generalPanel.Hide();
    }
}