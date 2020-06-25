using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    private GameObject playBtn;
    private GameObject aboutBtn;
    private GameObject backBtn;
    private GameObject about;
    private GameObject title;
    private GameObject logos;

    void Start()
    {
        PlayerData.ResetData();
        NPCData.ResetData();

        title = GameObject.Find("Title");
        logos = GameObject.Find("Logos");
        playBtn = GameObject.Find("PlayBtn");
        playBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            SceneManager.LoadScene("HowToPlay");
        });

        aboutBtn = GameObject.Find("AboutBtn");
        about = GameObject.Find("About");
        aboutBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            about.SetActive(true);
            playBtn.SetActive(false);
            aboutBtn.SetActive(false);
            title.SetActive(false);
            logos.SetActive(false);
        });

        backBtn = GameObject.Find("BackBtn");
        backBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            about.SetActive(false);
            playBtn.SetActive(true);
            aboutBtn.SetActive(true);
            title.SetActive(true);
            logos.SetActive(true);
        });
        about.SetActive(false);
    }
}
