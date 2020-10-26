using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void LoadLevel(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
