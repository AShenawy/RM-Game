using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;


public class DictionarySave : MonoBehaviour
{
    public Dictionary<string, int> dic;

    [DllImport("__Internal")]
    private static extern void SyncFiles();

    public void FillDic()
    {
        dic = new Dictionary<string, int>();

        dic.Add("SaveGame 0", 0);
        dic.Add("SaveGame 1", 1);

        print("Dictionary content: \nSaveGame 0 - " + dic["SaveGame 0"] + ".\nSaveGame 1 - " + dic["SaveGame 1"]);
    }

    public void SaveDictionary()
    {
        FillDic();
        DicSaveData sav = new DicSaveData();
        foreach (var kvp in dic)
        {
            sav.keys.Add(kvp.Key);
            sav.values.Add(kvp.Value);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Dic_Save.sex");
        bf.Serialize(file, sav);
        file.Close();
        SyncFiles();
        print("Dictionary saving complete.");
    }

    public void LoadDictionary()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/Dic_Save.sex", FileMode.Open);
        DicSaveData sav = (DicSaveData)bf.Deserialize(file);
        file.Close();

        dic = new Dictionary<string, int>();
        for (int i = 0; i < sav.keys.Count; i++)
        {
            dic.Add(sav.keys[i], sav.values[i]);
        }

        print("Loading complete");
        ShowContent();
    }

    public void ShowContent()
    {
        if (dic != null && dic.Count > 0)
        {
            print("Dictionary content: \nSaveGame 0 - " + dic["SaveGame 0"] + ".\nSaveGame 1 - " + dic["SaveGame 1"]);
        }
        else
            print("Dictionary is empty.");
    }
}

[System.Serializable]
public class DicSaveData
{
    public List<string> keys = new List<string>();
    public List<int> values = new List<int>();
}
