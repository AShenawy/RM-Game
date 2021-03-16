#define TESTING
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace Methodyca.Core
{
    // This class handles moving the player between different scenes/levels in the game
    public sealed class SceneManagerScript : MonoBehaviour
    {
        // make this class a singleton
        public static SceneManagerScript instance;
        
        public GameObject loadingScreenPrefab;

        [HideInInspector]
        public string startRoomName;
        //[HideInInspector]
        public List<int> minigamesWon = new List<int>();

        private AsyncOperation preloadSceneOpr;
        private bool isPreloadingScene;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else if (instance != this)
                Destroy(gameObject);
        }

#if TESTING
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
                SaveLoadManager.SetCurrentScene(SceneManager.GetActiveScene().name);
        }
#endif

        //public void GoToLevel(string sceneName, string roomName, bool keepMinigames = false , bool keepInteractions = false)
        public void GoToLevel(string sceneName, string roomName, bool keepInteractions = true)
        {
            startRoomName = roomName;
            StartCoroutine(LoadLevel(sceneName));

            // at some cases, like moving between main game scenes, interaction states from previous scene can be removed
            if (!keepInteractions)
            {
                SaveLoadManager.ClearInteractableState();
            }


            //TODO remove if redundant
            // when moving between scenes (Acts or main/mini game scenes) or loading a new game, minigame progression needs to be saved
            // Minigames should be cleared when loading a save file from inside the game (not main menu)
            //if(!keepMinigames)   
            //    SaveLoadManager.ClearMinigamesComplete();

            // if moving between levels during gameplay, interactions in previous scene can be deleted
            // when loading a game from file, interaction should be kept for interactables to check
            //if (!keepInteractions)
            //{
            //    SaveLoadManager.ClearInteractableState();
            //    SaveLoadManager.ClearInventoryItems();
            //}
        }

        IEnumerator LoadLevel(string sceneName)
        {
            // mute game sounds
            AudioListener.pause = true;

            // instantiate a loading screen and hide the start button in it using the attached ProgressBar script
            GameObject loadingScreen = Instantiate(loadingScreenPrefab);
            DontDestroyOnLoad(loadingScreen);   // so it carries over to the loaded scene and can delay it's removal
            ProgressBar progress = loadingScreen.GetComponent<ProgressBar>();
            progress.startButton.SetActive(false);

            // if a scene is being preloaded then it must be loaded before another scene can load
            if (isPreloadingScene)
                preloadSceneOpr.allowSceneActivation = true;

            // Start loading the new scene and stop it from auto activation
            AsyncOperation loadOpr = SceneManager.LoadSceneAsync(sceneName);
            loadOpr.priority = 0;
            loadOpr.allowSceneActivation = false;

            while (!loadOpr.isDone)
            {
                // fill up the loading screen progress bar
                progress.progressBar.fillAmount = loadOpr.progress;

                if (loadOpr.progress >= 0.9f)
                {
                    // when loading is finished activate the start button
                    //progress.startButton.SetActive(true);

                    loadOpr.allowSceneActivation = true;
                    //AudioListener.pause = false;

                    //if (progress.isStartClicked || Input.GetKeyDown(KeyCode.Space))
                    //{
                    //    loadOpr.allowSceneActivation = true;
                    //    AudioListener.pause = false;
                    //}
                }

                yield return null;
            }

            // update Save Manager with the current loaded scene
            SaveLoadManager.SetCurrentScene(sceneName);
            CheckSwitcherApplicable();
            UnloadAssets();
            // Delay loadscreen removal to allow Start() in all scripts to finish before player can see/hear the game
            StartCoroutine(RemoveLoadingScreen(loadingScreen)); 
        }

        IEnumerator RemoveLoadingScreen(GameObject screen)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            Destroy(screen);
            AudioListener.pause = false;
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
            isPreloadingScene = true;
            preloadSceneOpr = SceneManager.LoadSceneAsync(sceneName, loadMode);
            preloadSceneOpr.priority = -999;
            preloadSceneOpr.allowSceneActivation = false;
            Debug.LogWarning($"Scene \"{sceneName}\" loading in background.");
        }

        public void LoadPreloadedScene()
        {
            preloadSceneOpr.allowSceneActivation = true;
            SceneManager.sceneLoaded += SetLoadedSceneActive;
            isPreloadingScene = false;  // scene is loaded and no longer in async ops queue
        }

        //public void RemovePreloadedScene()
        //{
        //    // we have to finish loading the scene before we can remove it
        //    preloadSceneOpr.allowSceneActivation = true;

        //    while (!preloadSceneOpr.isDone)
        //    {
        //        Debug.LogWarning("Unloading preloaded scene.");
        //    }


        //}

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
            GameObject sceneStartRoom = GameObject.Find(startRoomName);
            return sceneStartRoom;
        }

        //public void SubscribeToOnLoadEvent(System.Action loadAction)
        //{
        //    loadAction += OnGameLoad;
        //}

        //public void UnSubscribeFromOnLoadEvent(System.Action loadAction)
        //{
        //    loadAction -= OnGameLoad;
        //}

        //void OnGameLoad()
        //{
        //    GoToLevel(SaveLoadManager.currentScene, SaveLoadManager.currentRoomName, true);
        //}
    }
}