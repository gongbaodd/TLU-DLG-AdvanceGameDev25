using UnityEngine;
using Ink.Runtime;
using Assets.Prefabs.Cat.Scripts;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Button = UnityEngine.UIElements.Button;
using UnityEngine.Events;

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

        enum Speaker { God, Boss }
        Speaker CurrentSpeaker
        {
            get
            {
                string speaker = story.variablesState["speaker"] as string;

                if (speaker == "god") return Speaker.God;
                else return Speaker.Boss;
            }
        }

        string currentText = "";

        [SerializeField] UnityEvent OnNextScene;
        bool IsGotoFruitNinja
        {
            get => story.variablesState["is_goto_fruit_ninja"] as bool? ?? false;
        }

        bool IsGotoDiablo
        {
            get => story.variablesState["is_goto_diablo"] as bool? ?? false;
        }
        void RenderStory()
        {

            var sceneManager = SceneManagerController.Instance.GetComponent<SceneManagerController>();

            story.variablesState["is_fruit_ninja_done"] = IsFruitNinjaDone;
            story.variablesState["is_diablo_done"] = IsDiabloDone;

            currentText = story.Continue();

  

            if (IsGotoFruitNinja)
            {
                sceneManager.GotoFruitNinjaGameScene();
                return;
            }

            if (IsGotoDiablo)
            {
                sceneManager.GotoDiabloGameScene();
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
            contentContainer.RemoveFromClassList("evil-content");

            if (CurrentSpeaker == Speaker.God)
            {
                contentContainer.AddToClassList("god-content");
            }
            else if (CurrentSpeaker == Speaker.Boss)
            {
                contentContainer.AddToClassList("evil-content");
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

        bool IsFruitNinjaDone
        {
            get
            {
                var items = Inventory.instance.GetItems();
                return items.Exists(x => x.name == "Fruit Memory");
            }
        }
        bool IsDiabloDone
        {
            get
            {
                var items = Inventory.instance.GetItems();
                return items.Exists(x => x.name == "Diablo Memory");
            }
        }

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
