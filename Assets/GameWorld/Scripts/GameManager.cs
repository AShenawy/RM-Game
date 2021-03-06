//#define TESTING
using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Core
{
    // This class handles the games main information and player details
    public sealed class GameManager : MonoBehaviour, ISaveable, ILoadable
    {
        #region Singleton
        public static GameManager instance;
        private void Awake()
        {
            // Check if not existing and assign as singleton
            if (instance == null)
                instance = this;
        }
        #endregion

        [Header("World Objects")]
        public GameObject player;
    
        [SerializeField] private GameObject[] rooms;    // array of rooms in the scenes
        [Space, SerializeField] private GameObject roomStart;  // ref to the starting room in the scene
    
        [Header("UI Objects")]
        [SerializeField] private GameObject inventoryPanel; // ref to inventory UI panel
        [SerializeField] private GameObject triggerTurnRight, triggerTurnLeft;  // ref to right/left UI panels
    
        [Header("Script Interaction Vars")]
        public bool canInteract = false;
        public ObjectInteraction interactableObject;
        private ObjectInteraction tempInteractObjectReference;
        
        private GameObject roomCurrent, roomTarget;
        private RoomData roomData;

        [HideInInspector] public bool isPlayerHoldingItem = false;
        [HideInInspector] public bool usedDimeSwitch { get; private set; }  // for NPC check to coach player on dimension switching

        #region Test Code Vars
        // typing in this string makes the player able to turn in current room (if originally disabled)
        private string[] cntrn = new string[] { "c", "n", "t", "r", "n" };
        private int cntrnIndex = 0;
        #endregion


        // When loading a new scene with GameManager in it, it should first check 
        private void OnEnable()
        {
            roomStart = SceneManagerScript.instance ? SceneManagerScript.instance.GetSceneStartingRoom() ?? roomStart : roomStart;
        }

        private void Start()
        {
            #region RoomSetup
            roomCurrent = roomStart;    // Set current room
            HideAllRooms();     // Make sure all rooms are inactive at play start
            GoToRoom(roomStart);    // Place player in first room in scene
            #endregion

            // Subscribe to context menu event
            CursorManager.instance.contextMenuDisabled += OnContextMenuDisabled;

            // KEEP BELOW RoomSetup region. Ensures all menus are hidden on start
            HideGUI();
            LoadState();
        }
    
        // Update is called once per frame
        private void Update()
        {
            Scan();

            // Check mouse input
            if (canInteract)
            {
                // Make click not work on world objects if mouse is over GUI
                if (IsCursorOnGUI())
                    return;

                if (Input.GetButtonUp("Fire1"))     // ref to LMB
                    DoMainAction();
                
                if (!isPlayerHoldingItem && Input.GetButtonDown("Fire2"))      // ref to RMB
                    DoSecondaryAction();
            }
            else
            {
                // Commented out because when active the cursor is always stuck in default texture
                //SetCursor(cursorDefault);
            }

            // ========== TEST CODE INPUT ==========
#if TESTING
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(cntrn[cntrnIndex]))
                    cntrnIndex++;
                else
                    cntrnIndex = 0;
            }

            if (cntrnIndex == cntrn.Length)
            {
                roomData.playerCanTurn = !roomData.playerCanTurn;
                CheckRoom(roomCurrent);
                cntrnIndex = 0;
            }
#endif
        }

        private void Scan()
        {
            // Check for mouse on GUI to not change cursor while GUI object is on screen
            if (IsCursorOnGUI())
                return;

            // cast ray at cursor location onto game world
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // check if cursor hit an interactable object at range of 20 units (rooms' farthest object is 15 units)
            if (Physics.Raycast(mouseRay, out RaycastHit hit, 20f))
            {
                if (hit.collider.GetComponent<ObjectInteraction>())
                {
                    interactableObject = hit.collider.GetComponent<ObjectInteraction>();
                    canInteract = interactableObject.canInteract;   // player can only interact if the object allows it
                    
                    if(!isPlayerHoldingItem && canInteract)    // change to interaction cursor if no item is held
                        CursorManager.instance.SetCursor(CursorTypes.Interact, null);
                }
                else
                {
                    interactableObject = null;
                    canInteract = false;

                    if(!isPlayerHoldingItem)    // change to default cursor if no item is held
                        CursorManager.instance.SetDefaultCursor();
                }
            }
        }

        private bool IsCursorOnGUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return true;
            else
                return false;
        }

        private void DoMainAction()
        {
            if (!isPlayerHoldingItem)   // Interact with object if player is not holding item
            {
                interactableObject.InteractWithObject();
                CursorManager.instance.SetDefaultCursor();
            }
            else
            {
                // Do not interact and use the held item
                interactableObject.UseWithHeldItem(player.GetComponent<PlayerItemHandler>().heldItem);   
                player.GetComponent<PlayerItemHandler>().RemoveFromHand();
                CursorManager.instance.SetDefaultCursor();      // return to default cursor right after using item
            }
        }

        private void DoSecondaryAction()
        {
            tempInteractObjectReference = interactableObject;
            CursorManager.instance.ShowContextMenu();
            CursorManager.instance.SetDefaultCursor();
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

            // Update the Save Manager
            SaveLoadManager.SetCurrentRoom(roomData.name);

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

            if (roomData.ForcePlayerOrientation)
                RotatePlayer();

            // Check if entered room is an autosave checkpoint
            if (roomData.isSavePoint)
                SaveLoadManager.SaveGameAuto();

            void RotatePlayer()
            {
                switch (roomData.orientation)
                {
                    case RoomData.Orientations.North:
                        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        break;

                    case RoomData.Orientations.East:
                        player.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                        break;

                    case RoomData.Orientations.South:
                        player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        break;

                    case RoomData.Orientations.West:
                        player.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                        break;
                }
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

        public void SetStartingRoom(GameObject room)
        {
            roomStart = room;
        }

        public void InteractWithObject(Interaction interactionType)
        {
            switch (interactionType)
            {
                case Interaction.Inspect:
                    tempInteractObjectReference.InspectObject();
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

        public string GetCurrentRoomTag()
        {
            return roomCurrent.tag;
        }

        public void SwitchDimension()
        {
            // check if player can actually switch
            if (!player.GetComponent<PlayerMovement>().canDimeSwitch)
            {
                DialogueHandler.instance.DisplayDialogue("I can't switch dimensions right now.");
                return;
            }
            
            usedDimeSwitch = true;
            SaveState();

            // find the corresponding room in the rooms list according to active state and location and teleport to it
            foreach (GameObject room in rooms)
            {
                if (room.activeSelf == false && room.transform.position.z == roomCurrent.transform.position.z)
                {
                    GoToRoom(room);
                    break;
                }
            }

            // Swap the UI image for dime switcher on successful switch
            inventoryPanel.GetComponentInChildren<SwapImageUI>(true).SwapImage();
        }

        public void SaveState()
        {
            SaveLoadManager.SetInteractableState($"{name}_usedDimeSwitch", usedDimeSwitch ? 1 : 0);
        }

        public void LoadState()
        {
            if (SaveLoadManager.interactableStates.TryGetValue($"{name}_usedDimeSwitch", out int usedSwitchState))
                usedDimeSwitch = (usedSwitchState == 1) ? true : false;
        }
    }
    
    public enum Interaction { Inspect, Interact, PickUp };
}