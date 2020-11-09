using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadManager
{
    

    public void SaveJSON(string key, Object saveObj)
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(saveObj));
    }

    public void SaveJSON(string key, PlayerData saveObj)
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(saveObj));
    }

    public void LoadJSON(string key, Object loadObj)
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), loadObj);
    }

    public void LoadJSON(string key, PlayerData loadObj)
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), loadObj);
    }

    public PlayerData LoadJSON(string key)
    {
        return JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(key));
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

public class SaveStates
{
    // autosave number is 0. When including save slots they will start from 1 and up
    public int stateNum;

    public Dictionary<PickUp, int> pickableItemsStates;

    public MoveObject moveableItemsStates;
}