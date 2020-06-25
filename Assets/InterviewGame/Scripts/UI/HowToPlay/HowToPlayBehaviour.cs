using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HowToPlayBehaviour : MonoBehaviour
{
    private GameObject continueToNameInputBtn;
    private GameObject continueBtn;
    private GameObject playerNameInput;
    private GameObject nameInputField;

    private string playerName;
    void Start()
    {
        continueToNameInputBtn = GameObject.Find("ContinueToNameInputButton");
        continueBtn = GameObject.Find("ContinueButton");
        playerNameInput = GameObject.Find("PlayerNameInput");
        nameInputField = GameObject.Find("NameInputField");
        playerNameInput.SetActive(false);
        continueBtn.GetComponent<Button>().interactable = false;

        continueToNameInputBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            playerNameInput.SetActive(true);
            continueToNameInputBtn.SetActive(false);
        });
        continueBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            PlayerData.playerName = playerName;
            SceneManager.LoadScene("Explination");
        });
    }

    void Update()
    {
        playerName = nameInputField.GetComponent<InputField>().text;
        if (playerName.Replace(" ", "").Length > 0)
        {
            continueBtn.GetComponent<Button>().interactable = true;
        } 
        else
        {
            continueBtn.GetComponent<Button>().interactable = false;
        }
    }
}
