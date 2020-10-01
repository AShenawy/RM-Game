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
        public AudioClip BGM;

        //public bool filesArranged;    // **************** this field seems unused. removed
        //public bool completed;        //*********** not needed. removed
        public GameObject winScreen;
        
        bool QNBoxSorted;
        bool QLBoxSorted;
        SoundManager soundMan;

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
            soundMan = FindObjectOfType<SoundManager>();
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
                Complete();
        }

        void Complete()
        {
            //if(!completed)  // ******************* is check necessary?
            //{
                //completed = true;         //************ unused. removed
            //}

            winScreen.SetActive(true);
            soundMan.Stop("Fraud Full");
            soundMan.Stop("static");
            soundMan.Play("comp");
            soundMan.Play("staticL");
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
