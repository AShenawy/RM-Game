using UnityEngine;


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
            "of it. Unique closes the main game scene and loads the minigame only.")]
        private MinigameAccessType sceneLoadType = MinigameAccessType.Additive;

        public delegate void GamePlayAccess(bool value);
        public event GamePlayAccess isGamePlayable;


        public void LoadMinigame()
        {
            switch (sceneLoadType)
            {
                case (MinigameAccessType.Additive):
                    SceneManagerScript.instance.LoadSceneAdditive(minigameSceneName);
                    break;

                case (MinigameAccessType.Unique):
                    SceneManagerScript.instance.GoToLevel(minigameSceneName);
                    break;

                default:
                    Debug.LogError("Could not find suitable scene loading type. Check it's set to either Additive or Unique.");
                    break;
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

    public enum MinigameAccessType { Additive, Unique }

    /* Additive access loads the minigame on top of the main game scene.
     * The main game is still running in background.
     * 
     * Unique access closes the main game and loads the minigame scene.
     * Progress in main game needs to be saved before doing this jump, otherwise main game state will reset.
     */
}
