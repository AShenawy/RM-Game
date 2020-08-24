using Methodyca.Core;
using UnityEngine;


// This script handles initiation of a minigame when interacted with
public class StartSortingGame : ObjectInteraction
{
    [Header("Specific Script Parameters")]
    [SerializeField, Tooltip("The scene name of minigame to be loaded")]
    private string minigameSceneName;
    [SerializeField, Multiline, Tooltip("In-game text to be displayed if game unplayable yet")]
    private string responseForDisabled;
    [Space]
    public GameObject unchargedCrystalQNPrefab;
    public GameObject unchargedCrystalQLPrefab;
    [Space]
    public GameObject chargedCrystalQNPrefab;
    public GameObject chargedCrystalQLPrefab;
    [Space]
    [SerializeField, Tooltip("Game Object holding qualitative crystal on desk")]
    private GameObject qualitativeCrystalDisplay;
    [SerializeField, Tooltip("Game Object holding quantitative crystal on desk")]
    private GameObject quantitativeCrystalDisplay;

    [HideInInspector] public bool isGameWon = false;

    private bool canStartGame;  // Check whether player can start sorting game or not
    private SwitchImageDisplay deskSpriteSwitch;

    protected override void Start()
    {
        base.Start();

        deskSpriteSwitch = GetComponent<SwitchImageDisplay>();
    }

    public override void InteractWithObject()
    {
        base.InteractWithObject();

        if (canStartGame)
            SceneManagerScript.instance.LoadSceneAdditive(minigameSceneName);   // load the sorting minigame
        else
            DialogueHandler.instance.DisplayDialogue(responseForDisabled);

    }

    public override void UseWithHeldItem(Item item)
    {
        base.UseWithHeldItem(item);

        if (usedCorrectItem)
        {
            DisplayCrystalOnCharger(item.name);   //See notes below
            requiredItemsLeft--; // take down the required items count by 1
            InventoryManager.instance.Remove(item);
        }
        else
            return;

        // if all required items are used, then unlock
        if (requiredItemsLeft < 1)
            canStartGame = true;
    }

    private void OnGameWon()
    {
        deskSpriteSwitch.SwitchImage();
        Destroy(qualitativeCrystalDisplay.GetComponentInChildren<Transform>().gameObject);
        Destroy(quantitativeCrystalDisplay.GetComponentInChildren<Transform>().gameObject);
        Instantiate(chargedCrystalQLPrefab, qualitativeCrystalDisplay.transform);
        Instantiate(chargedCrystalQNPrefab, quantitativeCrystalDisplay.transform);
    }

    private void DisplayCrystalOnCharger(string itemName)
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


        /* ********************************
         * NOTES:
         * Instead of switching static images of crystals, it's better to instantiate each crytal on a charger
         * with the Observe script so it has a description/interaction. Then, when the minigame is won, switch 
         * the discharged crytals with charged ones with a Pickup script on them so the player can interactively
         * pick them instead of automatically being added to the inventory.
         * After minigame is won, desk should be inactive and there's on big collision box covering both charged
         * crystals. This way, clicking on either crystal will automatically add both crytals to inventory.
         */

        //private void DisplayCrystalOnCharger(string name)
        //{
        //    switch (name)
        //    {
        //        case ("Dark Blue Crystal"):
        //            crystalQNDisplay.ShowImage();
        //            break;

        //        case ("Dark Purple Crystal"):
        //            crystalQLDisplay.ShowImage();
        //            break;

        //        default:
        //            Debug.LogError("Can't display crystal image on charger");
        //            break;
        //    }
        //}

        //public void CompleteGame()
        //{
        //    isGameWon = true;
        //    crystalQLDisplay.SwitchImage();
        //    crystalQNDisplay.SwitchImage();
        //}
    }
