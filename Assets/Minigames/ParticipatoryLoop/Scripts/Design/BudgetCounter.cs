using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.PartLoop
{
    public class BudgetCounter : MonoBehaviour
    {
        public delegate void BudgetChanged();
        public event BudgetChanged onBudgetChanged;
        public int startBudget;
        public int currentBudget;
        public Text budgetDisplay;


        // Start is called before the first frame update
        void Start()
        {

        }

        public void ResetCounter()
        {
            currentBudget = startBudget;
            UpdateBudgetDisplay();
        }

        public void AdjustBudget(int cost, bool isUsed)
        {
            if (isUsed)
            {
                currentBudget -= cost;
                UpdateBudgetDisplay();
                onBudgetChanged?.Invoke();
            }
            else
            {
                currentBudget += cost;
                UpdateBudgetDisplay();
                onBudgetChanged?.Invoke();
            }
        }

        void UpdateBudgetDisplay()
        {
            budgetDisplay.text = currentBudget.ToString() + " " + CultureInfo.GetCultureInfo("et").NumberFormat.CurrencySymbol;
        }
    }
}