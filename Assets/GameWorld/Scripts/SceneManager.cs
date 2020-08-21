using UnityEngine;
using UnityEngine.SceneManagement;

namespace Methodyca.Core
{
    /* This class handles moving the player between different scenes/levels in the game
     * and also protects some game objects from being destroyed when switching scenes
     */
    public class SceneManager : MonoBehaviour
    {
        // make this class a singleton
        public static SceneManager instance;

        [Header("Protected Game Objects")]
        [SerializeField] private GameObject gameManagerGO;
        [SerializeField] private GameObject userInterfaceGO;


        private void Awake()
        {
            // make this class a singleton
            if (instance == null)
                instance = this;

            ProtectGameObjects();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void ProtectGameObjects()
        {
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(gameManagerGO);
            DontDestroyOnLoad(userInterfaceGO);
        }
    }
}