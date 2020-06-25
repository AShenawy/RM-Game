using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Dialogue
{
    public List<DialogueNode> Nodes;

    public Dialogue()
    {
        Nodes = new List<DialogueNode>();
    }
  
    public static Dialogue LoadDialogue(TextAsset txtAsset)
    {
        TextReader textReader = new StringReader(txtAsset.text);
        XmlSerializer ser = new XmlSerializer(typeof(Dialogue));

        Dialogue dia = (Dialogue)ser.Deserialize(textReader);

        return dia;
    }
}