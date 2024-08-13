using System.Collections;
using UnityEngine;


namespace Methodyca.Core
{
    // this script is for when a player is moving from a scene (Act) to another in the main game
    public class SceneTransfer : ObjectInteraction, ISaveable, ILoadable
    {
        [Header("Specific Script Parameters")]
        [SerializeField, Tooltip("Path or name of scene to be loaded")] private string destinationSceneName;
        [SerializeField, Tooltip("Name of starting room in loaded scene")] private string startingRoomName;
        [SerializeField] private bool canTransfer;
        
        [SerializeField, Multiline, Tooltip("In-game text to be displayed if Can Transfer is set to false.")]
        private string responseForDisabled;

        [SerializeField, Tooltip("True if the object has a pair in opposite dimension")]
        private bool isPaired = false;
        [SerializeField, Tooltip("The object the mirrors this one")]
        private SceneTransfer pairedTransferObject;


        protected override void Start()
        {
            base.Start();
            LoadState();
        }

        public override void InteractWithObject()
        {
            if (canTransfer)
            {
                // can remove interaction states since moving to a new scene
                SceneManagerScript.instance.GoToLevel(destinationSceneName, startingRoomName, false);
            }
            else
                DialogueHandler.instance.DisplayDialogue(responseForDisabled);
        }

        public void AllowTransfer(bool value)
        {
            canTransfer = value;
            
            // if object has a twin in opposite dimension then activate it as well
            if (isPaired)
                pairedTransferObject.ActivateFromPair(value);

            SaveState();
        }

        // this additional method is used for pair instead of AllowTransfer() to avoid recursive calls to itself
        public void ActivateFromPair(bool value)
        {
            canTransfer = value;
            SaveState();
        }

        public void SaveState()
        {
            SaveLoadManager.SetInteractableState(name + "_transfer", canTransfer ? 1 : 0);
        }

        public void LoadState()
        {
            if (SaveLoadManager.interactableStates.TryGetValue(name + "_transfer", out int transferState))
                canTransfer = transferState == 0 ? false : true;
        }
    }
}