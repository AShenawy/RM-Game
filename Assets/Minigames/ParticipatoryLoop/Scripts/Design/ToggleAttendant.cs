using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.PartLoop
{
    public class ToggleAttendant : MonoBehaviour
    {
        public Attendant attendant;
        public Text costDisplay;
        public BudgetCounter budgeter;
        public BudgetLock startMeetingButton;
        private Toggle toggle;


        // Start is called before the first frame update
        void Start()
        {
            toggle = GetComponent<Toggle>();

            toggle.onValueChanged.AddListener(delegate
            {
                ChangeAttendance(toggle.isOn);
                budgeter.AdjustBudget(attendant.cost, toggle.isOn);
                startMeetingButton.CheckAttendants();
            });

            costDisplay.text = $"({attendant.cost} {CultureInfo.GetCultureInfo("et").NumberFormat.CurrencySymbol})";
        }

        void ChangeAttendance(bool value)
        {
            attendant.isAttending = value;
        }
    }
}