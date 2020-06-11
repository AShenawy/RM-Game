using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{
    // make game manager a singleton
    public static GameManager instance;
    
    [Header("World Objects")]
    public GameObject player;
    
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private GameObject roomStart;

    [Header("Interface Objects")]
    [SerializeField] private GameObject menuTop;
    [SerializeField] private GameObject menuBot;
    [SerializeField] private GameObject triggerTurnRight, triggerTurnLeft;
    [SerializeField] private GameObject menuContext;

    [Header("Exposed Vars for Debugging")]
    public Vector2 mousePos;
    public bool canInteract = false;
    public ObjectInteraction interactableObject;
    public ObjectInteraction tempInteractObjectReference;
    public GameObject roomCurrent, roomTarget;

    private void Awake()
    {
        // Check if not existing and assign as singleton
        if(instance == null)
            instance = this;   
    }
    private void Start()
    {
        HideMenus();        // Ensure all menus are hidden
        
        HideAllRooms();     // Make sure all rooms are inactive at play start
        roomStart.SetActive(true);      // Make only the starting room where player is the active one
        roomCurrent = roomStart;        // Set current room
        
        InitialisePlayerLocation();     // Make sure the player starts inside the starting room
    }

    // Update is called once per frame
    private void Update()
    {
        if (canInteract)
        {
            CursorManager.instance.SetCursor(CursorTypes.Interact);

            if (Input.GetButtonUp("Fire1"))
            {
                // Make click not work on world objects if mouse is over GUI
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                
                interactableObject.InteractWithObject();
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                if(EventSystem.current.IsPointerOverGameObject())
                    return;
                
                tempInteractObjectReference = interactableObject;
                ShowContextMenu();
            }
        }
        else
        {
            // this is commented out because when active the cursor is always stuck in default texture
            //SetCursor(cursorDefault);
        }
    }

    private void HideAllRooms()
    {
        for(int i = 0; i < rooms.Length; i++)
        {
            rooms[i].SetActive(false);
        }
    }

    private void HideMenus()
    {
        menuTop.SetActive(false);
        menuBot.SetActive(false);
        menuContext.SetActive(false);
    }

    private void ShowContextMenu()
    {
        menuContext.SetActive(true);
        menuContext.transform.position = Input.mousePosition;
    }

    private void InitialisePlayerLocation()
    {
        player.transform.position = roomStart.transform.position;
    }

    public void GoToRoom(GameObject destination)
    {
        // Set and activate the destination room
        roomTarget = destination;
        roomTarget.SetActive(true);

        player.transform.position = roomTarget.transform.position;
        RoomData targetRoomData = roomTarget.GetComponent<RoomData>();
        if (targetRoomData.playerCanTurn)
        {
            player.GetComponent<PlayerMovement>().turnAngle = targetRoomData.allowedTurnAngle;
            triggerTurnLeft.SetActive(true);
            triggerTurnRight.SetActive(true);
        }
        else
        {
            player.GetComponent<PlayerMovement>().canTurn = false;
            triggerTurnLeft.SetActive(false);
            triggerTurnRight.SetActive(false);
        }

        // Deactivate the previous room the player was in and set the destination as the new current room
        if (roomCurrent != null)
        {
            roomCurrent.SetActive(false);
            roomCurrent = roomTarget;
        }
        roomTarget = null;
        
        // Return cursor to default image after clicking door
        CursorManager.instance.SetCursor(CursorTypes.Default);
        
        print("Entered " + roomCurrent.name); // for debugging
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

public enum Interaction {Inspect, Interact, PickUp};