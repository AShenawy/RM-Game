﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Methodyca.Minigames.Questioniser
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] Texture2D cursor;
        [SerializeField] GameObject loadProgressCanvas;
        [SerializeField] UnityEngine.UI.Slider loadProgressSlider;

        //public Sound BGMMainMenu;
        //public Sound BGMLevel;

        public void ChangeScene(int index)
        {
            StartCoroutine(LoadSceneCor(index));
            //SoundManager.instance.PlayBGM(BGMMainMenu);
        }

        void Start()
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }

        IEnumerator LoadSceneCor(int index)
        {
            var opr = SceneManager.LoadSceneAsync(index);
            loadProgressCanvas.SetActive(true);
            //SoundManager.instance.PlayBGM(BGMLevel);
            //Debug.Log("swtiching Scene");
            

            while (!opr.isDone)
            {
                var progress = Mathf.Clamp01(opr.progress / .9f);

                loadProgressSlider.value = progress;
                yield return null;
            }

            loadProgressCanvas.SetActive(false);
        }
    }
}