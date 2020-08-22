using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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