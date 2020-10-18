using UnityEngine;

namespace Methodyca.Core
{
    public class ReturnToMainGame : MonoBehaviour
    {
        public string sceneToClose;

        public void ReturnToGame()
        {
            SceneManagerScript.instance.UnloadScene(sceneToClose);
        }
    }
}