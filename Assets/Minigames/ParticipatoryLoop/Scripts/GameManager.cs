using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Screens")]
    public GameObject startScreen;
    public GameObject introScreen;
    public GameObject designBoardScreen;
    public GameObject meetingScreen;
    public GameObject recapScreen;
    public GameObject endScreen;

    [Header("Behaviour Control")]
    public BudgetCounter budgeter;
    public DesignBoardBehaviour designBoardBehaviour;
    public Text clockHours;
    public Text clockMins;

    [Header("Game Turns")]
    //public Action newTurnStarted;
    //public int gameTurns;
    public int currentTurn = -1;

    private GameObject currentScreen;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        GoToStart();
    }

    public void GoToStart()
    {
        startScreen.SetActive(true);
        introScreen.SetActive(false);
        designBoardScreen.SetActive(false);
        meetingScreen.SetActive(false);
        recapScreen.SetActive(false);
        endScreen.SetActive(false);

        currentScreen = startScreen;

        designBoardBehaviour.ResetToggles();
        budgeter.ResetCounter();
        currentTurn = -1;
    }

    public void GoToIntro()
    {
        currentTurn++;

        startScreen.SetActive(false);
        introScreen.SetActive(true);
        designBoardScreen.SetActive(false);
        meetingScreen.SetActive(false);
        recapScreen.SetActive(false);
        endScreen.SetActive(false);

        currentScreen = introScreen;
        UpdateClock();
    }

    public void GoToDesignBoard()
    {


        startScreen.SetActive(false);
        introScreen.SetActive(false);
        designBoardScreen.SetActive(true);
        meetingScreen.SetActive(false);
        recapScreen.SetActive(false);
        endScreen.SetActive(false);

        currentScreen = designBoardScreen;
        UpdateClock();
    }

    public void GoToMeeting()
    {
        startScreen.SetActive(false);
        introScreen.SetActive(false);
        designBoardScreen.SetActive(false);
        meetingScreen.SetActive(true);
        recapScreen.SetActive(false);
        endScreen.SetActive(false);

        currentScreen = meetingScreen;
        UpdateClock();
    }

    public void GoToRecap()
    {
        startScreen.SetActive(false);
        introScreen.SetActive(false);
        designBoardScreen.SetActive(false);
        meetingScreen.SetActive(false);
        recapScreen.SetActive(true);
        endScreen.SetActive(false);

        currentScreen = recapScreen;
        UpdateClock();
    }

    public void GoToEnd()
    {
        startScreen.SetActive(false);
        introScreen.SetActive(false);
        designBoardScreen.SetActive(false);
        meetingScreen.SetActive(false);
        recapScreen.SetActive(false);
        endScreen.SetActive(true);

        currentScreen = endScreen;
    }

    void UpdateClock()
    {
        if (currentScreen == introScreen)
        {
            clockHours.text = "08";
            clockMins.text = Random.Range(34, 42).ToString();
        }
        else if (currentScreen == designBoardScreen)
        {
            clockHours.text = "10";
            clockMins.text = Random.Range(19, 26).ToString();
        }
        else if (currentScreen == meetingScreen)
        {
            clockHours.text = "14";
            clockMins.text = Random.Range(1, 4).ToString("D2");
        }
        else if (currentScreen == recapScreen)
        {
            clockHours.text = "17";
            clockMins.text = Random.Range(43, 57).ToString();
        }
    }
}
