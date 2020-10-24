using UnityEngine;
using Methodyca.Core;

// this script handles the sorting minigame hub
public class SortingGameHub : MinigameHub
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

    public override void Start()
    {
        deskSpriteSwitch = GetComponent<SwitchImageDisplay>();

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
    }

    void ReplaceCrystals()
    {
        // destroy existing uncharged crystals
        Destroy(qualitativeCrystalDisplay.transform.GetChild(0).gameObject);
        Destroy(quantitativeCrystalDisplay.transform.GetChild(0).gameObject);

        // spawn charged crystals
        Instantiate(chargedCrystalQLPrefab, qualitativeCrystalDisplay.transform);
        Instantiate(chargedCrystalQNPrefab, quantitativeCrystalDisplay.transform);
    }
}
