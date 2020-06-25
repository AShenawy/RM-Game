using Assets.Scripts.UI.ItemSelection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemSelectionBehaviour : MonoBehaviour
{
    public TextAsset jsonFile;
    public Items itemList;
    private GameObject continueBtn;

    void Start()
    {
        itemList = JsonUtility.FromJson<Items>(jsonFile.text);

        continueBtn = GameObject.Find("ContinueButton");
        continueBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            SceneManager.LoadScene("Interview");
        });

        foreach(Item item in itemList.items)
        {
            GameObject itemGameObject = GameObject.Find(item.itemName);
            itemGameObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate
            {
                if (itemGameObject.GetComponent<Toggle>().isOn)
                {
                    PlayerData.selectedItems.Add(item);
                }
                else
                {
                    PlayerData.selectedItems.Remove(item);
                }
            });
        }
    }
}
