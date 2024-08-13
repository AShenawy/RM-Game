using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Methodyca.Minigames.Utils
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private Texture2D cursor;
        [SerializeField] private GameObject loadProgressUIPanel;
        [SerializeField] private UnityEngine.UI.Slider loadProgressSlider;

        public void ChangeScene(string sceneName)
        {
            var opr = SceneManager.LoadSceneAsync(sceneName);
            StartCoroutine(LoadSceneOpr(opr));
        }

        private void Start()
        {
            if (cursor != null)
            {
                Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            }
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