using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Methodyca.Core
{
    // This class handles moving the player between different scenes/levels in the game
    public sealed class SceneManagerScript : MonoBehaviour
    {
        // make this class a singleton
        public static SceneManagerScript instance;

        [HideInInspector]
        public string startRoomTag = "Starting Room";
        public GameObject loadingScreen;

        private Scene sceneCurrent;
        private GameObject loadingScreenPrefab;
        private AsyncOperation preloadSceneOpr;


        private void Awake()
        {
            if (instance == null)
                instance = this;

            DontDestroyOnLoad(this);
        }

        public void GoToLevel(string sceneName, string roomTag = "Starting Room")   // Default start room tag in every scene
        {
            startRoomTag = roomTag;
            StartCoroutine(LoadLevel(sceneName));
        }

        IEnumerator LoadLevel(string sceneName)
        {
            // instantiate a loading screen and hide the start button in it using the attached ProgressBar script
            loadingScreen = Instantiate(loadingScreen);
            ProgressBar progress = loadingScreen.GetComponent<ProgressBar>();
            progress.startButton.SetActive(false);

            // Start loading the new scene and stop it from auto activation
            AsyncOperation loadOpr = SceneManager.LoadSceneAsync(sceneName);
            loadOpr.allowSceneActivation = false;

            while (!loadOpr.isDone)
            {
                // fill up the loading screen progress bar
                progress.progressBar.fillAmount = loadOpr.progress;

                if (loadOpr.progress >= 0.9f)
                {
                    // when loading is finished activate the start button
                    progress.startButton.SetActive(true);

                    if (progress.isStartClicked || Input.GetKeyDown(KeyCode.Space))
                        loadOpr.allowSceneActivation = true;
                }

                yield return null;
            }

            CheckSwitcherApplicable();
            UnloadAssets();
        }

        void CheckSwitcherApplicable()
        {
            // Give the player the dimension switcher on Act 2 entry
            Scene loadedScene = SceneManager.GetActiveScene();
            if (loadedScene.name == "Act 2" || loadedScene.name == "Act 3")
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

        // used to unload additive scenes
        public void UnloadScene()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }

        void UnloadAssets()
        {
            Debug.Log("Scene closed. Unloading unused resources.");
            // Unload minigame unused assests
            Resources.UnloadUnusedAssets();
        }

        // Typically called by GameManager script when player is going to a scene within main game
        public GameObject GetSceneStartingRoom()
        {
            GameObject sceneStartRoom = GameObject.FindGameObjectWithTag(startRoomTag);
            return sceneStartRoom;
        }
    }
}