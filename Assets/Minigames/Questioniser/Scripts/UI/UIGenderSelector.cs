using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class UIGenderSelector : MonoBehaviour
    {
        [SerializeField] Toggle male;
        [SerializeField] Toggle female;

        public void SetPreferredGender()
        {
            if (male.isOn)
            {
                GenderManager.Current.IsMale = true;
            }
            else if (female.isOn)
            {
                GenderManager.Current.IsMale = false;
            }
        }
    }
}