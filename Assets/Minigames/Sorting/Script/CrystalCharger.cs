using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.SortGame
{
    // this script handles crystal charging station and crystal behaviours
    public class CrystalCharger : MonoBehaviour
    {
        public Image crystalSprite;
        public Sprite[] crystalPhases;

        //The levitation for the crytsals.  
        public RectTransform levitate;          //for the floating crystals.
        public RectTransform shinnny;           //stored position for the rect transfrom.  
        public Vector3 temPos;

        //inputs for the levitations 
        public float amp;

        // Start is called before the first frame update
        void Start()
        {
            shinnny = crystalStation.GetComponent<RectTransform>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ChargeUp()
        {

        }

        public void ChargeDown()
        {

        }

        public void Rise()      //The levitation of the crystals        //******** will need to remove
        {
            temPos = levitate.position;
            levitate.transform.Translate(Vector3.up * amp * Mathf.Sin(Time.fixedTime));
            shinnny.position = levitate.position;
        }
    }
}