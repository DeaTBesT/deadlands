using DL.EnumsRuntime;
using NaughtyAttributes;
using UnityEngine;

namespace DL.Data.Scene
{
    [CreateAssetMenu(menuName = "Scene Configuration", fileName = "Scene Configuration")]
    public class SceneConfig : ScriptableObject
    {
        [SerializeField, Scene] private string _sceneName;
        [SerializeField] private SceneType _sceneType;
        
        public string SceneName => _sceneName;
        public SceneType TypeScene => _sceneType;
    }
}