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
            GameManager.OnLevelInitiated += LevelInitiatedHandler;
        }

        private void LevelInitiatedHandler(LevelData data)
        {
            leftArrow.gameObject.SetActive(data.LevelRules.Length > 2);
            rightArrow.gameObject.SetActive(data.LevelRules.Length > 2);
        }

        private void RulesUpdatedHandler(string previousRule, string nextRule)
        {
            leftPage.text = previousRule;
            rightPage.text = nextRule;
        }

        private void OnDisable()
        {
            GameManager.OnRulesUpdated -= RulesUpdatedHandler;
            GameManager.OnLevelInitiated -= LevelInitiatedHandler;
        }
    }
}