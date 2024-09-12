using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Core
{
    // class for behavior of load slot buttons in UI
    public class LoadSlotBehaviour : MonoBehaviour
    {
        //public event System.Action onLoadGame;
        [SerializeField, Range(1, 3)]
        private int saveSlot;
        [SerializeField]
        private Text saveDescription;
        private Button btn;

        private void Awake()
        {
            btn = GetComponent<Button>();
        }

        private void OnEnable()
        {
            SaveSlotInfo info = SaveLoadManager.GetSlotInfo(saveSlot);
            if (info != null)
            {
                // display save quick info and enable button to call LoadGame()
                saveDescription.text = $"Slot {info.saveSlotNumber} - {info.savedRoomName}\nMinigames Complete - {info.minigamesCompletedNumber}" +
                    $"\n{System.DateTime.FromBinary(System.Convert.ToInt64(info.dateTime))}";
                btn.interactable = true;
                //SceneManagerScript.instance.SubscribeToOnLoadEvent(onLoadGame);
            }
            else
            {
                // display empty slot and disable button from calling LoadGame()
                saveDescription.text = $"Slot {saveSlot} - Empty";
                btn.interactable = false;
            }
        }

        //TODO remove unused code
        //private void OnDisable()
        //{
        //    SceneManagerScript.instance.UnSubscribeFromOnLoadEvent(onLoadGame);
        //}

        // button click action
        public void LoadGame()
        {
            SaveLoadManager.LoadGameState(saveSlot);
            //onLoadGame?.Invoke();
            // Clear so no badge data is transferred to the next loaded instance
            SceneManagerScript.instance.minigamesWon.Clear();
            SceneManagerScript.instance.GoToLevel(SaveLoadManager.currentScene, SaveLoadManager.currentRoomName);
        }
    }
}