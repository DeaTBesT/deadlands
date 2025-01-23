using DL.InterfacesRuntime;
using UnityEngine;

namespace DL.CoreRuntime
{
    public class CameraController : MonoBehaviour, IInitialize
    {
        [SerializeField] private float _cameraSmooth = 5f;
        [SerializeField] private Vector3 _offset;

        private Camera _camera;

        private Transform _target;

        public bool IsEnable { get; set; }

        public void Initialize(params object[] objects)
        {
            _camera = objects[0] as Camera;
            _target = objects[1] as Transform;
        }

        private void FixedUpdate()
        {
            if (_camera == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Camera is null");
#endif
                return;
            }

            _camera.transform.position = Vector3.Lerp(_camera.transform.position,
                _target.position + _offset, _cameraSmooth * Time.fixedDeltaTime);
        }

        public void ChangeTarget(Transform target) => 
            _target = target.transform;
    }
}