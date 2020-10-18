using System.Collections;
using UnityEngine;

namespace Methodyca.Minigames.SortGame
{
    // this script handles the sorting minigame gameplay
    public class SortingManager : MonoBehaviour
    {
        public SortBoxBehaviour QNBox;
        public SortBoxBehaviour QLBox;
        public int requiredItemsInBox;
        public CanvasGroup buttonsPanel;
        public GameObject winScreen;

        [Header("Sound")]
        [Tooltip("The Background Music track during game")] public Sound BGM;
        public Sound gameWinTune;

        bool QNBoxSorted;
        bool QLBoxSorted;

        private void OnEnable()
        {
            QNBox.onItemDropped += CheckQNSorted;
            QLBox.onItemDropped += CheckQLSorted;
        }

        private void OnDisable()
        {
            QNBox.onItemDropped -= CheckQNSorted;
            QLBox.onItemDropped -= CheckQLSorted;
        }

        void Start()
        {
            winScreen.SetActive(false);

            SoundManager.instance.PlayBGM(BGM);
        }

        void CheckQNSorted(int correctItemsCount)
        {
            if (correctItemsCount == requiredItemsInBox && QNBox.inTheBox.Count == requiredItemsInBox)
                QNBoxSorted = true;
            else
                QNBoxSorted = false;

            CheckGameComplete();
        }

        void CheckQLSorted(int correctItemsCount)
        {
            if (correctItemsCount == requiredItemsInBox && QLBox.inTheBox.Count == requiredItemsInBox)
                QLBoxSorted = true;
            else
                QLBoxSorted = false;

            CheckGameComplete();
        }

        void CheckGameComplete()
        {
            if (QNBoxSorted && QLBoxSorted)
                StartCoroutine(CompleteGame());
        }

        IEnumerator CompleteGame()
        {
            yield return new WaitForSeconds(0.5f);
            winScreen.SetActive(true);
            SoundManager.instance.ChangeAllSFXVolume(0.13f);
            SoundManager.instance.PlayBGM(gameWinTune);
        }

        public void ResetLayout()
        {
            QNBox.EmptyBox();
            QLBox.EmptyBox();
        }

        public void EnableButtons(bool value)
        {
            buttonsPanel.blocksRaycasts = value;
        }
    }
}
