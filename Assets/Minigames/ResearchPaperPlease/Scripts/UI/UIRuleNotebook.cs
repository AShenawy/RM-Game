using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class UIRuleNotebook : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI leftPage;
        [SerializeField] private TextMeshProUGUI rightPage;
        [SerializeField] private Image leftArrow;
        [SerializeField] private Image rightArrow;

        private void OnEnable()
        {
            GameManager.OnRulesUpdated += RulesUpdatedHandler;
        }

        private void RulesUpdatedHandler(string previousRule, string nextRule)
        {
            if (string.IsNullOrEmpty(previousRule))
            {
                leftArrow.gameObject.SetActive(false);
            }
            else
            {
                leftArrow.gameObject.SetActive(true);
                leftPage.text = previousRule;
            }

            if (string.IsNullOrEmpty(nextRule))
            {
                rightArrow.gameObject.SetActive(false);
            }
            else
            {
                rightArrow.gameObject.SetActive(true);
                rightPage.text = nextRule;
            }
        }

        private void OnDisable()
        {
            GameManager.OnRulesUpdated -= RulesUpdatedHandler;
        }
    }
}