using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Core
{
    public class AutoLoadSlotBehaviour : MonoBehaviour
    {
        public event System.Action onLoadGame;
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

            SceneManagerScript.instance.SubscribeToOnLoadEvent(this);
        }

        private void OnDisable()
        {
            SceneManagerScript.instance.UnSubscribeFromOnLoadEvent(this);
        }

        // button click action
        public void LoadAutoSave()
        {
            SaveLoadManager.LoadGameAuto();
            onLoadGame?.Invoke();   // for SceneManagerScript & others to use loaded info
        }
    }
}