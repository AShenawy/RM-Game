using UnityEngine;

namespace Methodyca.Minigames.DocStudy
{
    public class Question : MonoBehaviour
    {
        [TextArea(1, 3)] public string Title;
        public Thread[] Threads;
    }
}