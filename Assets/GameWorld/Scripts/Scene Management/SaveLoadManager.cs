using UnityEngine;
using System.Collections;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

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
