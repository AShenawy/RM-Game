using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;


namespace Methodyca.Core
{
    // a base class for NPC stories created in Ink
    [RequireComponent(typeof(NPC))]
    public abstract class InkCharStory : MonoBehaviour
    {
        [SerializeField] protected TextAsset inkJSONAsset;
        [SerializeField] protected Canvas inkDialogueCanvasPrefab;
        [SerializeField] protected Text inkTextPrefab;
        [SerializeField] protected Button inkChoiceButtonPrefab;
        public Story inkStory;
        protected Canvas canvas;
        protected string savedJSONState;
        public static event System.Action<Story> OnBeginStory;
        public event System.Action OnEndStory;


        public virtual void StartStory()
        {
            inkStory = new Story(inkJSONAsset.text);
            OnBeginStory?.Invoke(inkStory);
            //inkStory.state.LoadJson(savedJSONState);
            canvas = Instantiate(inkDialogueCanvasPrefab);

            // check story variables before starting story
            CheckVariables();

            RefreshView();
        }

        // To be implemented by child classes. Some stories have variables
        // These variables determine which part of the story to start at
        protected abstract void CheckVariables();

        void RefreshView()
        {
            // Remove all the UI on screen
            RemoveChildren();

            // Read all the content until we can't continue any more
            while (inkStory.canContinue)
            {
                // Continue gets the next line of the story
                string text = inkStory.Continue();
                // This removes any white space from the text.
                text = text.Trim();
                // Display the text on screen!
                CreateContentView(text);
            }

            // Display all the choices, if there are any!
            if (inkStory.currentChoices.Count > 0)
            {
                for (int i = 0; i < inkStory.currentChoices.Count; i++)
                {
                    Choice choice = inkStory.currentChoices[i];
                    Button button = CreateChoiceView(choice.text.Trim());
                    // Tell the button what to do when we press it
                    button.onClick.AddListener(delegate {
                        OnClickChoiceButton(choice);
                    });
                }
            }
            // If we've read all the content and there's no choices, the story is finished!
            else
            {
                EndStory();
            }
        }

        // When we click the choice button, tell the story to choose that choice!
        void OnClickChoiceButton(Choice choice)
        {
            inkStory.ChooseChoiceIndex(choice.index);
            RefreshView();
        }

        // Creates a textbox showing the the line of text
        void CreateContentView(string text)
        {
            Text storyText = Instantiate(inkTextPrefab);
            storyText.text = text;
            storyText.transform.SetParent(canvas.transform, false);
        }

        // Creates a button showing the choice text
        Button CreateChoiceView(string text)
        {
            // Creates the button from a prefab
            Button choice = Instantiate(inkChoiceButtonPrefab);
            choice.transform.SetParent(canvas.transform, false);

            // Gets the text from the button prefab
            Text choiceText = choice.GetComponentInChildren<Text>();
            choiceText.text = text;

            // Make the button expand to fit the text
            HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
            layoutGroup.childForceExpandHeight = false;

            return choice;
        }

        // Destroys all the children of this gameobject (all the UI)
        void RemoveChildren()
        {
            int childCount = canvas.transform.childCount;
            for (int i = childCount - 1; i >= 0; --i)
            {
                Destroy(canvas.transform.GetChild(i).gameObject);
            }
        }

        protected virtual void EndStory()
        {
            // save conversation state
            //savedJSONState = inkStory.state.ToJson();
            OnEndStory?.Invoke();

            // destroy canvas and all children
            Destroy(canvas.gameObject);
        }
    }
}