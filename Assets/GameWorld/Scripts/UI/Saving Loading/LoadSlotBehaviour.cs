﻿using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Core
{
    // class for behavior of load slot buttons in UI
    public class LoadSlotBehaviour : MonoBehaviour
    {
        public event System.Action onLoadGame;
        [SerializeField]
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
                saveDescription.text = $"Slot {info.saveSlotNumber} - {info.saveSlotNumber}\nMinigames Complete - {info.minigamesCompletedNumber}";
                btn.interactable = true;
            }
            else
            {
                // display empty slot and disable button from calling LoadGame()
                saveDescription.text = $"Slot {saveSlot} - Empty";
                btn.interactable = false;
            }
        }

        // button click action
        public void LoadGame()
        {
            SaveLoadManager.LoadGameState(saveSlot);
            onLoadGame?.Invoke();   // for SceneManagerScript & others to use loaded info
        }
    }
}