using Core;
using InputModule;
using Managers;
using Player;
using PlayerInputModule;
using UnityEngine;

namespace PlayerModule
{
    [RequireComponent(typeof(InputHandler))]
    public class PlayerInitializer : EntityInitializer
    {
        [Header("Advanced components")] [SerializeField]
        private EntityStats _entityStats;

        [SerializeField] private EntityController _entityController;
        [SerializeField] private EntityMovementController _entityMovementController;
        [SerializeField] private EntityWeaponController _entityWeaponController;
        [SerializeField] private EntityInteractionController _entityInteractionController;
        [SerializeField] private EntityInventoryController _entityInventoryController;
        [SerializeField] private PlayerUIController _playerUIController;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private CameraController _cameraController;

        [Header("Default components")] [SerializeField]
        private Camera _cameraPrefab;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _graphics;

        private Camera _camera;

        private void OnValidate()
        {
            if (_entityStats == null)
            {
                _entityStats = GetComponent<EntityStats>();
            }

            if (_entityController == null)
            {
                _entityController = GetComponent<EntityController>();
            }

            if (_entityMovementController == null)
            {
                _entityMovementController = GetComponent<EntityMovementController>();
            }

            if (_entityWeaponController == null)
            {
                _entityWeaponController = GetComponent<EntityWeaponController>();
            }

            if (_entityInteractionController == null)
            {
                _entityInteractionController = GetComponent<EntityInteractionController>();
            }

            if (_entityInventoryController == null)
            {
                _entityInventoryController = GetComponent<EntityInventoryController>();
            }

            if (_playerUIController == null)
            {
                _playerUIController = GetComponent<PlayerUIController>();
            }

            if (_inputHandler == null)
            {
                _inputHandler = GetComponent<InputHandler>();
            }

            if (_cameraController == null)
            {
                _cameraController = GetComponent<CameraController>();
            }

            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }

            if (_collider == null)
            {
                _collider = GetComponent<Collider>();
            }
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

            var inputModule = new PlayerInput(_camera);
            var gameResourcesManager = GameResourcesManager.Instance;

            // ReSharper disable Unity.NoNullPropagation
            _entityStats?.Initialize();
            _inputHandler?.Initialize(inputModule);
            _inputHandler?.SetEnableLocal(true);
            _entityMovementController?.Initialize(inputModule,
                _rigidbody);
            _cameraController?.Initialize(_camera,
                _entityMovementController.transform);
            _entityWeaponController?.Initialize(inputModule,
                _entityStats);
            _entityInteractionController?.Initialize(inputModule);
            _entityController.Initialize(_entityStats,
                _entityMovementController,
                _entityWeaponController,
                _collider,
                _graphics);
            _entityInventoryController?.Initialize(gameResourcesManager);
            _playerUIController?.Initialize(gameResourcesManager);

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