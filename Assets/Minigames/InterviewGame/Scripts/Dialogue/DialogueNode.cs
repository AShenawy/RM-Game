using System.Collections.Generic;

namespace Methodyca.Minigames.Interview
{
    public class DialogueNode
    {
        public int NodeID = -1;
        public List<DialogueOption> Options;
        public string Text;
        public string Thoughts;
        public int ScaleValue;
        public string PlayerText;
        public string Info;

        public DialogueNode() // for serialization
        {
            Options = new List<DialogueOption>();
        }

        public DialogueNode(string text)
        {
            this.Text = text;
            Options = new List<DialogueOption>();
        }
    }
}