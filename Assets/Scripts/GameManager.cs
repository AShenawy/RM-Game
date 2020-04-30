using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public GameObject menuTop;
    public GameObject menuBot;
    public GameObject menuContext;
    public GameObject roomStart;

    [Header("Exposed Vars for Debugging")]
    public GameObject roomCurrent;
    public GameObject roomTarget;
    public List<GameObject> rooms;

    private void Awake()
    {
        gm = GetComponent<GameManager>();
        rooms.AddRange(GameObject.FindGameObjectsWithTag("Room"));
        foreach(GameObject room in rooms)
        {
            room.SetActive(false);
        }
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        HideMenus();
        roomStart.SetActive(true);
        SetPlayerLocation();
        SetCurrentRoom();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            ShowContextMenu();
    }

    void HideMenus()
    {
        menuTop.SetActive(false);
        menuBot.SetActive(false);
        menuContext.SetActive(false);
    }

    void ShowContextMenu()
    {
        menuContext.SetActive(true);
        menuContext.transform.position = Input.mousePosition;
    }

    void SetPlayerLocation()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
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

        // Find the player and move them to the destination
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = roomTarget.transform.position;

        // Deactivate the previous room the player was in and set the destination as the new current room
        if (roomCurrent != null)
        {
            roomCurrent.SetActive(false);
            roomCurrent = roomTarget;
        }

        roomTarget = null;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        print("Entered " + roomCurrent.name);
    }
}
