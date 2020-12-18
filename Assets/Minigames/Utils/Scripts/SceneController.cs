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
            StartCoroutine(LoadSceneCor(index));
        }

        public void ChangeScene(string sceneName)
        {
            var scene = SceneManager.GetSceneByName(sceneName);
            StartCoroutine(LoadSceneCor(scene.buildIndex));
        }

        private IEnumerator LoadSceneCor(int index)
        {
            var opr = SceneManager.LoadSceneAsync(index);

            if (loadProgressUIPanel == null || loadProgressSlider == null)
            {
                yield return null;
            }

            loadProgressUIPanel.SetActive(true);

            while (!opr.isDone)
            {
                var progress = Mathf.Clamp01(opr.progress / .9f);

                loadProgressSlider.value = progress;
                yield return null;
            }

            loadProgressUIPanel.SetActive(false);
        }
    }
}