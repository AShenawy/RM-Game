using UnityEngine;
using UnityEngine.UI;

public class BadgeBehaviour : MonoBehaviour
{
    private Text badgeText;

    // Start is called before the first frame update
    void Start()
    {
        badgeText = GetComponentInChildren<Text>();
    }

    public void UpdateBadgeInfo()
    {

    }
}
