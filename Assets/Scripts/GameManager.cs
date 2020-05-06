using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    [Header("Interface Objects")]
    public GameObject menuTop, menuBot;
    public GameObject turnRightTrigger, turnLeftTrigger;
    public GameObject menuContext;

    [Space]
    public GameObject roomStart;

    [Header("Exposed Vars for Debugging")]
    public GameObject player;
    public bool canInteract = false;
    public ObjectInteraction interactableObject;
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
        SetCurrentRoom();
    }

    // Update is called once per frame
    private void Update()
    {
        if (canInteract && Input.GetButton("Fire2"))
        {
            ShowContextMenu();
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

    void InitialisePlayerLocation()
    {
        player.transform.position = roomStart.transform.position;
    }

    void SetCurrentRoom()
    {
        roomCurrent = roomStart;
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
            turnLeftTrigger.SetActive(true);
            turnRightTrigger.SetActive(true);
        }
        else
        {
            player.GetComponent<PlayerMovement>().canTurn = false;
            turnLeftTrigger.SetActive(false);
            turnRightTrigger.SetActive(false);
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
                interactableObject.Inspect();
                break;
            case Interaction.Interact:
                interactableObject.Interact();
                break;
            case Interaction.PickUp:
                interactableObject.PickUp();
                break;
            default:
                print("No case in switch statement");
                break;
        }
    }
}

public enum Interaction {Inspect, Interact, PickUp};