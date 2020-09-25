using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.SortGame
{
    // this script handles crystal charging station
    public class CrystalCharger : MonoBehaviour
    {
        public SortBoxBehaviour sortingBox;     // box this charger is linked to
        public Crystal crystal;         // crystal this charger is linked to

        //The levitation for the crytsals.  
        //public RectTransform levitate;          //for the floating crystals.
        //public RectTransform shinnny;           //stored position for the rect transfrom.  
        //public Vector3 temPos;

        //inputs for the levitations 
        //public float amp;

        private SoundManipulator soundManipulator;

        private void OnEnable()
        {
            sortingBox.onItemDropped += AdjustCharge;
        }

        // Start is called before the first frame update
        void Start()
        {
            //shinnny = crystalStation.GetComponent<RectTransform>();
            soundManipulator = GetComponent<SoundManipulator>();
        }

        void AdjustCharge(int charge)
        {
            crystal.AdjustGlow(charge);

            if (charge > 0)
                soundManipulator.PlaySound();
        }


        //public void Rise()      //The levitation of the crystals        //******** functionality moved to VerticalOscillator
        //{
        //    temPos = levitate.position;
        //    levitate.transform.Translate(Vector3.up * amp * Mathf.Sin(Time.fixedTime));
        //    shinnny.position = levitate.position;
        //}

        private void OnDisable()
        {
            sortingBox.onItemDropped -= AdjustCharge;
        }
    }
}