using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.PartLoop
{
    public class Attendant : MonoBehaviour
    {
        public string attendantName;
        public AttendantType type;
        public int cost;
        public bool isAttending;
        public Image art;

        [Header("Ideas")]
        public Idea[] ideationIdeas;
        public Idea[] targetIdeas;
        public Idea[] storyIdeas;
        public Idea[] artIdeas;
        public Idea[] soundIdeas;

        public void HideAttendee()
        {
            art.enabled = false;
        }

        public void ShowAttendee()
        {
            art.enabled = true;
        }
    }

    public enum AttendantType { TeamMember, Expert, EndUser1, EndUser2, Subcontractor, Client }
}