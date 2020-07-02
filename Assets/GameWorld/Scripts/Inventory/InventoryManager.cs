using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Core
{
    // This script handles the player inventory UI
    public class InventoryManager : MonoBehaviour
    {
        // Make this class a singleton
        #region Singleton
        public static InventoryManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}