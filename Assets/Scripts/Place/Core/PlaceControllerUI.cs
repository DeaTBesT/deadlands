using System;
using System.Collections.Generic;
using Enums;
using Interfaces;
using UI.PlacePanels;
using UI.PlacePanels.Core;
using UnityEngine;

namespace Place.Core
{
    public class PlaceControllerUI : MonoBehaviour, IInitialize
    {
        [SerializeField] private BuildPanelUI _buildPanel;
        [SerializeField] private UpgradePanelUI _upgradePanel;
        [SerializeField] private MaxUpgradePanelUI _maxUpgradePanel;

        private List<SimplePlacePanelUI> _placePanels = new();
        
        private PlaceController _placeController;

        public bool IsEnable { get; set; }

        public void Initialize(params object[] objects)
        {
            _placeController = objects[0] as PlaceController;

            _placeController.OnStateChanged += OnStateChanged;
            
            _buildPanel.Initialize(_placeController);
            _upgradePanel.Initialize(_placeController);
            
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
            switch (_placeController.CurrentState)
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
            switch (_placeController.CurrentState)
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
            _placeController.TryBuildPlace();
            
            _buildPanel.Hide();
            _upgradePanel.Show();
        }

        //Здесь какие-нибудь анимации
        private void OnUpgradeButtonClick()
        {
            _placeController.TryUpgradePlace();
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