using UnityEngine;
using System.Collections;


namespace Methodyca.Core
{
    // this script handles progression & objects' update/activation in the main world between nodes (1,2,3) in Act2
    public class Act2ProgressController : MonoBehaviour
    {
        [Header("N1 Qualitative")]
        public Operate tollMachineQL;
        public Map mapTable;
        public Door gateQL;

        [Header("N1 Quantitative")]
        public Operate tollMachineQN;
        public Door gateQN;

        [Header("N1 Items")]
        public Item coin1;
        public Item coin2;

        //[Header("N2 Qualitative")]
        //[Header("N2 Quantitative")]
        //[Header("N3 Qualitative")]
        //[Header("N3 Quantitative")]



        // Use this for initialization
        void Start()
        {
            tollMachineQL.onCorrectItemUsed += UseItemWithMatchingToll;
            tollMachineQN.onCorrectItemUsed += UseItemWithMatchingToll;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                InventoryManager.instance.Add(coin1);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                InventoryManager.instance.Add(coin2);
        }

        private void OnDisable()
        {
            tollMachineQL.onCorrectItemUsed -= UseItemWithMatchingToll;
            tollMachineQN.onCorrectItemUsed -= UseItemWithMatchingToll;
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
    }
}