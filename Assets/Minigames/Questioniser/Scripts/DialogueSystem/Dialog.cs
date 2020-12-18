using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class Dialog : MonoBehaviour
    {
        [TextArea(3, 10)] public string Text;
        public Respond[] Responds;
    }
}