using UnityEngine;

namespace Methodyca.Core
{
    // This script allows preloading scenes on room entry to have a scene ready beforehand - like a minigame
    public class PreloadSceneOnEntry : MonoBehaviour
    {
        public string preloadSceneName;
        public UnityEngine.SceneManagement.LoadSceneMode sceneLoadingMode;

        private void Start()
        {
            SceneManagerScript.instance.PreloadScene(preloadSceneName, sceneLoadingMode);
            Debug.Log($"Scene \"{preloadSceneName}\" will be preloaded.");
        }
    }
}