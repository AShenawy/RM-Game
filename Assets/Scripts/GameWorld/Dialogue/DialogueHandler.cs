using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script handles the dialogue display in the UI
public class DialogueHandler : MonoBehaviour
{
    // make this class a singleton
    #region Singleton
    public static DialogueHandler instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion  

    private Text textDisplay;

    private void Start()
    {
        textDisplay = GetComponent<Text>();
    }

    public void AdvanceDialogue()
    {
        // moves to the next block of dialogue
    }

    public void DisplayDialogue(string[] inDialogues)
    {
        // Display the dialogue box and text
        print("Text dialogue");
    }

    void EndDialogue()
    {
        // ends the dialogue and hides the dialogue box
    }
}
