using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using Cursor = UnityEngine.Cursor;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    [Header("Interface Objects")]
    [SerializeField] private GameObject menuTop;
    [SerializeField] private GameObject menuBot;
    [SerializeField] private GameObject triggerTurnRight, triggerTurnLeft;
    [SerializeField] private GameObject menuContext;
    [Header("Cursor Styles")]
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Texture2D cursorInteract;
    [SerializeField] private Texture2D cursorPointRight;
    [SerializeField] private Texture2D cursorPointLeft;


    [Space]
    [SerializeField]private GameObject roomStart;

    [Header("Exposed Vars for Debugging")]
    public GameObject player;

    public Vector2 mousePos;
    public bool canInteract = false;
    public ObjectInteraction interactableObject;
    public ObjectInteraction tempInteractObjectReference;
    public GameObject roomCurrent, roomTarget;
    public List<GameObject> rooms;

    private void Awake()
    {
        gm = GetComponent<GameManager>();
        rooms.AddRange(GameObject.FindGameObjectsWithTag("Room"));
        foreach(GameObject room in rooms)
        {
            room.SetActive(false);
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        HideMenus();
        roomStart.SetActive(true);
        InitialisePlayerLocation();
        
        // Set current room
        roomCurrent = roomStart;;
    }

    // Update is called once per frame
    private void Update()
    {
        if (canInteract)
        {
            SetCursor(cursorInteract);

            if (Input.GetButtonUp("Fire1"))
            {
                if(EventSystem.current.IsPointerOverGameObject()) // Make click not work on world objects if mouse over GUI
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
            //SetCursor(cursorDefault);
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
    
    public void SetCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero,CursorMode.Auto);
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
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        print("Entered " + roomCurrent.name);
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
                Debug.Log("No case in switch statement");
                break;
        }
    }

    public void ClearTempInteractableReference()
    {
        tempInteractObjectReference = null;
    }
}

public enum Interaction {Inspect, Interact, PickUp};