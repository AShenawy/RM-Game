using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Core
{
    // class for behavior of load slot buttons in UI
    public class LoadSlotBehaviour : MonoBehaviour
    {
        public event System.Action onLoadGame;
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
                saveDescription.text = $"Save Game {info.saveSlotNumber} - {info.savedRoomName}\nMinigames Complete - {info.minigamesCompletedNumber}";
                btn.interactable = true;
                SceneManagerScript.instance.SubscribeToOnLoadEvent(this);
            }
            else
            {
                // display empty slot and disable button from calling LoadGame()
                saveDescription.text = $"Save Game {saveSlot} - Empty";
                btn.interactable = false;
            }
        }

        private void OnDisable()
        {
            SceneManagerScript.instance.UnSubscribeFromOnLoadEvent(this);
        }

        // button click action
        public void LoadGame()
        {
            SaveLoadManager.LoadGameState(saveSlot);
            onLoadGame?.Invoke();   // for SceneManagerScript & others to use loaded info
        }
    }
}