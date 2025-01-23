using System;
using System.Collections.Generic;
using System.Linq;
using DL.Data.Resource;
using DL.EnumsRuntime;
using DL.StructureRuntime.Core;
using DL.StructureRuntime.Model;
using UnityEngine;

namespace DL.ConstructibleStructureRuntime.Core
{
    public abstract class ConstructibleStructureController : StructureController
    {
        private const int StartUpgradeLevel = 1;
        
        [SerializeField] private List<ResourceDataModel> _requiredResourcesBuild;
        [SerializeField] private List<RequiredResourcesModel> _requiredResourcesUpgrade;

        [Header("Optional")] 
        [SerializeField] private StructureState _currentState = StructureState.Build;
        [SerializeField] private int _currentLevel;

        private List<ResourceDataModel> _currentRequiredResources;

        private StructureControllerUI _structureControllerUI;
        
        public StructureState CurrentState => _currentState;
        public int CurrentLevel => _currentLevel;
        public Action<List<ResourceDataModel>> OnBuildStart { get; set; }
        public Action<RequiredResourcesModel> OnUpgrade { get; set; }
        public Action<StructureState> OnStateChanged { get; set; }
        
        public override void Initialize(params object[] objects)
        {
            _structureControllerUI = objects[0] as StructureControllerUI;
            OnBuildStart?.Invoke(_requiredResourcesBuild);
        }

        public override void ActivateEntity()
        {
            throw new NotImplementedException();
        }

        public override void DiactivateEntity()
        {
            throw new NotImplementedException();
        }

        public override void Interact() => 
            _structureControllerUI.Interact();

        public override void FinishInteract() => 
            _structureControllerUI.FinishInteract();

        public bool TryBuildPlace()
        {
            _currentState = StructureState.Upgrade;
            _currentLevel = StartUpgradeLevel;
            OnStateChanged?.Invoke(_currentState);
            UpdateUpgradeUIPanel();
            
            return true;
        }

        public bool TryUpgradePlace()
        {
            _currentLevel++;

            if (_currentLevel > _requiredResourcesUpgrade.Count)
            {
                _currentState = StructureState.MaxUpgrade;
                OnStateChanged?.Invoke(_currentState);
                return true;
            }

            UpdateUpgradeUIPanel();

            return true;
        }

        private void UpdateUpgradeUIPanel()
        {
            if (_currentLevel - StartUpgradeLevel >= _requiredResourcesUpgrade.Count)
            {
                Debug.LogWarning("Level out of range");
                OnUpgrade?.Invoke(null);
                return;
            }
            
            var currentResources = _requiredResourcesUpgrade.FirstOrDefault(x => _currentLevel == x.Level);

            if (currentResources == null)
            {
                Debug.LogWarning("Current required resources by level isn't exist");
                return;
            }
            
            OnUpgrade?.Invoke(currentResources);
        }
        
        private bool IsEnoughResource(ResourceDataModel resource)
        {
            return _currentRequiredResources.Exists(x =>
                (x.ResourceConfig.TypeResource == (resource.ResourceConfig.TypeResource) &&
                 (x.AmountResource <= resource.AmountResource)));
        }
    }
}