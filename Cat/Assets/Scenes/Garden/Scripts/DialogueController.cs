using UnityEngine;
using Ink.Runtime;
using Assets.Prefabs.Cat.Scripts;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Button = UnityEngine.UIElements.Button;
using UnityEngine.Events;
using System.Linq;

namespace Assets.Scenes.Garden.Scripts
{

    public class DialogueController : MonoBehaviour
    {
        [SerializeField] GameObject dialog;
        [SerializeField] TextAsset storyJsonAsset;
        Story story;
        UIDocument ui;
        Label contentLabel;
        VisualElement contentContainer;
        readonly List<Button> choices = new();

        enum Speaker { God }
        Speaker CurrentSpeaker
        {
            get
            {
                // Check if the speaker variable is set in the story
                if (story.variablesState.Contains("speaker"))
                {
                    // If the speaker variable is present, use it
                    string speaker = story.variablesState["speaker"] as string;
                    if (speaker == "god") return Speaker.God;
                }
                return Speaker.God;
            }
        }

        string currentText = "";

        bool IsGotoFruitNinja
        {
            get => story.variablesState["is_goto_fruit_ninja"] as bool? ?? false;
        }
        bool IsTutorialDone
        {
            get => story.variablesState["is_tutorial_done"] as bool? ?? false;
        }

        void Awake()
        {
            dialog.SetActive(false); // Initially hide the dialog
        }
        void Start()
        {
            GameManager.OnOpenStory += OpenStory; // Subscribe to the event to open the story dialog
        }


        public void RenderStory()
        {
            var sceneManager = SceneManagerController.Instance.GetComponent<SceneManagerController>();

            // Check if we need to go to the Fruit Ninja game scene
            if (IsGotoFruitNinja)
            {

                sceneManager.GotoFruitNinjaGameScene();
                return;
            }
            // if (IsTutorialDone)
            // {
            //     sceneManager.GotoGardenScene();
            //     return;
            // }

            currentText = story.Continue(); // Get the next line of text from the story



            if (story.currentChoices.Count == 0)
            {
                RenderStory(); // Continue rendering until there are no choices left
            }

            RenderContent(); // Update the content label with the current text
            RenderChoices(); // Update the choices based on the current state of the story
        }

        void RenderContent()
        {
            contentLabel.text = currentText;

            contentContainer.RemoveFromClassList("god-content"); // Remove any previous speaker styles

            if (CurrentSpeaker == Speaker.God)
            {
                contentContainer.AddToClassList("god-content");
            }
        }

        void RenderChoices()
        {
            for (int i = 0; i < choices.Count; i++)
            {
                choices[i].AddToClassList("hidden"); // Hide all choices initially
            }

            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                choices[i].text = story.currentChoices[i].text;
                choices[i].RemoveFromClassList("hidden");
            }
        }

        // Event callback for when a choice is selected
        EventCallback<ClickEvent> OnChoiceSelected(int index)
        {
            return evt =>
            {
                story.ChooseChoiceIndex(index);
                RenderStory();
            };
        }

        void InitUI()
        {
            ui = dialog.GetComponent<UIDocument>();
            var root = ui.rootVisualElement; // Get the root visual element of the UI document

            contentLabel = root.Q<Label>("content");
            contentContainer = root.Q<VisualElement>("container");

            choices.Clear();
            choices.Add(root.Q<Button>("choice1"));
            choices.Add(root.Q<Button>("choice2"));

            for (int i = 0; i < choices.Count; i++)
            {
                choices[i].RegisterCallback(OnChoiceSelected(i)); // Register the callback for each choice button
            }
        }

        void OpenStory()
        {
            story = new(storyJsonAsset.text); // Load the story from the JSON asset

            dialog.SetActive(true); // Show the dialog
            InitUI();
            RenderStory();
        }


        void OnDestroy()
        {
            GameManager.OnOpenStory -= OpenStory; // Unsubscribe from the event to avoid memory leaks
        }
    }

}
