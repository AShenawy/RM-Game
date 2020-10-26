using UnityEngine;
using UnityEngine.SceneManagement;

namespace Methodyca.Core
{
    /* This class handles moving the player between different scenes/levels in the game
     * and also protects some game objects from being destroyed when switching scenes
     */
    public sealed class SceneManagerScript
    {
        // make this class a singleton
        #region Singleton
        private static SceneManagerScript singleton;

        public static SceneManagerScript instance
        {
            get
            {
                if (singleton == null)
                    singleton = new SceneManagerScript();

                return singleton;
            }
        }
        #endregion

        public string startRoomTag = "Starting Room";

        private Scene sceneCurrent;
        private GameObject loadingScreen;
        private AsyncOperation preloadSceneOpr;
        

        public void GoToLevel(string sceneName, string roomTag = "Starting Room")   // Default start room tag in every scene
        {
            startRoomTag = roomTag;
            SceneManager.LoadScene(sceneName);
            // TODO: Load and unload scenes behind a loading screen
            // loadingScreen = Object.Instantiate(Resources.Load("Loading Screen") as GameObject);

            // Give the player the dimension switcher on Act 2 entry
            Scene loadedScene = SceneManager.GetActiveScene();
            if (loadedScene.name == "Act 2" || loadedScene.name == "Act 3")
                GiveSwitcherItem();
        }

        void GiveSwitcherItem()
        {
            InventoryManager.instance.GiveSwitcherItem();
        }

        public void PreloadScene(string sceneName, LoadSceneMode loadMode)
        {
            preloadSceneOpr = SceneManager.LoadSceneAsync(sceneName, loadMode);
            preloadSceneOpr.allowSceneActivation = false;
            Debug.LogWarning($"Scene \"{sceneName}\" loading in background.");
        }

        public void LoadPreloadedScene()
        {
            preloadSceneOpr.allowSceneActivation = true;
            SceneManager.sceneLoaded += SetLoadedSceneActive;
        }

        public void LoadSceneAdditive(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            SceneManager.sceneLoaded += SetLoadedSceneActive;
        }

        void SetLoadedSceneActive(Scene scene, LoadSceneMode mode)
        {
            SceneManager.SetActiveScene(scene);
            SceneManager.sceneLoaded -= SetLoadedSceneActive;
        }

        public void UnloadScene()
        {
            AsyncOperation unloadOpr = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            //unloadOpr.completed += UnloadAssets;
        }

        void UnloadAssets(AsyncOperation opr)
        {
            Debug.Log("Scene closed. Unloading unused resources.");
            // Unload minigame unused assests
            Resources.UnloadUnusedAssets();
            
            opr.completed -= UnloadAssets;
            GameObject.FindObjectOfType<BGM>().BackToMain();
        }

        // Typically called by GameManager script when player is going to a scene within main game
        public GameObject GetSceneStartingRoom()
        {
            GameObject sceneStartRoom = GameObject.FindGameObjectWithTag(startRoomTag);
            return sceneStartRoom;
        }
    }
}