using DL.CoreRuntime;
using DL.InputModuleRuntime;
using DL.InputModuleRuntime.Modules;
using DL.InventorySystem.Core;
using DL.PrefabsPoolingRuntime;
using DL.WeaponSystem.Core;
using JoystickRuntime;
using UnityEngine;

namespace DL.PlayersRuntime
{
    [RequireComponent(typeof(InputHandler))]
    public class PlayerInitializer : EntityInitializer
    {
        [Header("Advanced components")] [SerializeField]
        private EntityStats _entityStats;

        [SerializeField] private EntityController _entityController;
        [SerializeField] private EntityMovementController _entityMovementController;
        [SerializeField] private WeaponController _entityWeaponController;
        [SerializeField] private EntityInteractionController _entityInteractionController;
        [SerializeField] private InventoryController _entityInventoryController;
        [SerializeField] private PlayerUIController _playerUIController;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private CameraController _cameraController;

        [Header("Default components")] [SerializeField]
        private Camera _cameraPrefab;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _graphics;

        [SerializeField] private Joystick _joystick;

        private Camera _camera;

        private void OnValidate()
        {
            _entityStats ??= GetComponent<EntityStats>();
            _entityController ??= GetComponent<EntityController>();
            _entityMovementController ??= GetComponent<EntityMovementController>();
            _entityWeaponController ??= GetComponent<WeaponController>();
            _entityInteractionController ??= GetComponent<EntityInteractionController>();
            _entityInventoryController ??= GetComponent<InventoryController>();
            _playerUIController ??= GetComponent<PlayerUIController>();
            _inputHandler ??= GetComponent<InputHandler>();
            _cameraController ??= GetComponent<CameraController>();
            _rigidbody ??= GetComponent<Rigidbody>();
            _collider ??= GetComponent<Collider>();
        }

        public override void Initialize(params object[] objects)
        {
            if (IsInitialized)
            {
                return;
            }

            if (_camera == null)
            {
                _camera = Camera.main;

                if (_camera == null)
                {
                    var cameraObject = Instantiate(_cameraPrefab, transform);
                    cameraObject.transform.parent = null;
                    cameraObject.TryGetComponent(out _camera);
                }
            }

            var prefabPoolManager = objects[0] as PrefabPoolManager;
            var inputModule = new PlayerInputMobile(_joystick);

            // ReSharper disable Unity.NoNullPropagation
            _entityStats?.Initialize(_entityInventoryController);
            _inputHandler?.Initialize(inputModule);
            _inputHandler?.SetEnableLocal(true);
            _cameraController?.Initialize(_camera,
                _entityMovementController.transform);
            _entityWeaponController?.Initialize(_entityStats, prefabPoolManager);
            _entityMovementController?.Initialize(inputModule,
                _rigidbody,
                _entityWeaponController);
            _entityInteractionController?.Initialize(inputModule);
            _entityController.Initialize(_entityStats,
                _entityMovementController,
                _entityWeaponController,
                _collider,
                _graphics);
            _entityInventoryController?.Initialize();
            _playerUIController?.Initialize(_entityInventoryController);

            IsInitialized = true;
        }


        public override void Deinitialize(params object[] objects)
        {
            _entityMovementController?.Deinitialize();
            _entityWeaponController?.Deinitialize();
            _entityInteractionController?.Deinitialize();
            _playerUIController?.Deinitialize();
            _entityInventoryController?.Deinitialize();

            IsInitialized = false;
        }
    }
}