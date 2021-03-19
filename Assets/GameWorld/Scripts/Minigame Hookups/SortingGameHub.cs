using UnityEngine;
using Methodyca.Core;

// this script handles the sorting minigame hub
[RequireComponent(typeof(SwitchImageDisplay))]
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
    public bool isQlTaken, isQnTaken;


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

        if (saveState == 0) 
            // nothing is saved yet and game is just ended
            SaveLoadManager.SetInteractableState(name + "_hub", 1);

        else if (isQlTaken && isQnTaken)
            // game is ended and both crystals taken
            SaveLoadManager.SetInteractableState(name + "_hub", 4);

        else if (isQlTaken)
            // game ended and QL crystal taken
            SaveLoadManager.SetInteractableState(name + "_hub", 2);

        else if (isQnTaken)
            // game ended and QN crystal taken
            SaveLoadManager.SetInteractableState(name + "_hub", 3);
    }

    public void LoadState()
    {
        SaveLoadManager.interactableStates.TryGetValue(name + "_hub", out int saveState);

        if (saveState == 0)     // game isn't finished
            return;

        else if (saveState == 1)     // game ended but no crystal picked
            EndGame();

        else if (saveState == 2)    // game ended and QL picked
        {
            EndGame();
            isQlTaken = true;

            // remove crystal created from ReplaceCrystals()
            Destroy(qualitativeCrystalDisplay.transform.GetChild(0).gameObject);
        }
        else if (saveState == 3)    // game ended and QN picked
        {
            EndGame();
            isQnTaken = true;

            // remove crystal created from ReplaceCrystals()
            Destroy(quantitativeCrystalDisplay.transform.GetChild(0).gameObject);
        }
        else if (saveState == 4)    // game ended and both crystals picked
        {
            EndGame();
            isQlTaken = true;
            isQnTaken = true;

            // remove crystals created from ReplaceCrystals()
            Destroy(qualitativeCrystalDisplay.transform.GetChild(0).gameObject);
            Destroy(quantitativeCrystalDisplay.transform.GetChild(0).gameObject);
        }
    }
}
