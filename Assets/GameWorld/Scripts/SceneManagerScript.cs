using UnityEngine;
using UnityEngine.SceneManagement;

namespace Methodyca.Core
{
    /* This class handles moving the player between different scenes/levels in the game
     * and also protects some game objects from being destroyed when switching scenes
     */
    public class SceneManagerScript : MonoBehaviour
    {
        // make this class a singleton
        public static SceneManagerScript instance;

        [Header("Protected Game Objects")]
        [SerializeField, Tooltip("Game Manager game object")]
        private GameObject gameManagerGO;
        
        [SerializeField, Tooltip("Main UI canvas game object. It holds the top, bottom, and mobile phone UI")]
        private GameObject userInterfaceGO;

        private Scene sceneCurrent;


        private void Awake()
        {
            // make this class a singleton
            if (instance == null)
                instance = this;

            // Ensure game object which this script is on is available at all times
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            sceneCurrent = SceneManager.GetActiveScene();
        }

        public void GoToLevel(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
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

        //private void ProtectGameObjects()
        //{
        //    DontDestroyOnLoad(gameManagerGO);
        //    DontDestroyOnLoad(userInterfaceGO);
        //}
    }
}