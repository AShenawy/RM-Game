using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExplinationBehaviour : MonoBehaviour
{
    public GameObject continueBtn;

    void Start()
    {

        continueBtn = GameObject.Find("continueBtn");
        continueBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            SceneManager.LoadScene("ItemSelection");
        });
    }
}