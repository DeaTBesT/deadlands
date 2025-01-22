using DL.CoreRuntime;

namespace DL.PlayersRuntime
{
    public class PlayerInteractionController : EntityInteractionController
    {
//         [SerializeField] private float _interactDistance;
//         [SerializeField] private LayerMask _interactLayer;
//         [SerializeField] private Transform _interactPoint;
//
//         private Vector2 _mousePosition;
//
//         private Transform _currentInteractable;
//         
//         public override void Initialize(params object[] objects)
//         {
//             base.Initialize(objects);
//
//             if (_inputModule != null)
//             {
//                 _inputModule.OnMousePosition += OnMousePosition;
//             }
//         }
//
//         public override void Deinitialize(params object[] objects)
//         {
//             base.Deinitialize();
//
//             if (_inputModule != null)
//             {
//                 _inputModule.OnMousePosition -= OnMousePosition;
//             }
//
//             if (_currentInteractable != null)
//             {
//                 if (_currentInteractable.TryGetComponent(out IInteractable interactable))
//                 {
//                     interactable.ForceFinishInteract(this);
//                     ToggleInteractionEvents();
//                 }
//             }
//         }
//
//         private void OnMousePosition(Ray ray) =>
//             _mousePosition = ray.origin;
//
//         public override void OnInteract()
//         {
//             var hit = InteractRay();
//             
//             if (hit.transform == null)
//             {
//                 return;
//             }
//
//             if (!hit.transform.TryGetComponent(out IInteractable interactable))
//             {
//                 return;
//             }
//
//             if (!interactable.TryInteract(this, OnEndInteract))
//             {
//                 return;
//             }
//             
//             switch (interactable.TypeInteract)
//             {
//                 case InteractType.OneTime:
//                 {
//                     SetCurrentInteractable(null);
//                 }
//                     break;
//                 case InteractType.Toggle:
//                 {
//                     SetCurrentInteractable(interactable.Interactable);
//                     ToggleInteractionEvents();
//                 }
//                     break;
//                 case InteractType.Holding:
//                 {
//                     SetCurrentInteractable(interactable.Interactable);
//                 }
//                     break;
//                 default:
//                 {
// #if UNITY_EDITOR
//                     Debug.LogError("None interactable type");
// #endif
//                 }
//                     break;
//             }
//         }
//
//         public override void OnInteractUp()
//         {
//             if (_currentInteractable == null)
//             {
//                 return;
//             }
//
//             if (_currentInteractable.TryGetComponent(out IInteractable interactable))
//             {
//                 interactable.StopHolding();
//             }
//         }
//
//         private RaycastHit InteractRay()
//         {
//             var currentScene = gameObject.scene;
//             var interactDirection = _mousePosition - (Vector2)_interactPoint.position;
//
//             //return currentScene.GetPhysicsScene().Raycast(_interactPoint.position,
//             //    interactDirection,
//             //    _interactDistance,
//             //    _interactLayer);
//
//             currentScene.GetPhysicsScene().Raycast(transform.position, transform.forward, out RaycastHit hit);
//
//             return hit;
//         }
//
//         private void SetCurrentInteractable(Transform interactor) =>
//             _currentInteractable = interactor;
//
//         public override void OnEndInteract()
//         {
//             if (_currentInteractable == null)
//             {
// #if UNITY_EDITOR
//                 Debug.LogError("Current interactable is null");
// #endif
//                 return;
//             }
//
//             if (_currentInteractable.TryGetComponent(out IInteractable interactable))
//             {
//                 interactable.FinishInteract(this);
//                 ToggleInteractionEvents();
//                 _currentInteractable = null;
//             }
//         }
        public override void OnInteract()
        {
            throw new System.NotImplementedException();
        }

        public override void OnEndInteract()

        {
            throw new System.NotImplementedException();
        }
    }
}