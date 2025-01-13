using Core;
using UnityEngine;

namespace Player
{
    public class PlayerController : EntityController
    {
        private EntityStats _entityStats;
        private EntityMovementController _entityMovementController;
        private EntityWeaponController _entityWeaponController;
        private Collider2D _collider;
        private Transform _graphics;

        public override void Initialize(params object[] objects)
        {
            _entityStats = objects[0] as EntityStats;
            _entityMovementController = objects[1] as EntityMovementController;
            _entityWeaponController = objects[2] as EntityWeaponController;
            _collider = objects[3] as Collider2D;
            _graphics = objects[4] as Transform;
        }

        public override void ActivateEntity()
        {
            _entityStats.IsEnable = true;
            _entityMovementController.IsEnable = true;
            _entityWeaponController.IsEnable = true;
            _collider.enabled = true;
            _graphics.gameObject.SetActive(true);
        }

        public override void DiactivateEntity()
        {
            _entityStats.IsEnable = false;
            _entityMovementController.IsEnable = false;
            _entityWeaponController.IsEnable = false;
            _collider.enabled = false;
            _graphics.gameObject.SetActive(false);
        }

        public override void ActivateMoveEntity()
        {
            _entityMovementController.IsEnable = true;
            _entityWeaponController.IsEnable = true;
        }
        
        public override void DiactivateMoveEntity()
        {
            _entityMovementController.IsEnable = false;
            _entityWeaponController.IsEnable = false;
        }
    }
}