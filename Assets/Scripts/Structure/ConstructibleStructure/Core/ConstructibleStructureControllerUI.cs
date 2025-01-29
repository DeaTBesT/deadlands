using System;
using System.Collections.Generic;
using DL.Data.Resource;
using DL.EnumsRuntime;
using DL.StructureRuntime.Core;
using DL.StructureRuntime.Model;
using DL.StructureRuntime.UIPanels.Core;
using UI.Core;
using UnityEngine;

namespace DL.ConstructibleStructureRuntime.Core
{
    public abstract class ConstructibleStructureControllerUI : StructureControllerUI
    {
        [SerializeField] private AdvancedStructurePanelUI _buildPanel;
        [SerializeField] private AdvancedStructurePanelUI _upgradePanel;
        [SerializeField] private SimpleStructurePanelUI _maxUpgradePanel;

        protected List<IPanelUI> _structurePanels = new();

        private ConstructibleStructureController _constructibleStructureController;

        public override void Initialize(params object[] objects)
        {
            _constructibleStructureController = objects[0] as ConstructibleStructureController;

            _constructibleStructureController.OnBuildStart += OnBuildStart;
            _constructibleStructureController.OnUpgrade += OnUpgrade;
            _constructibleStructureController.OnStateChanged += OnStateChanged;

            _buildPanel.AddOnClickEvent(OnBuildButtonClick);
            _upgradePanel.AddOnClickEvent(OnUpgradeButtonClick);

            //Сюда добавляем все новые панели
            _structurePanels = new List<IPanelUI>
            {
                _generalPanel,
                _buildPanel,
                _upgradePanel,
                _maxUpgradePanel
            };

            ClosePanels();
        }

        public override void Interact() => 
            OpenPanel();

        public override void FinishInteract() =>
            ClosePanels();

        public void OpenPanel()
        {
            switch (_constructibleStructureController.CurrentState)
            {
                case StructureState.Build:
                {
                    OpenBuildPanel();
                }
                    break;
                case StructureState.Upgrade:
                {
                    OpenUpgradePanel();
                }
                    break;
                case StructureState.MaxUpgrade:
                {
                    OpenMaxUpgradePanel();
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            OpenGeneralPanel();
        }

        private void OnStateChanged(StructureState currentState)
        {
            switch (currentState)
            {
                case StructureState.Build:
                {
                    OpenBuildPanel();
                }
                    break;
                case StructureState.Upgrade:
                {
                    OpenUpgradePanel();
                }
                    break;
                case StructureState.MaxUpgrade:
                {
                    OpenMaxUpgradePanel();
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            OpenGeneralPanel();
        }

        public void ClosePanels() =>
            _structurePanels.ForEach(panel => panel.Hide());

        //Здесь какие-нибудь анимации
        private void OnBuildButtonClick()
        {
            _constructibleStructureController.TryBuildPlace();

            _buildPanel.Hide();
            _upgradePanel.Show();
        }

        //Здесь какие-нибудь анимации
        private void OnUpgradeButtonClick()
        {
            _constructibleStructureController.TryUpgradePlace();
        }

        private void OpenBuildPanel()
        {
            _structurePanels.ForEach(panel => panel.Hide());
            _buildPanel.Show();
        }

        private void OpenUpgradePanel()
        {
            _structurePanels.ForEach(panel => panel.Hide());
            _upgradePanel.Show();
        }

        private void OpenMaxUpgradePanel()
        {
            _structurePanels.ForEach(panel => panel.Hide());
            _maxUpgradePanel.Show();
        }

        private void OpenGeneralPanel() =>
            _generalPanel.Show();

        private void OnBuildStart(List<ResourceDataModel> data) =>
            _buildPanel.UpdatePanelView(data);

        private void OnUpgrade(RequiredResourcesModel data) =>
            _upgradePanel.UpdatePanelView(data);
    }
}