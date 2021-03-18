#define TESTING
using UnityEngine;
using System.Collections.Generic;


namespace Methodyca.Core
{
    // this script handles progression & objects' update/activation in the main world between nodes (1,2,3) in Act2
    public class Act2ProgressController : MonoBehaviour, ISaveable, ILoadable
    {
        [Header("N1 ELEMENTS", order = -2), Space(-25f, order = -1)]
        [Header("__________________________________________", order = 0), Space(-5f, order = 1)]
        [Header("N1 Qualitative", order = 2)]
        public Operate tollMachineQL;
        public Map mapTable;    //TODO remove if unused
        public Door gateQL;     //TODO remove if unused
        
        [Header("N1 Quantitative")]
        public Operate tollMachineQN;
        public Door gateQN;

        [Header("N1 Rewards")]
        public Item coin1;         //TODO remove after debugging
        public Item coin2;         //TODO remove after debugging
        public List<Item> n1Rewards;

        [Header("N2 ELEMENTS", order = -2), Space(-25f, order = -1)]
        [Header("__________________________________________", order = 0), Space(-5f, order = 1)]
        //[Header("N2 Qualitative")]
        //[Header("N2 Quantitative")]
        [Header("N2 Rewards", order = 5)]
        public List<Item> n2Rewards;

        [Header("N3 ELEMENTS", order = -2), Space(-25f, order = -1)]
        [Header("__________________________________________", order = 0), Space(-5f, order = 1)]
        [Header("N3 Qualitative", order = 2)]
        public NPC laceCharQL;
        
        [Header("N3 Quantitative")]
        public NPC laceCharQN;

        [Header("N3 Rewards")]
        public Item punchCard1;         //TODO remove after debugging
        public Item punchCard2;         //TODO remove after debugging
        public List<Item> n3Rewards;

        [Header("DEBUGGING")]
        public List<Item> rewardsGiven = new List<Item>();      //TODO make private after debugging


        // Use this for initialization
        void Start()
        {
            tollMachineQL.onCorrectItemUsed += UseItemWithMatchingToll;
            tollMachineQN.onCorrectItemUsed += UseItemWithMatchingToll;

            laceCharQL.onCorrectItemUsed += UseItemWithMatchingNPC;
            laceCharQN.onCorrectItemUsed += UseItemWithMatchingNPC;
        }

#if TESTING
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                InventoryManager.instance.Add(coin1);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                InventoryManager.instance.Add(coin2);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                InventoryManager.instance.Add(punchCard1);

            if (Input.GetKeyDown(KeyCode.Alpha4))
                InventoryManager.instance.Add(punchCard2);
        }
#endif

        private void OnDisable()
        {
            tollMachineQL.onCorrectItemUsed -= UseItemWithMatchingToll;
            tollMachineQN.onCorrectItemUsed -= UseItemWithMatchingToll;
        }

        public void GiveMinigameReward(Minigames id)
        {
            LoadState();

            // Give rewards based on the minigames node where they belong
            if (id == Minigames.Questionnaire || id == Minigames.Interview || id == Minigames.Observation || id == Minigames.DocStudy)
            {
                if (n1Rewards.Count > 0)
                    GiveRewardFromList(n1Rewards);
            }
            else if (id == Minigames.Participatory || id == Minigames.Prototyping)
            {
                if (n3Rewards.Count > 0)
                    GiveRewardFromList(n3Rewards);
            }
            else
                Debug.LogWarning("Unable to give minigame completion reward. Minigame ID does not exist or not linked to reward.");

            SaveState();

            void GiveRewardFromList(List<Item> list)
            {
                InventoryManager.instance.Add(list[0]);
                rewardsGiven.Add(list[0]);
            }
        }

        public void SaveState()
        {
            foreach (Item i in rewardsGiven)
                SaveLoadManager.SetInteractableState($"{name}_{i.name}", 1);
        }

        public void LoadState()
        {
            rewardsGiven.Clear();

            foreach (Item i in n1Rewards.ToArray())
            {
                // if reward is saved then it's given and should be removed from available rewards
                if (SaveLoadManager.interactableStates.ContainsKey($"{name}_{i.name}"))
                {
                    //print("found " + i.name);
                    rewardsGiven.Add(i);
                    n1Rewards.Remove(i);
                }
            }

            foreach (Item i in n2Rewards.ToArray())
            {
                // if reward is saved then it's given and should be removed from available rewards
                if (SaveLoadManager.interactableStates.ContainsKey($"{name}_{i.name}"))
                {
                    rewardsGiven.Add(i);
                    n2Rewards.Remove(i);
                }
            }

            foreach (Item i in n3Rewards.ToArray())
            {
                // if reward is saved then it's given and should be removed from available rewards
                if (SaveLoadManager.interactableStates.ContainsKey($"{name}_{i.name}"))
                {
                    rewardsGiven.Add(i);
                    n3Rewards.Remove(i);
                }
            }
        }

        //***********   Methods for N1   ***********

        private void UseItemWithMatchingToll(Operate toll, Item itemUsed)
        {
            if (toll == tollMachineQN)
            {
                print("coin used in QN toll machine. Updating QL machine");
                // unsub from the same event called when using the item to avoid looping this method
                // after running the UseWithHeldItem method, re-sub again for future events
                tollMachineQL.onCorrectItemUsed -= UseItemWithMatchingToll;
                tollMachineQL.UseWithHeldItem(itemUsed);
                tollMachineQL.onCorrectItemUsed += UseItemWithMatchingToll;
            }
            else
            {
                print("coin used in QL toll machine. Updating QN machine");
                // unsub from the same event called when using the item to avoid looping this method
                // after running the UseWithHeldItem method, re-sub again for future events
                tollMachineQN.onCorrectItemUsed -= UseItemWithMatchingToll;
                tollMachineQN.UseWithHeldItem(itemUsed);
                tollMachineQN.onCorrectItemUsed += UseItemWithMatchingToll;
            }
        }

        //***********   Methods for N3   ***********

        private void UseItemWithMatchingNPC(NPC npc, Item itemUsed)
        {
            if (npc == laceCharQL)
            {
                print("punch card used with Lace QL. Updating Lace QN");
                // unsub from the same event called when using the item to avoid looping this method
                // after running the UseWithHeldItem method, re-sub again for future events
                laceCharQN.onCorrectItemUsed -= UseItemWithMatchingNPC;
                laceCharQN.UseWithHeldItem(itemUsed);
                laceCharQN.onCorrectItemUsed += UseItemWithMatchingNPC;
            }
            else
            {
                print("punch card used with Lace QN. Updating Lace QL");
                // unsub from the same event called when using the item to avoid looping this method
                // after running the UseWithHeldItem method, re-sub again for future events
                laceCharQL.onCorrectItemUsed -= UseItemWithMatchingNPC;
                laceCharQL.UseWithHeldItem(itemUsed);
                laceCharQL.onCorrectItemUsed += UseItemWithMatchingNPC;
            }
        }
    }
}