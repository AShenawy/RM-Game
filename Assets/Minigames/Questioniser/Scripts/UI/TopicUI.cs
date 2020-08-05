using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class TopicUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI topicText;

        void Start()
        {
            GameManager.Instance.OnTopicChanged += TopicChangedHandler;
        }

        void TopicChangedHandler(string topicName)
        {
            topicText.text = topicName;
        }

        void OnDisable()
        {
            if (GameManager.InstanceExists)
                GameManager.Instance.OnTopicChanged -= TopicChangedHandler;
        }
    }
}