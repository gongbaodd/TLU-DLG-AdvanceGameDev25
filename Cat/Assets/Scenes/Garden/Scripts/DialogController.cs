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

    public class DialogController : MonoBehaviour
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


        void RenderStory()
        {

            var sceneManager = SceneManagerController.Instance.GetComponent<SceneManagerController>();

            currentText = story.Continue();

            if (IsGotoFruitNinja)
            {
                sceneManager.GotoFruitNinjaGameScene();
                return;
            }

            if (story.currentChoices.Count == 0)
            {
                RenderStory();
            }

            RenderContent();
            RenderChoices();
        }

        void RenderContent()
        {
            contentLabel.text = currentText;

            contentContainer.RemoveFromClassList("god-content");

            if (CurrentSpeaker == Speaker.God)
            {
                contentContainer.AddToClassList("god-content");
            }
        }

        void RenderChoices()
        {
            for (int i = 0; i < choices.Count; i++)
            {
                choices[i].AddToClassList("hidden");
            }

            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                choices[i].text = story.currentChoices[i].text;
                choices[i].RemoveFromClassList("hidden");
            }
        }

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
            var root = ui.rootVisualElement;

            contentLabel = root.Q<Label>("content");
            contentContainer = root.Q<VisualElement>("container");

            choices.Clear();
            choices.Add(root.Q<Button>("choice1"));
            choices.Add(root.Q<Button>("choice2"));

            for (int i = 0; i < choices.Count; i++)
            {
                choices[i].RegisterCallback(OnChoiceSelected(i));
            }
        }

        // bool IsFruitNinjaDone
        // {
        //     get
        //     {
        //         var items = Inventory.instance.GetItems();
        //         return items.Exists(x => x.name == "Fruit Memory");
        //     }
        // }
        // bool IsDiabloDone
        // {
        //     get
        //     {
        //         var items = Inventory.instance.GetItems();
        //         return items.Exists(x => x.name == "Diablo Memory");
        //     }
        // }

        void OpenStory()
        {
            story = new(storyJsonAsset.text);

            dialog.SetActive(true);
            InitUI();
            RenderStory();
        }
        void Awake()
        {
            dialog.SetActive(false);
        }
        void Start()
        {
            GameManager.OnOpenStory += OpenStory;
        }

        void OnDestroy()
        {
            GameManager.OnOpenStory -= OpenStory;
        }
    }

}
