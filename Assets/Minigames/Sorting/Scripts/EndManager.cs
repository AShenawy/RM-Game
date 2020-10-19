using UnityEngine;
using UnityEngine.SceneManagement;
using Methodyca.Core;

namespace Methodyca.Minigames.SortGame
{
    public class EndManager : MonoBehaviour
    {
        public void ResetGame(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ExitMinigame(string name)
        {
            SceneManagerScript.instance.UnloadScene(name);
        }
    }
}
