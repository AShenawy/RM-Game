using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class StoryPointUI : MonoBehaviour
    {
        [SerializeField] GameObject root;
        [SerializeField] TopicRowUI[] topicRows;

        public void EndDialog()
        {
            GameManager.Instance.HandleStoryDialog();
        }

        void Start()
        {
            GameManager.Instance.OnChartUpdated += ChartUpdatedHandler;
        }

        void ChartUpdatedHandler(string currentTopicName, string currentCardName, bool isEnough)
        {
            if (isEnough)
            {
                root.SetActive(true);
            }
        }

        void OnDisable()
        {
            if (GameManager.InstanceExists)
                GameManager.Instance.OnChartUpdated -= ChartUpdatedHandler;
        }
    }
}