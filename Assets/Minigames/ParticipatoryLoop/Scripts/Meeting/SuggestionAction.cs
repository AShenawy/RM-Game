using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;


namespace Methodyca.Minigames.PartLoop
{
    public class SuggestionAction : MonoBehaviour, IPointerClickHandler
    {
        public SuggestionManager suggestionManager;
        public MeetingBehaviour meetingBehaviour;
        public GameObject speechBubble;
        public TMP_Text speechTextDisplay;

        private Attendant attendant;


        // Start is called before the first frame update
        void Start()
        {
            attendant = GetComponent<Attendant>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (meetingBehaviour.meetingStage == MeetingStages.Ideation)
            {
                suggestionManager.OnSuggestionClicked(speechBubble, attendant.ideationIdeas[Random.Range(0, attendant.ideationIdeas.Length)]);
            }
            else if (meetingBehaviour.meetingStage == MeetingStages.TargetAudience)
            {
                suggestionManager.OnSuggestionClicked(speechBubble, attendant.targetIdeas[Random.Range(0, attendant.targetIdeas.Length)]);

            }
            else if (meetingBehaviour.meetingStage == MeetingStages.Story)
            {
                suggestionManager.OnSuggestionClicked(speechBubble, attendant.storyIdeas[Random.Range(0, attendant.storyIdeas.Length)]);

            }
            else if (meetingBehaviour.meetingStage == MeetingStages.Art)
            {
                suggestionManager.OnSuggestionClicked(speechBubble, attendant.artIdeas[Random.Range(0, attendant.artIdeas.Length)]);

            }
            else if (meetingBehaviour.meetingStage == MeetingStages.Music)
            {
                suggestionManager.OnSuggestionClicked(speechBubble, attendant.soundIdeas[Random.Range(0, attendant.soundIdeas.Length)]);

            }
        }
    }
}