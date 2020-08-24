using UnityEngine;


namespace Methodyca.Core
{
    // This script handles initiation of a minigame when interacted with
    public class SortingGameHub : MonoBehaviour
    {

        [SerializeField, Tooltip("The scene name of minigame to be loaded")]
        private string minigameSceneName;
        [SerializeField, Tooltip("How the minigame will be loaded. Additive keeps the main game running and loads the minigame on top " +
            "of it. Unique closes the main game scene and loads the minigame only.")]
        private MinigameAccessType sceneLoadType = MinigameAccessType.Additive;

        [Space]
        public GameObject unchargedCrystalQLPrefab;
        public GameObject unchargedCrystalQNPrefab;

        [Space]
        public GameObject chargedCrystalQLPrefab;
        public GameObject chargedCrystalQNPrefab;

        [Space]
        [SerializeField, Tooltip("Game Object holding qualitative crystal on desk")]
        private GameObject qualitativeCrystalDisplay;
        [SerializeField, Tooltip("Game Object holding quantitative crystal on desk")]
        private GameObject quantitativeCrystalDisplay;

        public delegate void GamePlayAccess(bool value);
        public event GamePlayAccess isGamePlayable;

        private SwitchImageDisplay deskSpriteSwitch;

        private void Start()
        {
            deskSpriteSwitch = GetComponent<SwitchImageDisplay>();
        }

        public void EndGame()
        {
            isGamePlayable?.Invoke(false);
            deskSpriteSwitch.SwitchImage();
            ReplaceCrystals();
        }

        void ReplaceCrystals()
        {
            // destroy existing uncharged crystals
            Destroy(qualitativeCrystalDisplay.GetComponentInChildren<Transform>().gameObject);
            Destroy(quantitativeCrystalDisplay.GetComponentInChildren<Transform>().gameObject);
            
            // spawn charged crystals
            Instantiate(chargedCrystalQLPrefab, qualitativeCrystalDisplay.transform);
            Instantiate(chargedCrystalQNPrefab, quantitativeCrystalDisplay.transform);
        }

        public void DisplayCrystalOnCharger(string itemName)
        {
            switch (itemName)
            {
                case ("Dark Blue Crystal"):
                    Instantiate(unchargedCrystalQNPrefab, quantitativeCrystalDisplay.transform);
                    break;

                case ("Dark Purple Crystal"):
                    Instantiate(unchargedCrystalQLPrefab, qualitativeCrystalDisplay.transform);
                    break;

                default:
                    Debug.LogError("Can't display crystal image on charger. Check item names");
                    break;
            }
        }

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

        /* ********************************
         * NOTES:
         * Instead of switching static images of crystals, it's better to instantiate each crytal on a charger
         * with the Observe script so it has a description/interaction. Then, when the minigame is won, switch 
         * the discharged crytals with charged ones with a Pickup script on them so the player can interactively
         * pick them instead of automatically being added to the inventory.
         * After minigame is won, desk should be inactive and there's on big collision box covering both charged
         * crystals. This way, clicking on either crystal will automatically add both crytals to inventory.
         */

        //public void CompleteGame()
        //{
        //    isGameWon = true;
        //    crystalQLDisplay.SwitchImage();
        //    crystalQNDisplay.SwitchImage();
        //}
    }

    public enum MinigameAccessType { Additive, Unique }

    /* Additive access loads the minigame on top of the main game scene.
     * The main game is still running in background.
     * 
     * Unique access closes the main game and loads the minigame scene.
     * Progress in main game needs to be saved before doing this jump, otherwise main game state will reset.
     */
}
