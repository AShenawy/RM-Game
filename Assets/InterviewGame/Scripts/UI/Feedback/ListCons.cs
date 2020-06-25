using Assets.Scripts.UI.Feedback.FeedbackInfo;
using UnityEngine;

public class ListCons : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject item;
    [SerializeField]
    private RectTransform content;
    private int count = 0;
    void Start()
    {
        content.sizeDelta = new Vector2(0, PlayerData.cons.Count * 145);
        foreach (InfoCon con in PlayerData.cons)
        {
            float spawnY = count * 145;
            Vector3 pos = new Vector3(0, -spawnY, 0);
            GameObject spawnedItem = Instantiate(item, pos, spawnPoint.rotation);
            spawnedItem.transform.SetParent(spawnPoint, false);
            ProAndConDetails itemDetails = spawnedItem.GetComponent<ProAndConDetails>();
            itemDetails.itemInfo.text = con.info;
            itemDetails.itemComment.text = con.comment;
            count++;
        }
        count = 0;
    }
}
