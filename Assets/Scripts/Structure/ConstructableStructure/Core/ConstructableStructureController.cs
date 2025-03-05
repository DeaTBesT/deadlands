using System;
using System.Collections.Generic;
using System.Linq;
using DL.CoreRuntime;
using DL.Data.Resource;
using DL.EnumsRuntime;
using DL.StructureRuntime.Core;
using DL.StructureRuntime.Model;
using UnityEngine;

namespace DL.ConstructableStructureRuntime.Core
{
    public abstract class ConstructableStructureController : StructureController
    {
        private const int BuildLevel = 0;
        private const int StartUpgradeLevel = 1;

        [SerializeField] private List<ResourceDataModel> _requiredResourcesBuild;
        [SerializeField] private List<RequiredResourcesModel> _requiredResourcesUpgrade;

        [Header("Optional")] [SerializeField] private StructureState _currentState = StructureState.Build;
        [SerializeField] private int _currentLevel = 0;

        private List<ResourceDataModel> _currentRequiredResources;

        private StructureControllerUI _structureControllerUI;

        private IInventoryController _currentInventory;

        public StructureState CurrentState => _currentState;
        public int CurrentLevel => _currentLevel;
        public Action<List<ResourceDataModel>> OnBuildStart { get; set; }
        public Action<RequiredResourcesModel> OnUpgrade { get; set; }
        public Action<StructureState> OnStateChanged { get; set; }

        public override void Initialize(params object[] objects)
        {
            _structureControllerUI = objects[0] as StructureControllerUI;

            OnBuildStart?.Invoke(_requiredResourcesBuild);

            _currentRequiredResources =
                new List<ResourceDataModel>(GetRequiredResources().RequiredResources);
        }

        public override void ActivateEntity()
        {
            throw new NotImplementedException();
        }

        public override void DiactivateEntity()
        {
            throw new NotImplementedException();
        }

        public override bool TryInteract(Transform interactor)
        {
            if (!interactor.TryGetComponent(out IInventoryController inventoryController))
            {
                return false;
            }

            _currentInventory = inventoryController;
            _structureControllerUI.TryInteract(interactor);

            return true;
        }

        public override void FinishInteract()
        {
            _structureControllerUI.FinishInteract();
            _currentInventory = null;
        }

        public bool TryBuildPlace()
        {
            if (_currentInventory == null)
            {
                return false;
            }

            if (!IsEnoughResources(_currentInventory.ResourcesData))
            {
                return false;
            }

            _currentState = StructureState.Upgrade;
            _currentLevel = StartUpgradeLevel;

            _currentInventory.RemoveResources(_currentRequiredResources);
            _currentRequiredResources =
                new List<ResourceDataModel>(GetRequiredResources().RequiredResources);

            OnStateChanged?.Invoke(_currentState);
            UpdateUpgradeUIPanel();

            return true;
        }

        public bool TryUpgradePlace()
        {
            if (_currentInventory == null)
            {
                return false;
            }

            if (!IsEnoughResources(_currentInventory.ResourcesData))
            {
                return false;
            }

            _currentLevel++;

            _currentInventory.RemoveResources(_currentRequiredResources);

            if (_currentLevel > _requiredResourcesUpgrade.Count)
            {
                _currentState = StructureState.MaxUpgrade;
                OnStateChanged?.Invoke(_currentState);
                return true;
            }

            _currentRequiredResources =
                new List<ResourceDataModel>(GetRequiredResources().RequiredResources);
            
            UpdateUpgradeUIPanel();

            return true;
        }

        private void UpdateUpgradeUIPanel()
        {
            if (_currentLevel - StartUpgradeLevel >= _requiredResourcesUpgrade.Count)
            {
                OnUpgrade?.Invoke(null);
                return;
            }

            var currentResources = GetRequiredResources();

            if (currentResources == null)
            {
                return;
            }

            OnUpgrade?.Invoke(currentResources);
        }

        private bool IsEnoughResources(List<ResourceDataModel> resources) =>
            resources.Any() && resources.All(IsEnoughResource);

        private bool IsEnoughResource(ResourceDataModel resource)
        {
            return _currentRequiredResources.Any(x =>
                (x.ResourceConfig.TypeRare == resource.ResourceConfig.TypeRare) &&
                (x.AmountResource <= resource.AmountResource));
        }

        /// <summary>
        /// Получает требуемые ресурсы от уровня _currentLevel
        /// </summary>
        /// <returns></returns>
        private RequiredResourcesModel GetRequiredResources()
        {
            return _currentLevel <= BuildLevel
                ? new RequiredResourcesModel(_requiredResourcesBuild, BuildLevel)
                : _requiredResourcesUpgrade.FirstOrDefault(x => _currentLevel == x.Level);
        }

        /// <summary>
        /// Получает требуемые ресурсы от введенного уровня
        /// </summary>
        /// <returns></returns>
        private RequiredResourcesModel GetRequiredResourcesByLevel(int level)
        {
            return level <= BuildLevel
                ? new RequiredResourcesModel(_requiredResourcesBuild, BuildLevel)
                : _requiredResourcesUpgrade.FirstOrDefault(x => level == x.Level);
        }
    }
}