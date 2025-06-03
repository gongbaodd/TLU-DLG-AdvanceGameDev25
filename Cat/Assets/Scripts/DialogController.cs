using UnityEngine;
using Ink.Runtime;
using Assets.Prefabs.Cat.Scripts;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Button = UnityEngine.UIElements.Button;
using UnityEngine.Events;

public class DialogController : MonoBehaviour
{
    [SerializeField] GameObject god;
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    [SerializeField] bool hideSpeaker = false;
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

    public bool IsGaming
    {
        get
        {
            bool isGaming = story.variablesState["isGaming"] as bool? ?? false;
            return isGaming;
        }
    }
    bool IsGotoDiablo
    {
        get => story.variablesState["is_goto_diablo"] as bool? ?? false;
    }
    bool IsFruitNinjaDone
    {
        get => story.variablesState["is_fruit_ninja_done"] as bool? ?? false;
    }

    void SetupCat()
    {
        if (hideSpeaker) return;

        player.SetActive(true);
        var catCtrl = player.GetComponentInChildren<CatController>();
        catCtrl.ChooseAnimationLayer(CatController.AnimationLayer.FIGHT);
    }
    public void Win()
    {
        if (!IsGaming) return;
        story.ChooseChoiceIndex(0);
    }
    public void Lose()
    {
        if (!IsGaming) return;
        story.ChooseChoiceIndex(1);
    }

    string currentText = "";

    [SerializeField] UnityEvent OnNextScene;
    [SerializeField] UnityEvent OnStartGame;
    void RenderStory()
    {
        var sceneManager = SceneManagerController.Instance.GetComponent<SceneManagerController>();

        // Check if we need to go to the Fruit Ninja game scene
        if (IsGotoDiablo)
        {
            sceneManager.GotoDiabloGameScene();
            return;
        }

        if (!story.canContinue)
        {
            OnNextScene?.Invoke();
            return;
            // if (!IsFruitNinjaDone)
            // {
            //     OnNextScene?.Invoke();
            //     return;
            // }
            // else
            // {
            //     sceneManager.GotoDiabloGameScene();
            //     return;

            // }
        }

        currentText = story.Continue();

        if (IsGaming)
        {
            OnStartGame?.Invoke();
        }

        if (story.currentChoices.Count == 0)
        {
            RenderStory();
        }

        RenderContent();
        RenderSpeaker();
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
    void RenderSpeaker()
    {
        if (hideSpeaker) return;

        god.SetActive(CurrentSpeaker == Speaker.God);
        boss.SetActive(CurrentSpeaker == Speaker.Boss);
    }

    void ToggleDialogByState(LevelStateController.State state)
    {
        switch (state)
        {
            case LevelStateController.State.Story:
                OpenStory();
                break;
            case LevelStateController.State.Game:
                dialog.SetActive(false);
                break;
        }
    }

    void OpenStory()
    {
        dialog.SetActive(true);
        InitUI();
        SetupCat();
        RenderStory();
    }
    void Awake()
    {
        god.SetActive(false);
        player.SetActive(false);
        boss.SetActive(false);

        story = new(storyJsonAsset.text);
    }
    void Start()
    {
        OpenStory();
        LevelStateController.OnStateChange += ToggleDialogByState;
    }

    void OnDestroy()
    {
        LevelStateController.OnStateChange -= ToggleDialogByState;
    }
}
