using System.Collections;
using UnityEngine;
using Methodyca.Core;

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
        public Crystal blueCrystal;
        public Crystal pinkCrystal;

        [Header("Sound")]
        [Tooltip("The Background Music track during game")] public Sound BGM;
        public Sound gameWinTune;

        bool QNBoxSorted;
        bool QLBoxSorted;
        int blueCurrentPhase;
        int pinkCurrentPhase;

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
            //if (correctItemsCount == requiredItemsInBox && QNBox.inTheBox.Count == requiredItemsInBox)
            //{
            //    QNBoxSorted = true;
            //}
            //else
            //{
            //    QNBoxSorted = false;
            //}

            //The following paragraph was temporarily used to rescue the bug.
            //The blueCurrentPhase of the blue crystal in this script
            //is always not the same as the currentPhase in the Crystal.cs script,
            //while everything is fine with the pink crystal.
            //I couldn't find the reason.
            if (blueCrystal.currentPhase>0)
            {
                blueCurrentPhase = blueCrystal.currentPhase+1;
            }
            else if(blueCrystal.currentPhase<0)
            {
                blueCurrentPhase = blueCrystal.currentPhase-1;
            }
            else
            {
                blueCurrentPhase = blueCrystal.currentPhase;
            }

            if (blueCurrentPhase >= 5)
            {
                QNBoxSorted = true;
                Debug.Log("Blue Current Phase Completed");
            }
            else
            {
                QNBoxSorted = false;
                Debug.Log("Blue Current Phase in CheckQNSorted: " + blueCurrentPhase);
            }

            CheckGameComplete();
        }

        void CheckQLSorted(int correctItemsCount)
        {
            //if (correctItemsCount == requiredItemsInBox && QLBox.inTheBox.Count == requiredItemsInBox)
            //{
            //    QLBoxSorted = true;
            //}
            //else
            //{
            //    QLBoxSorted = false;
            //}
            pinkCurrentPhase = pinkCrystal.currentPhase;
            if (pinkCurrentPhase >= 5)
            {
                QLBoxSorted = true;
                Debug.Log("Pink Current Phase Completed");
            }
            else
            {
                QLBoxSorted = false;
                Debug.Log("Pink Current Phase in CheckQLSorted: " + pinkCurrentPhase);
            }

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
