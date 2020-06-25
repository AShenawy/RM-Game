using UnityEngine;
using Assets.Scripts.UI.Feedback.FeedbackInfo;
using UnityEngine.UI;
using System.Linq;

public class ListInfo : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject item;
    [SerializeField]
    private RectTransform content;
    private int count = 0;
    private FeedbackData feedbackData;

    public TextAsset feedbackDataJsonFile;

    void Start()
    {
        feedbackData = JsonUtility.FromJson<FeedbackData>(feedbackDataJsonFile.text);
        content.sizeDelta = new Vector2(0, feedbackData.info.Length * 65);
        foreach (Info info in PlayerData.info)
        {
            float spawnY = count * 65;
            Vector3 pos = new Vector3(0, -spawnY, 0);
            GameObject spawnedItem = Instantiate(item, pos, spawnPoint.rotation);
            spawnedItem.transform.SetParent(spawnPoint, false);
            InfoItemDetails itemDetails = spawnedItem.GetComponent<InfoItemDetails>();
            itemDetails.itemInfo.text = info.info;
            count++;
        }
        foreach (Info info in feedbackData.info)
        {
            Info infoFound = PlayerData.info.SingleOrDefault(i => i.info == info.info);
            if (infoFound == null)
            {
                float spawnY = count * 65;
                Vector3 pos = new Vector3(0, -spawnY, 0);
                GameObject spawnedItem = Instantiate(item, pos, spawnPoint.rotation);
                spawnedItem.transform.SetParent(spawnPoint, false);
                spawnedItem.GetComponent<Image>().color = new Color32(220, 220, 220, 255);
                InfoItemDetails itemDetails = spawnedItem.GetComponent<InfoItemDetails>();
                itemDetails.itemInfo.text = info.info;
                count++;
            }
        }
    }
}
