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
        [SerializeField] private GameObject gameManagerGO;
        [SerializeField] private GameObject userInterfaceGO;

        private Scene sceneCurrent;


        private void Awake()
        {
            // make this class a singleton
            if (instance == null)
                instance = this;

            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            sceneCurrent = SceneManager.GetActiveScene();
        }

        public void GoToLevel(string sceneName)
        {
            
        }

        public void GoToLevel(int sceneIndex)
        {

        }

        public void GoToMinigame(string sceneName)
        {

        }

        public void GoToMinigame(int sceneIndex)
        {

        }

        private void ProtectGameObjects()
        {
            DontDestroyOnLoad(gameManagerGO);
            DontDestroyOnLoad(userInterfaceGO);
        }
    }
}