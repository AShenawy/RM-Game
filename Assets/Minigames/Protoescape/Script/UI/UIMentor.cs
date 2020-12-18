using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIMentor : MonoBehaviour
    {
        [SerializeField] private GameObject speechBubble;
        [SerializeField] private TMPro.TextMeshProUGUI mentorText;

        private void OnEnable()
        {
            MentorController.OnMentorTalked += MentorTalkedHandler;
        }

        private void MentorTalkedHandler(string dialog)
        {
            speechBubble.SetActive(true);
            mentorText.text = dialog;
        }

        private void OnDisable()
        {
            MentorController.OnMentorTalked -= MentorTalkedHandler;
        }
    }
}