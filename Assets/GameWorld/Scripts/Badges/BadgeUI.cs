using UnityEngine;
using System.Collections.Generic;


namespace Methodyca.Core
{
    // This script is responsible for displaying badges in the badges app
    public class BadgeUI : MonoBehaviour
    {
        public List<BadgeBehaviour> availableBadges = new List<BadgeBehaviour>();


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DisplayBagdeInApp(Minigames id)
        {
            availableBadges.Find(x => x.minigameID == id).gameObject.SetActive(true);
        }
    }
}