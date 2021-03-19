using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Core
{
    // script for first saved game slot (autosave) in load game screen (main menu)
    public class AutoLoadSlotBehaviour : MonoBehaviour
    {
        //public event System.Action onLoadGame;
        [SerializeField]
        private Text saveDescription;
        private Button btn;


        private void Awake()
        {
            btn = GetComponent<Button>();
        }

        private void OnEnable()
        {
            SaveSlotInfo info = SaveLoadManager.GetAutosaveInfo();

            if (info == null)
            {
                // display empty slot and disable button from calling LoadGame()
                saveDescription.text = "No auto-save data available";
                btn.interactable = false;
            }
            else
            {
                // display save quick info and enable button to call LoadGame()
                saveDescription.text = $"Auto-Save - {info.savedRoomName}\nMinigames Complete - {info.minigamesCompletedNumber}" +
                    $"\n{System.DateTime.FromBinary(System.Convert.ToInt64(info.dateTime))}";
                btn.interactable = true;
            }

            //SceneManagerScript.instance.SubscribeToOnLoadEvent(onLoadGame);
        }

        //private void OnDisable()
        //{
        //    SceneManagerScript.instance.UnSubscribeFromOnLoadEvent(onLoadGame);
        //}

        // button click action
        public void LoadAutoSave()
        {
            SaveLoadManager.LoadGameAuto();
            //onLoadGame?.Invoke();

            // Clear so no badge data is transferred to the next loaded instance
            SceneManagerScript.instance.minigamesWon.Clear();
            SceneManagerScript.instance.GoToLevel(SaveLoadManager.currentScene, SaveLoadManager.currentRoomName);
        }
    }
}