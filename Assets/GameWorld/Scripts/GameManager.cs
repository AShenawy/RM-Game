using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Core
{
    // This class handles the games main information and player details
    public class GameManager : MonoBehaviour
    {
        // make this a singleton class
        public static GameManager instance;
    
        [Header("World Objects")]
        public GameObject player;
    
        [SerializeField] private GameObject[] rooms;    // array of rooms in the scenes
        [SerializeField] private GameObject roomStart;  // ref to the starting room in the scene
    
        [Header("UI Objects")]
        [SerializeField] private GameObject inventoryPanel; // ref to inventory UI panel
        [SerializeField] private GameObject dialoguePanel;  // ref to dialogue UI panel
        [SerializeField] private GameObject triggerTurnRight, triggerTurnLeft;  // ref to right/left UI panels
    
        [Header("Script Interaction Vars")]
        public bool canInteract = false;
        public ObjectInteraction interactableObject;
        private ObjectInteraction tempInteractObjectReference;
        
        private GameObject roomCurrent, roomTarget;
        private RoomData roomData;

        
        [HideInInspector] public bool isPlayerHoldingItem = false;
    
        private void Awake()
        {
            // Check if not existing and assign as singleton
            if (instance == null)
                instance = this;
        }

        private void Start()
        {
            #region RoomSetup
            roomCurrent = roomStart;    // Set current room
            HideAllRooms();     // Make sure all rooms are inactive at play start
            GoToRoom(roomStart);    // Initialise first room in scene
            #endregion

            // Subscribe to context menu event
            CursorManager.instance.contextMenuDisabled += OnContextMenuDisabled;

            HideGUI();  // Ensure all menus are hidden. KEEP below RoomSetup region
        }
    
        // Update is called once per frame
        private void Update()
        {
            Scan();

            // Check mouse input
            if (canInteract)
            {
                // Make click not work on world objects if mouse is over GUI
                if (EventSystem.current.IsPointerOverGameObject())
                    return;

                if (Input.GetButtonUp("Fire1"))     // ref to LMB
                    DoMainAction();
                
                if (!isPlayerHoldingItem && Input.GetButtonDown("Fire2"))      // ref to 
                    DoSecondaryAction();
            }
            else
            {
                // Commented out because when active the cursor is always stuck in default texture
                //SetCursor(cursorDefault);
            }
        }

        private void Scan()
        {
            // cast ray at cursor location onto game world
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // check if cursor hit an interactable object
            if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.GetComponent<ObjectInteraction>())
                {
                    interactableObject = hit.collider.GetComponent<ObjectInteraction>();
                    canInteract = interactableObject.canInteract;
                    
                    if(!isPlayerHoldingItem && canInteract)    // change to interaction cursor if no item is held
                        CursorManager.instance.SetCursor(CursorTypes.Interact, null);

                    print("Found something!");
                }
                else
                {
                    interactableObject = null;
                    canInteract = false;

                    if(!isPlayerHoldingItem)    // change to default cursor if no item is held
                        CursorManager.instance.SetDefaultCursor();

                    print("Nothing here!");
                }
            }
        }

        private void DoMainAction()
        {
            if (!isPlayerHoldingItem)   // Interact with object if player is not holding item
                interactableObject.InteractWithObject();
            else
            {
                interactableObject.UseHeldItem(player.GetComponent<PlayerItemHandler>().heldItem);   // Do not interact and use the held item
                player.GetComponent<PlayerItemHandler>().RemoveFromHand();
            }
        }

        private void DoSecondaryAction()
        {
            tempInteractObjectReference = interactableObject;
            CursorManager.instance.ShowContextMenu();
        }

        private void CheckPlayerHoldingItem(Item heldItem)
        {
            isPlayerHoldingItem = heldItem ? true : false;
        }

        private void HideAllRooms()
        {
            for (var i = 0; i < rooms.Length; i++)
            {
                rooms[i].SetActive(false);
            }
        }
    
        private void HideGUI()
        {
            inventoryPanel.SetActive(false);
            dialoguePanel.SetActive(false);
            CursorManager.instance.HideContextMenu();
        }
    
        public void GoToRoom(GameObject destination)
        {
            // Set and activate the destination room
            roomTarget = destination;
            roomTarget.SetActive(true);
    
            // Check details about destination room
            CheckRoom(roomTarget);
            
            // Move player to target room
            player.transform.position = roomTarget.transform.position;

            // Deactivate previous room player was in and update current room info (player is now inside)
            // check and only disable previous room if destination room is different (not the one player is already in)
            if ((roomCurrent != null) && (roomCurrent != roomTarget))
            {
                roomCurrent.SetActive(false);

                // set destination room as new current room
                roomCurrent = roomTarget;
            }
            else if ((roomCurrent != null) && (roomCurrent == roomTarget))
            {
                Debug.LogWarning("Warning: Player re-entered the same room.");
            }
            else
            {
                Debug.LogWarning("Warning: Entering new room while previous room value not set/found.");
            }

            // clear target room value
            roomTarget = null;

            ResetCursor();

            // ---- for debugging ----
            print("Entered " + roomCurrent.name);
        }

        private void CheckRoom(GameObject room)
        {
            // Get room data
            roomData = room.GetComponent<RoomData>();

            // Check if room allows player to turn or not
            if (roomData.playerCanTurn)
            {
                PlayerMovement movement = player.GetComponent<PlayerMovement>();
                movement.canTurn = true;
                movement.turnAngle = roomData.allowedTurnAngle;

                SetTurnTriggersActive(true);
            }
            else
            {
                player.GetComponent<PlayerMovement>().canTurn = false;

                SetTurnTriggersActive(false);
            }

            // Check if entered room is a save checkpoint
            if (roomData.isCheckpoint)
            {
                SaveGame();
            }
        }
    
        private void ResetCursor()
        {
            // Reset cursor to default image
            CursorManager.instance.SetDefaultCursor();
        }

        private void OnContextMenuDisabled(bool isDisabled)
        {
            // check if context menu is NOT disabled, then disable player turning
            if(!isDisabled)
            {
                SetTurnTriggersActive(false);
            }
            // if context menu IS disabled and room allows turning, let player turn
            else if(roomData.playerCanTurn)
            {
                SetTurnTriggersActive(true);
            }
        }

        // Enable or disable UI triggers for player turn using mouse
        public void SetTurnTriggersActive(bool value)
        {
            triggerTurnLeft.SetActive(value);
            triggerTurnRight.SetActive(value);
        }

        public void SaveGame()
        {
            //TODO Save the game
            print("Save the current game progress");
        }

        public void InteractWithObject(Interaction interactionType)
        {
            switch (interactionType)
            {
                case Interaction.Inspect:
                    print(tempInteractObjectReference.InspectObject());
                    break;

                case Interaction.Interact:
                    tempInteractObjectReference.InteractWithObject();
                    break;

                case Interaction.PickUp:
                    tempInteractObjectReference.PickUpObject();
                    break;

                default:
                    Debug.LogWarning("No case in switch statement");
                    break;
            }
        }
    }
    
    public enum Interaction { Inspect, Interact, PickUp };
}