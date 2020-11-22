using UnityEngine;
using Methodyca.Core;

// this script handles the sorting minigame hub
public class SortingGameHub : MinigameHub, ISaveable, ILoadable
{
    [Header("Specific Script Parameters")]
    [SerializeField] private GameObject unchargedCrystalQLPrefab;
    [SerializeField] private GameObject unchargedCrystalQNPrefab;

    [Space]
    [SerializeField] private GameObject chargedCrystalQLPrefab;
    [SerializeField] private GameObject chargedCrystalQNPrefab;

    [Space]
    [SerializeField, Tooltip("Game Object holding qualitative crystal on desk")]
    private GameObject qualitativeCrystalDisplay;
    [SerializeField, Tooltip("Game Object holding quantitative crystal on desk")]
    private GameObject quantitativeCrystalDisplay;

    private SwitchImageDisplay deskSpriteSwitch;
    public bool isCompleted, isQlTaken, isQnTaken;


    public override void Start()
    {
        deskSpriteSwitch = GetComponent<SwitchImageDisplay>();

        LoadState();
        if (isCompleted)    // no need to preload scene since game is finished
            preloadMinigame = false;

        base.Start();
    }

    public override void OnItemPlacement(Item item)
    {
        DisplayCrystalOnCharger(item.itemName);
    }

    void DisplayCrystalOnCharger(string itemName)
    {
        switch (itemName)
        {
            case ("Dark Blue Crystal"):
                Instantiate(unchargedCrystalQNPrefab, quantitativeCrystalDisplay.transform);
                break;

            case ("Dark Purple Crystal"):
                Instantiate(unchargedCrystalQLPrefab, qualitativeCrystalDisplay.transform);
                break;

            default:
                Debug.LogError("Can't display crystal image on charger. Check item names");
                break;
        }
    }

    public override void EndGame()
    {
        base.EndGame();
        deskSpriteSwitch.SwitchImage();     // switch to clean desk image
        ReplaceCrystals();
        isCompleted = true;
        SaveState();
    }

    void ReplaceCrystals()
    {
        // destroy existing uncharged crystals
        Destroy(qualitativeCrystalDisplay.transform.GetChild(0).gameObject);
        Destroy(quantitativeCrystalDisplay.transform.GetChild(0).gameObject);

        // spawn charged crystals
        GameObject crystalQL = Instantiate(chargedCrystalQLPrefab, qualitativeCrystalDisplay.transform);
        GameObject crystalQN = Instantiate(chargedCrystalQNPrefab, quantitativeCrystalDisplay.transform);

        // subscribe to pick up event of crystals to save which is picked
        crystalQL.GetComponent<PickUp>().onPickUp += MarkCrystalRemoved;
        crystalQN.GetComponent<PickUp>().onPickUp += MarkCrystalRemoved;
    }

    void MarkCrystalRemoved(Item crystal)
    {
        if (crystal.itemName == "Bright Blue Crystal")
            isQnTaken = true;

        if (crystal.itemName == "Bright Purple Crystal")
            isQlTaken = true;

        SaveState();
    }

    public void SaveState()
    {
        // save state will be called 3 times. game end, QL crystal picked, and QN crystal picked
        // save states: 0 - default; 1 - game ended; 2 - ended and QL picked; 3 - ended and QN picked; 4 - ended and both crystals picked
        SaveLoadManager.interactableStates.TryGetValue(name + "_hub", out int saveState);
        print("saved state value = " + saveState);

        if (saveState == 0) // nothing is saved yet and game is just ended
        {
            SaveLoadManager.SetInteractableState(name + "_hub", 1);
            print("saving state as: 1");
        }
        else    // game is ended and a crystal is taken
        {
            if (isQlTaken && isQnTaken)
            {
                SaveLoadManager.SetInteractableState(name + "_hub", 4);
                print("save state as 4");
            }
            else if (isQlTaken)
            {
                SaveLoadManager.SetInteractableState(name + "_hub", 2);
                print("Save state as 2");
            }
            else if (isQnTaken)
            {
                SaveLoadManager.SetInteractableState(name + "_hub", 3);
                print("Save state as 3");
            }
        }
    }

    public void LoadState()
    {
        SaveLoadManager.interactableStates.TryGetValue(name + "_hub", out int saveState);
        print("Load state: Try get value returned " + saveState);

        if (saveState == 0)     // game isn't finished
        {
            print("load state: game isn't finished");
        }
        else if (saveState == 1)     // game ended but no crystal picked
        {
            print("load state: game is finished w/o crystals picked");
            EndGame();
        }
        else if (saveState == 2)    // game ended and QL picked
        {
            print("load state: game is finished and QL picked");
            EndGame();
            isQlTaken = true;

            // remove crystal created from ReplaceCrystals()
            Destroy(qualitativeCrystalDisplay.transform.GetChild(0).gameObject);
        }
        else if (saveState == 3)    // game ended and QN picked
        {
            print("load state: game is finished and QN picked");
            EndGame();
            isQnTaken = true;

            // remove crystal created from ReplaceCrystals()
            Destroy(quantitativeCrystalDisplay.transform.GetChild(0).gameObject);
        }
        else if (saveState == 4)    // game ended and both crystals picked
        {
            print("load state: game is finished and both picked");
            EndGame();
            isQlTaken = true;
            isQnTaken = true;

            // remove crystals created from ReplaceCrystals()
            Destroy(qualitativeCrystalDisplay.transform.GetChild(0).gameObject);
            Destroy(quantitativeCrystalDisplay.transform.GetChild(0).gameObject);
        }
    }
}
