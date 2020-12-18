using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.PartLoop
{
    public class DesignPlan : MonoBehaviour, IPointerClickHandler
    {
        public MeetingBehaviour meetingBehaviour;
        public Button submitButton;
        public GameObject planView;
        public Idea ideationIdea;
        public Text ideationSumm;
        public Idea targetAudienceIdea;
        public Text targetSumm;
        public Idea storyIdea;
        public Text storySumm;
        public Idea artIdea;
        public Text artSumm;
        public Idea soundIdea;
        public Text soundSumm;

        private void OnEnable()
        {
            ClearSummaries();
            ClearSavedIdeas();
        }

        // Start is called before the first frame update
        void Start()
        {
            submitButton.interactable = false;
        }

        void ClearSummaries()
        {
            ideationSumm.text = "";
            targetSumm.text = "";
            storySumm.text = "";
            artSumm.text = "";
            soundSumm.text = "";
        }

        void ClearSavedIdeas()
        {
            ideationIdea.Clear();
            targetAudienceIdea.Clear();
            storyIdea.Clear();
            artIdea.Clear();
            soundIdea.Clear();
        }


        public void SaveIdea(Idea idea)
        {
            switch (meetingBehaviour.meetingStage)
            {
                case MeetingStages.Ideation:
                    ideationIdea = idea;
                    ideationSumm.text = idea.designDocEntry;
                    submitButton.GetComponent<SubmitIdea>().UpdateNextStage(MeetingStages.TargetAudience);
                    submitButton.interactable = true;
                    break;

                case MeetingStages.TargetAudience:
                    targetAudienceIdea = idea;
                    targetSumm.text = idea.designDocEntry;
                    submitButton.GetComponent<SubmitIdea>().UpdateNextStage(MeetingStages.Story);
                    submitButton.interactable = true;
                    break;

                case MeetingStages.Story:
                    storyIdea = idea;
                    storySumm.text = idea.designDocEntry;
                    submitButton.GetComponent<SubmitIdea>().UpdateNextStage(MeetingStages.Art);
                    submitButton.interactable = true;
                    break;

                case MeetingStages.Art:
                    artIdea = idea;
                    artSumm.text = idea.designDocEntry;
                    submitButton.GetComponent<SubmitIdea>().UpdateNextStage(MeetingStages.Music);
                    submitButton.interactable = true;
                    break;

                case MeetingStages.Music:
                    soundIdea = idea;
                    soundSumm.text = idea.designDocEntry;
                    submitButton.GetComponent<SubmitIdea>().UpdateNextStage(MeetingStages.Conclusion);
                    submitButton.interactable = true;
                    break;

                default:
                    break;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            planView.SetActive(true);
        }
    }
}