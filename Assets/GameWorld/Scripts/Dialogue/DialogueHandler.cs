using System;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Core
{
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
    
        [SerializeField] private Text textDisplay;
        [SerializeField] private GameObject dialoguePanel;

        private string[] dialoguePieces;
        private int progressionIndex = 0;

        private void Start()
        {
            dialoguePanel.SetActive(false);
        }

        public void AdvanceDialogue()
        {
            // moves to the next block of dialogue
    
            // check which section of the dialogue array it's at
            if (progressionIndex < dialoguePieces.Length)
            {
                textDisplay.text = dialoguePieces[progressionIndex];
                progressionIndex++;
            }
            else
                EndDialogue();
        }
    
        public void DisplayDialogue(string[] inDialogues)
        {
            dialoguePieces = inDialogues;   // store incoming dialogue array
            
            // Display the dialogue box and text
    
            dialoguePanel.SetActive(true);  // display the dialogue panel
            AdvanceDialogue();      // start going through the dialogue blocks
        }

        public void DisplayDialogue(string inDialogue)
        {

            // Display the dialogue box and text
            if (inDialogue != "")
            {
                progressionIndex = 0;
                dialoguePanel.SetActive(true);  // display the dialogue panel
                dialoguePieces = new[] { inDialogue };
                AdvanceDialogue();
            }
        }
    
        void EndDialogue()
        {
            // ends the dialogue and hides the dialogue box
            progressionIndex = 0;   // reset the index for the next dialogue interaction
            Debug.Log("Dialogue has ended. Hiding Dialogue box");
            dialoguePanel.SetActive(false);
        }
    }
}