using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.SortGame
{
    // this script handles crystal charging station
    public class CrystalCharger : MonoBehaviour
    {
        public SortBoxBehaviour sortingBox;     // box this charger is linked to
        public Crystal crystal;         // crystal this charger is linked to

        private SoundManipulator soundManipulator;

        private void OnEnable()
        {
            sortingBox.onItemDropped += AdjustCharge;
        }

        // Start is called before the first frame update
        void Start()
        {
            soundManipulator = GetComponent<SoundManipulator>();
        }

        void AdjustCharge(int charge)
        {
            crystal.AdjustGlow(charge);

            if (charge > 0)
                soundManipulator.PlaySound();
            else
                soundManipulator.StopSound();
        }

        private void OnDisable()
        {
            sortingBox.onItemDropped -= AdjustCharge;
        }
    }
}