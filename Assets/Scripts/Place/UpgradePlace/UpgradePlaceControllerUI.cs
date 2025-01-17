using System;
using System.Collections.Generic;
using Enums;
using Place.Core;
using UI.PlacePanels;
using UI.PlacePanels.Core;
using UnityEngine;

namespace Place.UpgradePlace
{
    public class UpgradePlaceControllerUI : PlaceControllerUI
    {
        [SerializeField] private BuildPanelUI _buildPanel;
        [SerializeField] private UpgradePanelUI _upgradePanel;
        [SerializeField] private MaxUpgradePanelUI _maxUpgradePanel;

        private List<SimplePlacePanelUI> _placePanels = new();
        
        private UpgradePlaceController _upgradePlaceController;

        public override void Initialize(params object[] objects)
        {
            _upgradePlaceController = objects[0] as UpgradePlaceController;

            _upgradePlaceController.OnStateChanged += OnStateChanged;
            
            _buildPanel.Initialize(_upgradePlaceController);
            _upgradePanel.Initialize(_upgradePlaceController);
            
            _buildPanel.AddOnClickEvent(OnBuildButtonClick);
            _upgradePanel.AddOnClickEvent(OnUpgradeButtonClick);

            //Сюда добавляем все новые панели
            _placePanels = new List<SimplePlacePanelUI>
            {
                _buildPanel,
                _upgradePanel,
                _maxUpgradePanel
            };

            ClosePanels();
        }

        public void OpenPanel()
        {
            switch (_upgradePlaceController.CurrentState)
            {
                case PlaceState.Build:
                {
                    OpenBuildPanel();
                }
                    break;
                case PlaceState.Upgrade:
                {
                    OpenUpgradePanel();
                }
                    break;
                case PlaceState.MaxUpgrade:
                {
                    OpenMaxUpgradePanel();
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnStateChanged(PlaceState currentState)
        {
            switch (_upgradePlaceController.CurrentState)
            {
                case PlaceState.Build:
                {
                    OpenBuildPanel();
                }
                    break;
                case PlaceState.Upgrade:
                {
                    OpenUpgradePanel();
                }
                    break;
                case PlaceState.MaxUpgrade:
                {
                    OpenMaxUpgradePanel();
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ClosePanels() => 
            _placePanels.ForEach(panel => panel.Hide());

        //Здесь какие-нибудь анимации
        private void OnBuildButtonClick()
        {
            _upgradePlaceController.TryBuildPlace();
            
            _buildPanel.Hide();
            _upgradePanel.Show();
        }

        //Здесь какие-нибудь анимации
        private void OnUpgradeButtonClick()
        {
            _upgradePlaceController.TryUpgradePlace();
        }
        
        private void OpenBuildPanel()
        {
            _placePanels.ForEach(panel => panel.Hide());
            _buildPanel.Show();
        }

        private void OpenUpgradePanel()
        {
            _placePanels.ForEach(panel => panel.Hide());
            _upgradePanel.Show();
        }

        private void OpenMaxUpgradePanel()
        {
            _placePanels.ForEach(panel => panel.Hide());
            _maxUpgradePanel.Show();
        }
    }
}