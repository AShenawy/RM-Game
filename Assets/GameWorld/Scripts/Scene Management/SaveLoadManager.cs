using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Methodyca.Core
{
    public static class SaveLoadManager
    {
        public static string currentScene { get; private set; }
        public static string currentRoomName { get; private set; }
        public static List<string> currentInventoryItems = new List<string>();

        // string is the object's name and int represents bool for interaction done (1/true) or not (0/false)
        public static Dictionary<string, int> interactableStates = new Dictionary<string, int>();
        
        // minigames IDs start with Sorting at ID 0
        public static List<int> completedMinigamesIDs = new List<int>();

        private static Action onSaveComplete;

        // JS interaction with browser to force file saving on system
        [DllImport("__Internal")]
        private static extern void SyncFiles();

        // ====== Saving and loading player prefs for game settings (mute, volume, etc.) ======
        public static void SavePlayPref(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public static void SavePlayPref(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        private static void SetAutosaveAvailable(bool value)
        {
            if (value)
                PlayerPrefs.SetInt("SaveSlot_0", 1);
            else
                PlayerPrefs.SetInt("SaveSlot_0", 0);
        }

        // details saved on the save/load slots when manually saving/loading
        public static void SetSlotInfo(int slotNum, string roomName)
        {
            //// autosave slot doesn't show up in save/load screen so shouldn't save info for it. Slots should be from 1 to 3
            //if (slotNum == 0)
            //{
            //    Debug.LogError("Warning: Attempting to save slot info on autosave slot. Action stopped.");
            //    return;
            //}

            SaveSlotInfo info = new SaveSlotInfo();
            info.saveSlotNumber = slotNum;
            info.savedRoomName = roomName;
            info.minigamesCompletedNumber = completedMinigamesIDs.Count;
            info.dateTime = DateTime.Now.ToBinary().ToString();

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

        public static SaveSlotInfo GetAutosaveInfo()
        {
            string savePath = Path.Combine(Application.persistentDataPath, "SavedGames", "AutoSave.mth");

            if (!PlayerPrefs.HasKey("SaveSlot_0") || !File.Exists(savePath))
            {
                PlayerPrefs.DeleteKey("SaveSlot_0");    // if file doesn't exist
                Debug.LogWarning($"Autosave file not found in {savePath}");
                return null;
            }
            else
                return JsonUtility.FromJson<SaveSlotInfo>(PlayerPrefs.GetString("SaveSlot_0"));

            //if (PlayerPrefs.GetInt("SaveSlot_0") == 1)
            //    return true;
            //else                
            //    // Autosave not available at this moment
            //    return null;
        }

        public static SaveSlotInfo GetSlotInfo(int slotNum)
        {
            if (PlayerPrefs.HasKey($"SaveSlot_{slotNum}") && SaveFileExists(slotNum))
                return JsonUtility.FromJson<SaveSlotInfo>(PlayerPrefs.GetString($"SaveSlot_{slotNum}"));
            else
            {
                // if save file doesn't exist then key should be removed
                PlayerPrefs.DeleteKey($"SaveSlot_{ slotNum}");
                Debug.LogWarning($"No save data found for Save Slot {slotNum}");
                return null;
            }
        }

        public static int GetLatestSaveSlot()
        {
            // set comparison parameters
            DateTime latestDateTime = DateTime.MinValue;
            int latestSlotNum = -1;

            SaveSlotInfo[] slotsInfo = new SaveSlotInfo[4];
            for (int i = 0; i < slotsInfo.Length; i++)
            {
                if (PlayerPrefs.HasKey($"SaveSlot_{i}") && SaveFileExists(i))
                {
                    slotsInfo[i] = JsonUtility.FromJson<SaveSlotInfo>(PlayerPrefs.GetString($"SaveSlot_{i}"));
                    DateTime slotDateTime = DateTime.FromBinary(Convert.ToInt64(slotsInfo[i].dateTime));

                    // if the slot date/time is later than the comparing var then set it as latest save slot
                    if (DateTime.Compare(slotDateTime, latestDateTime) > 0)
                    {
                        latestDateTime = slotDateTime;
                        latestSlotNum = slotsInfo[i].saveSlotNumber;
                    }
                }
            }

            return latestSlotNum;
        }

        public static void ClearPlayPrefsAll()
        {
            PlayerPrefs.DeleteAll();
        }

        // ====== Saving and loading game states ======
        public static void SetCurrentScene(string scene)
        {
            currentScene = scene;
        }

        public static void SetCurrentRoom(string name)
        {
            currentRoomName = name;
        }

        public static void SetCompletedMinigamesList(List<int> IDs)
        {
            // refresh the list of completed games
            completedMinigamesIDs.Clear();
            foreach (int id in IDs)
                completedMinigamesIDs.Add(id);
        }

        public static void SetMinigameComplete(Minigames game)
        {
            // only add the minigame to the list if it doesn't contain it
            if (completedMinigamesIDs.Contains((int)game))
                return;

            completedMinigamesIDs.Add((int)game);
        }

        //TODO remove if unused and below methods working
        public static void SetCurrentInventoryItems(List<Item> items)
        {
            // Convert the Item list to a string array (faster) of names
            List<string> itemNames = new List<string>();
            foreach (Item i in items)
                itemNames.Add(i.name);

            // Include new items added to player inventory
            foreach (string s in itemNames)
            {
                if (!currentInventoryItems.Contains(s))
                    currentInventoryItems.Add(s);
            }

            // Discard items removed from player inventory
            foreach (string s in currentInventoryItems.ToArray())
            {
                if (!itemNames.Contains(s))
                    currentInventoryItems.Remove(s);
            }
        }

        public static void AddInventoryItem(Item item)
        {
            if (!currentInventoryItems.Contains(item.name))
                currentInventoryItems.Add(item.name);
        }

        public static void RemoveInventoryItem(Item item)
        {
            if (currentInventoryItems.Contains(item.name))
                currentInventoryItems.Remove(item.name);
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

        public static void ClearInventoryItems()
        {
            currentInventoryItems.Clear();
        }

        public static void ClearMinigamesComplete()
        {
            completedMinigamesIDs.Clear();
        }

        public static void SaveGameAuto()
        {
            // set up a new state and fill with current information
            SaveStates state = new SaveStates();
            state.stateNum = 0; // the state number for autosaving
            state.sceneName = currentScene;
            state.roomName = currentRoomName;
            state.itemsHeld = currentInventoryItems;
            state.minigames = completedMinigamesIDs;

            foreach (var kvp in interactableStates)
            {
                state.objectInteractionName.Add(kvp.Key);
                state.isObjectInteracted.Add(kvp.Value);
            }

            // save the new state to file
            BinaryFormatter bf = new BinaryFormatter();
            string savePath = Path.Combine(Application.persistentDataPath, "SavedGames");
            Directory.CreateDirectory(savePath);
            savePath = Path.Combine(savePath, "AutoSave.mth");
            FileStream file = File.Create(savePath);
            bf.Serialize(file, state);
            file.Close();
            //SetAutosaveAvailable(true);
            SetSlotInfo(0, currentRoomName);

            if (Application.platform == RuntimePlatform.WebGLPlayer)
                SyncFiles();    // ensure browser syncs save file to local indexed DB file system

            Debug.Log("Autosave complete.");
        }

        public static Action SaveGameState(int slotNum)
        {
            // set up a new state and fill with current information
            SaveStates state = new SaveStates();
            state.stateNum = slotNum; // the state number for autosaving
            state.sceneName = currentScene;
            state.roomName = currentRoomName;
            state.itemsHeld = currentInventoryItems;
            state.minigames = completedMinigamesIDs;

            foreach (var kvp in interactableStates)
            {
                state.objectInteractionName.Add(kvp.Key);
                state.isObjectInteracted.Add(kvp.Value);
            }

            // save the new state to file
            BinaryFormatter bf = new BinaryFormatter();
            string savePath = Path.Combine(Application.persistentDataPath, "SavedGames");
            Directory.CreateDirectory(savePath);
            savePath = Path.Combine(savePath, $"Save_{slotNum}.mth");
            FileStream file = File.Create(savePath);
            bf.Serialize(file, state);
            file.Close();
            SetSlotInfo(slotNum, currentRoomName);  // update the playerprefs for save/load UI

            if (Application.platform == RuntimePlatform.WebGLPlayer)
                SyncFiles();    // ensure browser syncs save file to local indexed DB file system

            Debug.Log($"Saving to slot {slotNum} complete.");
            return onSaveComplete;
        }

        public static void LoadGameAuto()
        {
            // Clear current states for new file
            currentScene = "";
            currentRoomName = "";
            currentInventoryItems.Clear();
            interactableStates.Clear();
            completedMinigamesIDs.Clear();

            // load the autosave file
            BinaryFormatter bf = new BinaryFormatter();
            string savePath = Path.Combine(Application.persistentDataPath, "SavedGames");
            Directory.CreateDirectory(savePath);
            savePath = Path.Combine(savePath, "AutoSave.mth");

            if (!File.Exists(savePath))
            {
                Debug.LogWarning($"Autosave file not found in {savePath}");
                return;
            }
            
            FileStream file = File.Open(savePath, FileMode.Open);
            SaveStates state = (SaveStates)bf.Deserialize(file);
            file.Close();

            // update current information for other scripts to pull
            currentScene = state.sceneName;
            currentRoomName = state.roomName;
            currentInventoryItems = state.itemsHeld;
            completedMinigamesIDs = state.minigames;

            //interactableStates.Clear();       // ---- moved up
            for (int i = 0; i < state.objectInteractionName.Count; i++)
                interactableStates.Add(state.objectInteractionName[i], state.isObjectInteracted[i]);

            Debug.Log("Autosave loading complete.");
        }

        public static void LoadGameState(int slotNum)
        {
            // Clear current states for new file
            currentScene = "";
            currentRoomName = "";
            currentInventoryItems.Clear();
            interactableStates.Clear();
            completedMinigamesIDs.Clear();

            // load the autosave file
            BinaryFormatter bf = new BinaryFormatter();
            string savePath = Path.Combine(Application.persistentDataPath, "SavedGames");
            Directory.CreateDirectory(savePath);
            savePath = Path.Combine(savePath, $"Save_{slotNum}.mth");

            if (!SaveFileExists(slotNum))
            {
                Debug.LogWarning($"No save file found in {savePath} for slot {slotNum}");
                return;
            }

            FileStream file = File.Open(savePath, FileMode.Open);
            SaveStates state = (SaveStates)bf.Deserialize(file);
            file.Close();

            // update current information for other scripts to pull
            currentScene = state.sceneName;
            currentRoomName = state.roomName;
            currentInventoryItems = state.itemsHeld;
            completedMinigamesIDs = state.minigames;

            interactableStates.Clear();
            for (int i = 0; i < state.objectInteractionName.Count; i++)
                interactableStates.Add(state.objectInteractionName[i], state.isObjectInteracted[i]);

            Debug.Log($"Loading from slot {slotNum} complete.");
        }

        static bool SaveFileExists(int slotNum)
        {
            string savePath;
            if (slotNum == 0)
                savePath = Path.Combine(Application.persistentDataPath, "SavedGames", $"AutoSave.mth");
            else
                savePath = Path.Combine(Application.persistentDataPath, "SavedGames", $"Save_{slotNum}.mth");


            if (File.Exists(savePath))
                return true;
            else
                return false;
        }

        // Delete all saved games and player prefs
        public static void DeleteAllSaveData()
        {
            ClearPlayPrefsAll();
            // delete autosave
            string savePath = Path.Combine(Application.persistentDataPath, "SavedGames", "AutoSave.mth");
            File.Delete(savePath);
            // delete save 1
            savePath = Path.Combine(Application.persistentDataPath, "SavedGames", "Save_1.mth");
            File.Delete(savePath);
            // delete save 2
            savePath = Path.Combine(Application.persistentDataPath, "SavedGames", "Save_2.mth");
            File.Delete(savePath);
            // delete save 3
            savePath = Path.Combine(Application.persistentDataPath, "SavedGames", "Save_3.mth");
            File.Delete(savePath);
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
        public string sceneName;

        // last room player was in
        public string roomName;

        // inventory items held
        public List<string> itemsHeld = new List<string>();

        // split interactable items dictionary into 2 lists
        public List<string> objectInteractionName = new List<string>();
        public List<int> isObjectInteracted = new List<int>(); // value should be either 0 (false) or 1 (true)

        // minigames complete
        public List<int> minigames = new List<int>();
    }
}