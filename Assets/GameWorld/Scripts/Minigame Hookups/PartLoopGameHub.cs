using UnityEngine;
using Methodyca.Core;


// this script handles collaborative design / participatory loop minigame hub
[RequireComponent(typeof(MinigameInteraction))]
public class PartLoopGameHub : MinigameHub, ISaveable, ILoadable
{
    [Header("Specific Script Parameters")]
    public Minigames minigameID;
    public bool isRewardGiven;

    public override void Start()
    {
        base.Start();
        LoadState();

        if (isCompleted)
            EndGame();
        else
            CheckGameWon();
    }

    public override void EndGame()
    {
        base.EndGame();

        if (!isRewardGiven)
        {
            //TODO give reward
            isRewardGiven = true;
        }

        SaveState();
    }

    void CheckGameWon()
    {
        if (SceneManagerScript.instance.minigamesWon.Contains((int)minigameID))
            EndGame();
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