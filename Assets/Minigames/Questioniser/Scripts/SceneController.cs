using UnityEngine;
using UnityEngine.SceneManagement;

namespace Methodyca.Minigames.Questioniser
{
    public class SceneController : MonoBehaviour
    {
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}