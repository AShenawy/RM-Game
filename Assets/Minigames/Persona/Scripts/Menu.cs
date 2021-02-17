using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject playerA;
    public GameObject playerB;
    public Button play;

    static string selectedPlayer = "";

    // Start is called before the first frame update
    void Start()
    {
        if(selectedPlayer != "")
            SelectCharacter(selectedPlayer);
        DontDestroyOnLoad(transform.gameObject);
    }

    public void SelectCharacter(string character)
    {
        selectedPlayer = character;
        if (character == "PlayerA")
        {
            playerA.GetComponent<Image>().color = Color.white;
            playerB.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
        } else
        {
            playerA.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
            playerB.GetComponent<Image>().color = Color.white;
        }
        play.interactable = true;
    }

}
