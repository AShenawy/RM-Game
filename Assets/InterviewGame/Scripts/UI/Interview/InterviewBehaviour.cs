using Assets.Scripts.UI.ItemSelection;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterviewBehaviour : MonoBehaviour
{
    private GameObject backBtn;
    public TextAsset jsonFile;
    public Items itemList;
    void Start()
    {
        itemList = JsonUtility.FromJson<Items>(jsonFile.text);
        backBtn = GameObject.Find("BackBtn");
        backBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            SceneManager.LoadScene("MainMenu");
        });

        foreach (Item item in itemList.items)
        {
            GameObject itemGameObject = GameObject.Find(item.itemName);
            itemGameObject.SetActive(false);
            Item selectedItem = PlayerData.selectedItems.SingleOrDefault(i => i.itemName == item.itemName);
            if (selectedItem != null)
            {
                itemGameObject.SetActive(true);
            }
        }
    }
}
