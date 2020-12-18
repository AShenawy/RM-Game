using UnityEngine;

namespace Methodyca.Minigames.PartLoop
{
    public class AdvanceFeedback : MonoBehaviour
    {
        public ReviewStages reviewStage;
        public RecapBehaviour recapBehaviour;

        public void Advance()
        {
            if (reviewStage < ReviewStages.Outro)
            {
                reviewStage++;
                recapBehaviour.AdvanceReview(reviewStage);
            }
            else
                recapBehaviour.OnRecapEnded();

        }
    }
}