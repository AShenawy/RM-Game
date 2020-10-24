using UnityEngine;
using UnityEngine.SceneManagement;


namespace Methodyca.Core
{
    // This script handles initiation of a minigame when interacted with
    [RequireComponent(typeof(MinigameInteraction))]
    public class MinigameHub : MonoBehaviour
    {
        [Header("Scene Loading Parameters")]
        [SerializeField, Tooltip("The scene name of minigame to be loaded")]
        private string minigameSceneName;
        [SerializeField, Tooltip("How the minigame will be loaded. Additive keeps the main game running and loads the minigame on top " +
            "of it. Single closes the main game scene and loads the minigame only.")]
        private LoadSceneMode sceneLoadType = LoadSceneMode.Additive;
        [SerializeField, Tooltip("Should the minigame be preloaded when entering the room?")]
        private bool preloadMinigame = false;

        public delegate void GamePlayAccess(bool value);
        public event GamePlayAccess isGamePlayable;


        public virtual void Start()
        {
            if (preloadMinigame)
                SceneManagerScript.instance.PreloadScene(minigameSceneName, sceneLoadType);
        }

        public void LoadMinigame()
        {

            if (preloadMinigame == false)
            {
                switch (sceneLoadType)
                {
                    case LoadSceneMode.Additive:
                        SceneManagerScript.instance.LoadSceneAdditive(minigameSceneName);
                        break;

                    case LoadSceneMode.Single:
                        SceneManagerScript.instance.GoToLevel(minigameSceneName);
                        break;

                    default:
                        Debug.LogError("Could not find suitable scene loading type. Check it's set to either Additive or Unique.");
                        break;
                }
            }
            else
            {
                SceneManagerScript.instance.LoadPreloadedScene();
                preloadMinigame = false;
            }
        }

        // This method will be overrideen by derived classes
        public virtual void OnItemPlacement(Item item) { }

        // This method will be overridden by derived classes
        public virtual void EndGame()
        {
            isGamePlayable?.Invoke(false);
        }
    }
}
