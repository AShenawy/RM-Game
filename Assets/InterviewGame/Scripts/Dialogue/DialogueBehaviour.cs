using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.UI.ItemSelection;
using Assets.Scripts.UI.Feedback.FeedbackInfo;

public class DialogueBehaviour : MonoBehaviour
{
    private Dialogue dia;
    private GameObject npcSpeechBubble;
    private GameObject npcText;
    private GameObject npcThoughtsBubble;
    private GameObject npcThoughtsText;
    private GameObject option1;
    private GameObject option2;
    private GameObject option3;
    private GameObject option4;
    private IEnumerator displayTextCoroutine;
    private bool isCoroutineRunning = false;
    private bool optionsCreated = false;
    private bool mouseClicked = false;
    private int selectedOption = -2;  // exit node is -1
    private FeedbackData feedbackData;

    public TextAsset dialogueFile;
    public TextAsset feedbackDataJsonFile;

    private void Awake()
    {
        dia = Dialogue.LoadDialogue(dialogueFile);
    }

    void Start()
    {
        feedbackData = JsonUtility.FromJson<FeedbackData>(feedbackDataJsonFile.text);
        npcSpeechBubble = GameObject.Find("SpeechBubble");
        npcText = GameObject.Find("SpeechBubbleText");
        npcThoughtsBubble = GameObject.Find("ThoughtBubble");
        npcThoughtsText = GameObject.Find("ThoughtBubbleText");
        option1 = GameObject.Find("Option1");
        option2 = GameObject.Find("Option2");
        option3 = GameObject.Find("Option3");
        option4 = GameObject.Find("Option4");
        
        RunDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseClicked = true;
        }
    }

    private void CheckForDistractingItems()
    {
        int distractingItemCount = 0;
        foreach (Item item in PlayerData.selectedItems)
        {
            if (item.distracting)
            {
                npcThoughtsText.GetComponent<Text>().text = item.comment;
                distractingItemCount++;
            }
        }
        if (distractingItemCount > 0)
        {
            if (distractingItemCount > 1 && PlayerData.selectedItems.Count == distractingItemCount)
            {
                npcThoughtsText.GetComponent<Text>().text = "This is weird, why do they need all of these items for the interview?";
            }
            else if (distractingItemCount > 1)
            {
                npcThoughtsText.GetComponent<Text>().text = "This is weird, why do they need some of these items for the interview?";
            }
            NPCData.SetMaxComfortValue(distractingItemCount);
            InfoCon conToAdd = feedbackData.cons.SingleOrDefault(i => i.id == "3");
            PlayerData.cons.Add(conToAdd);
            npcThoughtsBubble.SetActive(true);
        }
    }

    private void CheckForRequiredItems()
    {
        int requiredItemsCount = 0;
        foreach(Item item in PlayerData.selectedItems)
        {
            if (item.required)
            {
                requiredItemsCount++;
            }
        }
        if(requiredItemsCount == 0)
        {
            InfoCon conToAdd = feedbackData.cons.SingleOrDefault(i => i.id == "16");
            PlayerData.cons.Add(conToAdd);
        }
    }

    public void RunDialogue()
    {
        CheckForRequiredItems();
        StartCoroutine(Run());
    }

    public void SetSelectedOption(int x)
    {
        selectedOption = x;
        optionsCreated = false;
    }

    private IEnumerator DisplayText(DialogueNode node, GameObject textObject)
    {
        mouseClicked = false;
        isCoroutineRunning = true;
        char[] textChars;
        string text = node.Text;
        string newText;
        int pauseIndex = -1;
        
        if (text.Contains("[PAUSE]"))
        {
            pauseIndex = text.IndexOf('[') - 3;
            text = text.Replace("[PAUSE]", "");
        }
        textChars = text.ToCharArray(0, text.Length);
        for (int i = 0; i < textChars.Length; i++)  
        {
            if (mouseClicked)
            {
                textObject.GetComponent<Text>().text = text;
                break;
            }
            if (pauseIndex == i)
            {
                bool paused = true;
                int pauseTick = 0;
                while (paused && !mouseClicked)
                {
                    newText = npcText.GetComponent<Text>().text;
                    textObject.GetComponent<Text>().text = newText + textChars[i];
                    yield return new WaitForSeconds(0.75f);
                    if (pauseTick == 2)
                    {
                        paused = false;
                    } 
                    else
                    {
                        pauseTick++;
                        i++;
                    }
                }
            }
            else
            {
                newText = npcText.GetComponent<Text>().text;
                textObject.GetComponent<Text>().text = newText + textChars[i];
                yield return new WaitForSeconds(0.03f);
            }
        }
        CreateNodeOptionButtons(node);
        isCoroutineRunning = false;
    } 
    
    private void NodeActions(DialogueNode node)
    {
        if (node.Text.Length > 0)
        {
            npcSpeechBubble.SetActive(true);
        } 
        else
        {
            npcSpeechBubble.SetActive(false);
        }
        npcText.GetComponent<Text>().text = "";
        displayTextCoroutine = DisplayText(node, npcText);
        StartCoroutine(displayTextCoroutine);

        npcThoughtsBubble.SetActive(false);
        if (node.Thoughts.Length > 0)
        {
            npcThoughtsText.GetComponent<Text>().text = node.Thoughts;
            npcThoughtsBubble.SetActive(true);
        }

        if(node.NodeID == 0)
        {
            CheckForDistractingItems();
        }

        if (node.ScaleValue != 0)
        {
            NPCData.AddToComfortValue(node.ScaleValue);
        }

        if (node.Info.Length > 0)
        {
            string[] infoIds = node.Info.Split(',');
            foreach (Info info in feedbackData.info)
            {
                if (Array.IndexOf(infoIds, info.id) > -1 && !PlayerData.info.Contains(info))
                {
                    PlayerData.info.Add(info);
                }
            }
        }
    }

    private void DisplayNode(DialogueNode node)
    {
        option1.SetActive(false);
        option2.SetActive(false);
        option3.SetActive(false);
        option4.SetActive(false);
        NodeActions(node);
    }

    private void CreateNodeOptionButtons(DialogueNode node)
    {
        for (int i = 0; i < node.Options.Count; i++)
        {
            switch (i)
            {
                case 0:
                    SetOptionBtn(option1, node.Options[i]);
                    break;
                case 1:
                    SetOptionBtn(option2, node.Options[i]);
                    break;
                case 2:
                    SetOptionBtn(option3, node.Options[i]);
                    break;
                case 3:
                    SetOptionBtn(option4, node.Options[i]);
                    break;
            }
        }
    }

    private void SetOptionBtn(GameObject btn, DialogueOption opt)
    {
        string optText = SetOptionText(opt);
        if (optText.Length == 0)
        {
            return;
        }
        btn.SetActive(true);
        btn.GetComponentInChildren<Text>().text = optText;
        btn.GetComponent<Button>().onClick.AddListener(delegate
        {
            if (opt.Pro.Length > 0)
            {
                string[] proIds = opt.Pro.Split(',');
                foreach (InfoPro pro in feedbackData.pros)
                {
                    if (Array.IndexOf(proIds, pro.id) > -1 && !PlayerData.pros.Contains(pro))
                    {
                        PlayerData.pros.Add(pro);
                    }
                }
            }

            if (opt.Con.Length > 0)
            {
                string[] conIds = opt.Con.Split(',');
                foreach (InfoCon con in feedbackData.cons)
                {
                    if (Array.IndexOf(conIds, con.id) > -1 && !PlayerData.cons.Contains(con))
                    {
                        PlayerData.cons.Add(con);
                    }
                }
            }

            if (isCoroutineRunning)
            {
                StopCoroutine(displayTextCoroutine);
            }

            SetSelectedOption(opt.DestNodeID);
            if(opt.DestNodeID == -1) 
            {
                SceneManager.LoadScene("Feedback");
            }
            btn.GetComponent<Button>().onClick.RemoveAllListeners();
        });
    }

    private string SetOptionText(DialogueOption opt)
    {
        string optionText;
        string specialItemsString = "";
        bool neededItemExists = false;
        if (opt.SpecialInteractionItems.Length > 0)
        {
            string[] specialItemsList = opt.SpecialInteractionItems.Split(',');
            foreach(Item item in PlayerData.selectedItems)
            {
                if(Array.IndexOf(specialItemsList, item.itemName) > -1)
                {
                    neededItemExists = true;
                    specialItemsString += item.itemName + ", ";
                }
            }
            if(specialItemsString.Length > 0)
            {
                specialItemsString = specialItemsString.Remove(specialItemsString.Length - 2);
            }
            if (neededItemExists)
            {
                if (opt.Text.Contains("[ITEM_LIST]"))
                {
                    optionText = opt.Text.Replace("[ITEM_LIST]", specialItemsString);
                }
                else
                {
                    optionText = opt.Text;
                }
            } 
            else
            {
                optionText = "";
            }
        }
        else 
        {
            optionText = opt.Text;
        }
        if (opt.Text.Contains("[PLAYER_NAME]"))
        {
            optionText = opt.Text.Replace("[PLAYER_NAME]", PlayerData.playerName);
        }
        return optionText;
    }

    public IEnumerator Run()
    {
        int nodeId = 0;

        while (nodeId != -1)
        {
            if (!optionsCreated)
            {
                for (int i = 0; i < dia.Nodes.Count; i++)
                {
                    if (nodeId == dia.Nodes[i].NodeID)
                    {
                        DisplayNode(dia.Nodes[i]);
                    }
                }
                optionsCreated = true;
            }
            selectedOption = -2;
            while (selectedOption == -2)
            {
                yield return new WaitForSeconds(0.25f);
            }
            nodeId = selectedOption;
        }
    }
}
