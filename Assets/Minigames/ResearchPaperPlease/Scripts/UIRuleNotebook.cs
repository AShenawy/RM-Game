using UnityEngine;
using TMPro;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class UIRuleNotebook : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI leftPage;
        [SerializeField] private TextMeshProUGUI rightPage;

        private void OnEnable()
        {
            GameManager.OnRulesUpdated += RulesUpdatedHandler;
        }

        private void RulesUpdatedHandler(string previousRule, string nextRule)
        {
            leftPage.text = previousRule;
            rightPage.text = nextRule;
        }

        private void OnDisable()
        {
            GameManager.OnRulesUpdated += RulesUpdatedHandler;
        }
    }
}