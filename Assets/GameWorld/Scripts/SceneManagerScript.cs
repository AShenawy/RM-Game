﻿using UnityEngine;
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

        public void GoToLevel(string sceneName, string roomTag = "Starting Room")   // Default start room tag in every scene
        {
            startRoomTag = roomTag;
            SceneManager.LoadScene(sceneName);
            // TODO: Load and unload scenes behind a loading screen
            // loadingScreen = Object.Instantiate(Resources.Load("Loading Screen") as GameObject);

            // Give the player the dimension switcher on Act 2 entry
            Scene loadedScene = SceneManager.GetActiveScene();
            if (loadedScene.name == "Act 2")
                GiveSwitcherItem();
        }

        void GiveSwitcherItem()
        {
            InventoryManager.instance.GiveSwitcherItem();
        }

        public void LoadSceneAdditive(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

        public void UnloadScene(string scene)
        {
            SceneManager.UnloadSceneAsync(scene);
            
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