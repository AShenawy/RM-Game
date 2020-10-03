using UnityEngine;

namespace Methodyca.Minigames.SortGame
{
    // this script handles the sorting minigame gameplay
    public class SortingManager : MonoBehaviour
    {
        public delegate void OnGameComplete();
        public event OnGameComplete gameComplete;

        public SortBoxBehaviour QNBox;
        public SortBoxBehaviour QLBox;
        public int requiredItemsInBox;
        public CanvasGroup buttonsPanel;
        public GameObject winScreen;

        //public bool filesArranged;    // **************** this field seems unused. removed
        //public bool completed;        //*********** not needed. removed

        [Header("Sound")]
        [Tooltip("The Background Music track during game")] public Sound BGM;
        public Sound gameWinTune;

        bool QNBoxSorted;
        bool QLBoxSorted;
        //SoundManager soundMan;        //***** new SoundManager setup makes this redundant

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
            //soundMan = FindObjectOfType<SoundManager>();

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
                Complete();
        }

        void Complete()
        {
            //if(!completed)  // ******************* is check necessary?
            //{
            //completed = true;         //************ unused. removed
            //}

            gameComplete?.Invoke();
            winScreen.SetActive(true);
            //soundMan.Stop("Fraud Full");     //******* with the new SoundManager setup we can simply switch to another BGM straight away
            //soundMan.Play("comp");
            SoundManager.instance.PlayBGM(gameWinTune);
            
            //soundMan.Stop("static");
            //soundMan.Play("staticL");         //******* according to Kewa this is no longer needed to be played
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
