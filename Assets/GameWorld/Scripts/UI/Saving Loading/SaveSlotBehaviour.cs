using UnityEngine;
using UnityEngine.UI;


namespace Methodyca.Core
{
    // class for behavior of save slot buttons in UI
    public class SaveSlotBehaviour : MonoBehaviour
    {
        public int saveSlot;
        private Text saveDescription;

        void OnEnable()
        {
            SaveSlotInfo info = SaveLoadManager.GetSlotInfo(saveSlot);
            saveDescription.text = $"Slot {info.saveSlotNumber} - {info.saveSlotNumber}\nMinigames Complete - {info.minigamesCompletedNumber}";
        }

        public void SaveGame()
        {
            SaveLoadManager.SaveGameState(saveSlot);
        }
    }

    [System.Serializable]
    public class SaveSlotInfo
    {
        public int saveSlotNumber;
        public string savedRoomName;
        public int minigamesCompletedNumber;
    }
}