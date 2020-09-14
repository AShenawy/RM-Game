using UnityEngine;
using UnityEngine.SceneManagement;

namespace Methodyca.Minigames.Questioniser
{
    public class SceneController : MonoBehaviour
    {
        public void ReloadSceneByIndex(int index)
        {
            SceneManager.UnloadSceneAsync(index).completed += (opr) =>
            {
                ChangeScene(index);
            };
        }

        public void ChangeScene(int index)
        {
            SceneManager.LoadSceneAsync(0).completed += (opr) => SceneManager.LoadScene(index);
        }

        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(0).completed += (opr) => SceneManager.LoadScene(sceneName);
        }
    }
}