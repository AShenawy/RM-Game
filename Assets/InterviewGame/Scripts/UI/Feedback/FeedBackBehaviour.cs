using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FeedBackBehaviour : MonoBehaviour
{
    private GameObject continueBtn;
    private GameObject prosBtn;
    private GameObject consBtn;
    private GameObject infoBtn;
    private GameObject prosContent;
    private GameObject consContent;
    private GameObject infoContent;

    void Start()
    {
        prosContent = GameObject.Find("Pros");
        consContent = GameObject.Find("Cons");
        infoContent = GameObject.Find("Info");
        prosBtn = GameObject.Find("ProsButton");
        consBtn = GameObject.Find("ConsButton");
        infoBtn = GameObject.Find("InfoButton");

        consContent.SetActive(false);
        infoContent.SetActive(false);

        continueBtn = GameObject.Find("ContinueButton");
        continueBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            SceneManager.LoadScene("MainMenu");
        });

        prosBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            prosContent.SetActive(true);
            consContent.SetActive(false);
            infoContent.SetActive(false);
            prosBtn.GetComponent<Image>().color = new Color32(170, 170, 170, 255);
            consBtn.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
            infoBtn.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
        });

        consBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            prosContent.SetActive(false);
            consContent.SetActive(true);
            infoContent.SetActive(false);
            prosBtn.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
            consBtn.GetComponent<Image>().color = new Color32(170, 170, 170, 255);
            infoBtn.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
        });

        infoBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            prosContent.SetActive(false);
            consContent.SetActive(false);
            infoContent.SetActive(true);
            prosBtn.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
            consBtn.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
            infoBtn.GetComponent<Image>().color = new Color32(170, 170, 170, 255);
        });
    }
}
