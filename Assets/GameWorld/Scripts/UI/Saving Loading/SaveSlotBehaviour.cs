﻿using UnityEngine;
using UnityEngine.UI;


namespace Methodyca.Core
{
    // class for behavior of save slot buttons in UI
    public class SaveSlotBehaviour : MonoBehaviour
    {
        [SerializeField, Range(1,3)]
        private int saveSlot;
        [SerializeField]
        private Text saveDescription;

        private void OnEnable()
        {
            // display the latest save data info on the slot. Or show that it's empty
            SaveSlotInfo info = SaveLoadManager.GetSlotInfo(saveSlot);
            if (info != null)
                saveDescription.text = $"Save Game {info.saveSlotNumber} - {info.savedRoomName}\nMinigames Complete - {info.minigamesCompletedNumber}";
            else
                saveDescription.text = $"Save Game {saveSlot} - Empty";
        }

        // button click action
        public void SaveGame()
        {
            System.Action onSaveComplete = SaveLoadManager.SaveGameState(saveSlot); //**** needs testing to check if delegate does get called
            onSaveComplete += UpdateSlotInfo;
            onSaveComplete();
            onSaveComplete -= UpdateSlotInfo;
        }

        void UpdateSlotInfo()
        {
            SaveSlotInfo info = SaveLoadManager.GetSlotInfo(saveSlot);
            saveDescription.text = $"Save Game {info.saveSlotNumber} - {info.savedRoomName}\nMinigames Complete - {info.minigamesCompletedNumber}";
        }
    }

    [System.Serializable]
    public class SaveSlotInfo
    {
        public int saveSlotNumber = -1;
        public string savedRoomName = "N/A";
        public int minigamesCompletedNumber = -1;
    }
}