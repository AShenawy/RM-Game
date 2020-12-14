using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Methodyca.Minigames.Utils
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] GameObject loadProgressUIPanel;
        [SerializeField] UnityEngine.UI.Slider loadProgressSlider;

        public void ChangeScene(int index)
        {
            var opr = SceneManager.LoadSceneAsync(index);
            StartCoroutine(LoadSceneOpr(opr));
        }

        public void ChangeScene(string sceneName)
        {
            var opr = SceneManager.LoadSceneAsync(sceneName);
            StartCoroutine(LoadSceneOpr(opr));
        }

        private IEnumerator LoadSceneOpr(AsyncOperation operation)
        {
            if (loadProgressUIPanel == null || loadProgressSlider == null)
            {
                yield return null;
            }

            loadProgressUIPanel.SetActive(true);

            while (!operation.isDone)
            {
                var progress = Mathf.Clamp01(operation.progress / .9f);

                loadProgressSlider.value = progress;
                yield return null;
            }

            loadProgressUIPanel.SetActive(false);
        }
    }
}