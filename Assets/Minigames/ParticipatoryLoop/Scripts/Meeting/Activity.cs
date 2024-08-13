using UnityEngine;

namespace Methodyca.Minigames.PartLoop
{
    public class Activity : MonoBehaviour
    {
        public ActivityType activity;
        public int cost;
        public bool isHappening;


    }

    public enum ActivityType { Ideation, TargetAudience, StoryWorkshop, ArtWorkshop, SoundWorkshop }
}