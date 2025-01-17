using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using GameResources.Core;
using Place.Core;
using Place.Models;
using UnityEngine;

namespace Place.UpgradePlace
{
    public class UpgradePlaceController : PlaceController
    {
        private const int StartUpgradeLevel = 1;
        
        [SerializeField] private List<ResourceData> _requiredResourcesBuild;
        [SerializeField] private List<ResourcesUpgradeModel> _requiredResourcesUpgrade;

        [Header("Optional")] 
        [SerializeField] private PlaceState _currentState = PlaceState.Build;
        [SerializeField] private int _currentLevel;

        private List<ResourceData> _currentRequiredResources;

        public PlaceState CurrentState => _currentState;
        public int CurrentLevel => _currentLevel;
        public Action<List<ResourceData>> OnBuildStart { get; set; }
        public Action<ResourcesUpgradeModel> OnUpgrade { get; set; }
        public Action<PlaceState> OnStateChanged { get; set; }
        
        public override void Initialize(params object[] objects) => 
            OnBuildStart?.Invoke(_requiredResourcesBuild);

        public override void ActivateEntity()
        {
            throw new NotImplementedException();
        }

        public override void DiactivateEntity()
        {
            throw new NotImplementedException();
        }

        public bool TryBuildPlace()
        {
            _currentState = PlaceState.Upgrade;
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
                _currentState = PlaceState.MaxUpgrade;
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
        
        private bool IsEnoughResource(ResourceData resource)
        {
            return _currentRequiredResources.Exists(x =>
                (x.ResourceConfig.TypeResource == (resource.ResourceConfig.TypeResource) &&
                 (x.AmountResource <= resource.AmountResource)));
        }
    }
}