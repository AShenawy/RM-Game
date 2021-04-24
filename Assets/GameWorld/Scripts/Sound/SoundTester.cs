using UnityEngine;
using Methodyca.Core;
using UnityEngine.UI;

public class SoundTester : MonoBehaviour
{
    public GameObject menu;
    public GameObject cluster;

    public GameObject messageBoard;
    [SerializeField]private Text groupone;
    [SerializeField]private Text grouptwo;
    [SerializeField]private Text groupthree;
    private string grouptext;
    private string note;

    // Use this for initialization
    void Start()
    {
        menu.SetActive(false);
        messageBoard.SetActive(false);
        grouptext = " ";
    }

    // Update is called once per frame
    void Update()
    {
        grouptext = note;
        
    }

    public void Groupone(string word)
    {
        messageBoard.SetActive(true);
        word = note;
        note = groupone.text;
        
    }

    public void Grouptwo(string word)
    {
        messageBoard.SetActive(true);
        word = note;
        note = grouptwo.text;
    }

    public void Groupthree(string word)
    {
        messageBoard.SetActive(true);
        word = note;
        note = groupthree.text;
    }
}
