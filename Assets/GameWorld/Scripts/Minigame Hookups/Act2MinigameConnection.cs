#define TESTING
using UnityEngine;
using Methodyca.Core;


// this script handles minigames launched in Act2
[RequireComponent(typeof(MinigameInteraction))]
public class Act2MinigameConnection : MinigameHub, ISaveable, ILoadable
{
    [Header("Specific Script Parameters")]
    public Act2ProgressController progressController;
    public Minigames minigameID;
    public bool isRewardGiven;      // TODO make it private after debugging
    [Tooltip("If the minigame is accessible from both QL/QN nodes, then place the object from opposite node")]
    public Act2MinigameConnection linkedMinigameAccess;
    //private static bool[] isRewardGivenStatic = new bool[System.Enum.GetValues(typeof(Minigames)).Length];


    public override void Start()
    {
#if TESTING
        //print("Static bool size is: " + isRewardGivenStatic.Length);
        //for (int i = 0; i < isRewardGivenStatic.Length; i++)
        //    print($"Static bool stat for minigame {i} is {isRewardGivenStatic[i]}");
#endif
        base.Start();
        LoadState();

        if (isCompleted)
        {
            EndGame();

            // if there are 2 of the minigame accesses in QN/QL, then mirror access
            // should be ended to avoid giving 2 rewards per minigame.
            linkedMinigameAccess?.EndLinkedAccess();
        }
        else
            CheckGameWon();
    }

    public override void EndGame()
    {
        base.EndGame();

        if (!isRewardGiven)
        {
            progressController.GiveMinigameReward(minigameID);
            isRewardGiven = true;
            //isRewardGivenStatic[(int)minigameID] = true;
        }

        SaveState();
    }

//#if TESTING
//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.A))
//            print($"Static bool stat for minigame {minigameID} is {isRewardGivenStatic[(int)minigameID]}");
//    }
//#endif

    void CheckGameWon()
    {
        if (SceneManagerScript.instance.minigamesWon.Contains((int)minigameID))
        {
            EndGame();

            // if there are 2 of the minigame accesses in QN/QL, then mirror access
            // should be ended to avoid giving 2 rewards per minigame.
            linkedMinigameAccess?.EndLinkedAccess();
        }
    }

    public void EndLinkedAccess()
    {
        isCompleted = true;
        isRewardGiven = true;
    }

    public void SaveState()
    {
        SaveLoadManager.SetInteractableState(name + "_completed", isCompleted ? 1 : 0);
        SaveLoadManager.SetInteractableState(name + "_rewardGiven", isRewardGiven ? 1 : 0);
    }

    public void LoadState()
    {
        if (SaveLoadManager.interactableStates.TryGetValue(name + "_completed", out int completedState))
            isCompleted = (completedState == 1) ? true : false;
        if (SaveLoadManager.interactableStates.TryGetValue(name + "_rewardGiven", out int rewardState))
            isRewardGiven = (rewardState == 1) ? true : false;
    }
}