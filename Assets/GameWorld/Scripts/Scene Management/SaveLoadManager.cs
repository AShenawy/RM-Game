using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Methodyca.Core
{
    public sealed class SaveLoadManager
    {
        [DllImport("__Internal")]
        private static extern void SyncFiles();

        public static bool autosaveAvailable { get; private set; }  // determines if Continue button is available or not on main menu
        public static int currentSceneIndex { get; private set; }
        public static string currentRoomName { get; private set; }
        public static List<string> currentInventoryItems
        {
            get { return currentInventoryItems; }
            private set { currentInventoryItems = new List<string>(); }
        }

        public static Dictionary<string, int> interactableStates
        {
            get { return interactableStates; }
            private set { interactableStates = new Dictionary<string, int>(); }
        }

        public static List<int> completedMinigames
        {
            get { return completedMinigames; }
            private set { completedMinigames = new List<int>(); }
        }

        // ====== Saving and loading player prefs for game settings (mute, volume, etc.) ======
        public static void SavePlayPref(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public static void SavePlayPref(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        private static void SetAutosaveAvailable()
        {
            autosaveAvailable = true;
            PlayerPrefs.SetInt("SaveSlot_0", 1);
        }

        // details saved on the save/load slots when manually saving/loading
        public static void SetSlotInfo(int slotNum, string roomName)
        {
            // autosave slot doesn't show up in save/load screen so shouldn't save info for it. Slots should be from 1 to 3
            if (slotNum == 0)
            {
                Debug.LogError("Warning: Attempting to save slot info on autosave slot. Action stopped.");
                return;
            }

            SaveSlotInfo info = new SaveSlotInfo();
            info.saveSlotNumber = slotNum;
            info.savedRoomName = roomName;
            info.minigamesCompletedNumber = completedMinigames.Count;

            PlayerPrefs.SetString($"SaveSlot_{slotNum}", JsonUtility.ToJson(info));
        }

        public static int LoadPlayPrefInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public static float LoadPlayPrefFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        private static void GetAutosaveAvailable()
        {
            if (!PlayerPrefs.HasKey("SaveSlot_0"))
                return;

            if (PlayerPrefs.GetInt("SaveSlot_0") == 1)
                autosaveAvailable = true;
        }

        public static SaveSlotInfo GetSlotInfo(int slotNum)
        {
            return JsonUtility.FromJson<SaveSlotInfo>(PlayerPrefs.GetString($"SaveSlot_{slotNum}"));
        }

        // ====== Saving and loading game states ======
        public static void SetCurrentScene(int sceneIndex)
        {
            currentSceneIndex = sceneIndex;
        }

        public static void SetCurrentRoom(string name)
        {
            currentRoomName = name;
        }

        public static void SetCurrentInventoryItems(string[] items)
        {
            // refresh the list of held items
            currentInventoryItems.Clear();
            foreach (string item in items)
            {
                currentInventoryItems.Add(item);
            }
        }

        public static void SetInteractableState(string objectName, int value)
        {
            // either set a new value if the key isn't there or update the existing one
            if (!interactableStates.ContainsKey(objectName))
                interactableStates.Add(objectName, value);
            else
                interactableStates[objectName] = value;
        }

        // to refresh the dictionary between scenes. removes unneeded information from previous scene
        public static void ClearInteractableState()
        {
            interactableStates.Clear();
        }

        public static void SaveGameAuto()
        {
            // set up a new state and fill with current information
            SaveStates state = new SaveStates();
            state.stateNum = 0; // the state number for autosaving
            state.sceneIndex = currentSceneIndex;
            state.roomName = currentRoomName;
            state.itemsHeld = currentInventoryItems;

            foreach (var kvp in interactableStates)
            {
                state.objectInteractionName.Add(kvp.Key);
                state.isObjectInteracted.Add(kvp.Value);
            }

            // save the new state to file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/SavedGames/AutoSave.mth");
            bf.Serialize(file, state);
            file.Close();
            SetAutosaveAvailable();
            SyncFiles();    // ensure browser syncs save file to local indexed DB file system
            Debug.Log("Autosave complete.");
        }

        public static void LoadGameAuto()
        {
            // load the autosave file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SavedGames/AutoSave.mth", FileMode.Open);
            SaveStates state = (SaveStates)bf.Deserialize(file);
            file.Close();

            // update current information for other scripts to pull
            currentSceneIndex = state.sceneIndex;
            currentRoomName = state.roomName;
            currentInventoryItems = state.itemsHeld;

            interactableStates.Clear();
            for (int i = 0; i < state.objectInteractionName.Count; i++)
                interactableStates.Add(state.objectInteractionName[i], state.isObjectInteracted[i]);


            Debug.Log("Autosave loading complete.");
        }

        public static void SaveGameState(int slotNum)
        {
            // set up a new state and fill with current information
            SaveStates state = new SaveStates();
            state.stateNum = slotNum; // the state number for autosaving
            state.sceneIndex = currentSceneIndex;
            state.roomName = currentRoomName;
            state.itemsHeld = currentInventoryItems;

            foreach (var kvp in interactableStates)
            {
                state.objectInteractionName.Add(kvp.Key);
                state.isObjectInteracted.Add(kvp.Value);
            }

            // save the new state to file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + $"/SavedGames/Save_{slotNum}.mth");
            bf.Serialize(file, state);
            file.Close();
            SetSlotInfo(slotNum, currentRoomName);  // update the playerprefs for save/load UI
            SyncFiles();    // ensure browser syncs save file to local indexed DB file system
            Debug.Log($"Saving to slot {slotNum} complete.");
        }

        public static void LoadGameState(int slotNum)
        {
            // load the autosave file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + $"/SavedGames/Save_{slotNum}.mth", FileMode.Open);
            SaveStates state = (SaveStates)bf.Deserialize(file);
            file.Close();

            // update current information for other scripts to pull
            currentSceneIndex = state.sceneIndex;
            currentRoomName = state.roomName;
            currentInventoryItems = state.itemsHeld;

            interactableStates.Clear();
            for (int i = 0; i < state.objectInteractionName.Count; i++)
                interactableStates.Add(state.objectInteractionName[i], state.isObjectInteracted[i]);


            Debug.Log($"Loading from slot {slotNum} complete.");
        }
    }

    public interface ISaveable
    {
        void SaveState();
    }

    public interface ILoadable
    {
        void LoadState();
    }

    [System.Serializable]
    public class SaveStates
    {
        // autosave number is 0. When including save slots they will start from 1 and up
        public int stateNum;

        // last scene (by index) player was in
        public int sceneIndex;

        // last room player was in
        public string roomName;

        // inventory items held
        public List<string> itemsHeld = new List<string>();

        // interactable items dictionary into list
        public List<string> objectInteractionName = new List<string>();
        public List<int> isObjectInteracted = new List<int>(); // value should be either 0 (false) or 1 (true)
    }
}