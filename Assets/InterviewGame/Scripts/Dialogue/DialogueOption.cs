public class DialogueOption
{
    public string Text;
    public string SpecialInteractionItems;
    public bool RightOption;
    public int DestNodeID;
    public string Pro;
    public string Con;
    public DialogueOption() { } // for serialization

    public DialogueOption(string text, string pro, string con, string items, bool rightOpt, int dest)
    {
        this.Text = text;
        this.Pro = pro;
        this.Pro = con;
        this.SpecialInteractionItems = items;
        this.RightOption = rightOpt;
        this.DestNodeID = dest;
    }
}

