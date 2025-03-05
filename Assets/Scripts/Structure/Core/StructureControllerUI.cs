using System;
using DL.InterfacesRuntime;
using DL.StructureRuntime.UIPanels.Core;
using UnityEngine;

namespace DL.StructureRuntime.Core
{
    public abstract class StructureControllerUI : MonoBehaviour, IInitialize, IInteractable
    {
        [SerializeField] protected SimpleStructurePanelUI _generalPanel;
        
        public bool IsEnable { get; set; } = true;
        
        public virtual void Initialize(params object[] objects)
        {
            throw new NotImplementedException();
        }

        public abstract bool TryInteract(Transform interactor);

        public abstract void FinishInteract();
    }
}