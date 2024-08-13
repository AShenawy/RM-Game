//#define TESTING
using UnityEngine;

namespace Methodyca.Core
{

    [RequireComponent(typeof(MinigameInteraction))]
    public class ResearchGameHub : MinigameHub, ISaveable, ILoadable
    {
        [Header("Specific Script Parameters")]
        [SerializeField] private Minigames minigameID;
        [SerializeField] private GameObject crystalSpotQL;
        [SerializeField] private GameObject crystalSpotQN;
        [Space]
        [SerializeField] private GameObject chargedCrystalQLPrefab;
        [SerializeField] private GameObject chargedCrystalQNPrefab;

        private bool isQLTaken, isQNTaken;


        // Use this for initialization
        public override void Start()
        {
            LoadState();

            if (!isCompleted)
                CheckGameWon();
        }

#if TESTING
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                EndGame();
        }
#endif

        public override void EndGame()
        {
            base.EndGame();
            ReplaceCrystals();
            SaveState();
        }

        void CheckGameWon()
        {
            if (SceneManagerScript.instance.minigamesWon.Contains((int)minigameID))
                EndGame();
        }

        void ReplaceCrystals()
        {
            // Destroy uncharged crystal objects
            Destroy(crystalSpotQL.transform.GetChild(0).gameObject);
            Destroy(crystalSpotQN.transform.GetChild(0).gameObject);

            // Instantiate charged crystal objects
            GameObject crystalQL = Instantiate(chargedCrystalQLPrefab, crystalSpotQL.transform);
            GameObject crystalQN = Instantiate(chargedCrystalQNPrefab, crystalSpotQN.transform);

            // subscribe to pick up event of crystals to save which is picked
            crystalQL.GetComponent<PickUp>().onPickUp += MarkCrystalRemoved;
            crystalQN.GetComponent<PickUp>().onPickUp += MarkCrystalRemoved;
        }

        void MarkCrystalRemoved(Item crystal)
        {
            if (crystal.itemName == "Bright Purple Crystal")
                isQLTaken = true;

            if (crystal.itemName == "Bright Blue Crystal")
                isQNTaken = true;


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

            else if (isQLTaken && isQNTaken)
                // game is ended and both crystals taken
                SaveLoadManager.SetInteractableState(name + "_hub", 4);

            else if (isQLTaken)
                // game ended and QL crystal taken
                SaveLoadManager.SetInteractableState(name + "_hub", 2);

            else if (isQNTaken)
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
                isQLTaken = true;

                // remove crystal created from ReplaceCrystals()
                Destroy(crystalSpotQL.transform.GetChild(0).gameObject);
            }
            else if (saveState == 3)    // game ended and QN picked
            {
                EndGame();
                isQNTaken = true;

                // remove crystal created from ReplaceCrystals()
                Destroy(crystalSpotQN.transform.GetChild(0).gameObject);
            }
            else if (saveState == 4)    // game ended and both crystals picked
            {
                EndGame();
                isQLTaken = true;
                isQNTaken = true;

                // remove crystals created from ReplaceCrystals()
                Destroy(crystalSpotQL.transform.GetChild(0).gameObject);
                Destroy(crystalSpotQN.transform.GetChild(0).gameObject);
            }
        }
    }
}