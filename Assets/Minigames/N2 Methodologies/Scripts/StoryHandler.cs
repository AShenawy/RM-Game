using UnityEngine;
using Methodyca.Core;


namespace Methodyca.Minigames.Methodologies
{
    // Script that kicks off the ink story in a minigame
    public class StoryHandler : MonoBehaviour
    {
        private MethodologyStory methodStory;

        // Start is called before the first frame update
        void Start()
        {
            methodStory = GetComponent<MethodologyStory>();
            methodStory.StartStory();
            methodStory.OnEndStory += OnMinigameEnded;
            CursorManager.instance.SetDefaultCursor();
        }

        void OnMinigameEnded()
        {
            methodStory.OnEndStory -= OnMinigameEnded;
            if (methodStory.wonDiscussion)
                BadgeManager.instance.SetMinigameComplete((int)methodStory.minigameID);

            SceneManagerScript.instance.UnloadScene();
        }
    }
}