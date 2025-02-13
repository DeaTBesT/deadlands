using System;
using System.Collections.Generic;
using DL.Data.Scene;
using DL.InterfacesRuntime;
using DL.SceneTransitionRuntime;
using DL.SelectingLevel.Models;
using DL.UtilsRuntime;
using UnityEngine;

namespace DL.SelectingLevel
{
    public class SelectLevelManager : Singleton<SelectLevelManager>, IInitialize, IDeinitialize
    {
        [SerializeField] private List<LevelInfoModel> _levelsInfo;

        private readonly List<IButtonClick> _clickHandlers = new();

        private SelectLevelControllerUI _selectLevelControllerUI;
        
        public Action OnShowPanel { get; set; }
        public Action<List<LevelInfoModel>> OnLevelsInfoInitialize { get; set; }

        public bool IsEnable { get; set; }

        private void ResetStatics()
        {
            // reset all statics

            foreach (var handler in _clickHandlers)
            {
                handler.OnButtonClick -= OnButtonClickHandler;
            }
        }
        
        public void Initialize(params object[] objects)
        {
            _selectLevelControllerUI = objects[0] as SelectLevelControllerUI;
            _selectLevelControllerUI.Initialize(this);
            
            SceneLoader.OnStartLoadScene += OnStartLoadScene;

            OnLevelsInfoInitialize?.Invoke(_levelsInfo);
        }

        public void Deinitialize(params object[] objects)
        {
            _selectLevelControllerUI.Deinitialize();
            SceneLoader.OnStartLoadScene -= OnStartLoadScene;
        }

        private void OnStartLoadScene(SceneConfig sceneConfig) =>
            ResetStatics();

        /// <summary>
        /// Для регистрации нажания на кнопку, для открытия панели
        /// </summary>
        public void RegisterButtonClick(IButtonClick clickHandler)
        {
            if ((clickHandler == null) || (_clickHandlers.Contains(clickHandler)))
            {
                return;
            }
            
            clickHandler.OnButtonClick += OnButtonClickHandler;
            _clickHandlers.Add(clickHandler);
        }

        /// <summary>
        /// Для удаления регистрации нажания на кнопку, для открытия панели
        /// </summary>
        public void UnRegisterButtonClick(IButtonClick clickHandler)
        {
            if ((clickHandler == null) || (!_clickHandlers.Contains(clickHandler)))
            {
                return;
            }
            
            clickHandler.OnButtonClick -= OnButtonClickHandler;
            _clickHandlers.Remove(clickHandler);
        }

        private void OnButtonClickHandler() =>
            OnShowPanel?.Invoke();
    }
}