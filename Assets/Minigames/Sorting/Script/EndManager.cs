using UnityEngine;
using UnityEngine.SceneManagement;

namespace Methodyca.Minigames.SortGame
{
    public class EndManager : MonoBehaviour
    {
        public void ResetGame(string sceneName)
        {
            Debug.Log("game reset");
            SceneManager.LoadScene(sceneName);
        }
    }
}
