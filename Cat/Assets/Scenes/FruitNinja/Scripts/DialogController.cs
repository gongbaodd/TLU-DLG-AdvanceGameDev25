using UnityEngine;
using Ink.Runtime;
using Assets.Prefabs.Cat.Scripts;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Button = UnityEngine.UIElements.Button;
using Assets.Scenes.FruitNinja.Scripts;

public class DialogController : MonoBehaviour
{
    [SerializeField] GameObject god;
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject dialog;
    [SerializeField] string storyJson;
    Story story;
    UIDocument ui;
    Label contentLabel;
    readonly List<Button> choices = new();
    LevelManagerController manager;

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

    void SetupCat()
    {
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
    void RenderStory()
    {
        if (!story.canContinue)
        {
            return;
        }

        if (IsGaming)
        {
            manager.StartGame();
            return;
        }

        currentText = story.Continue();

        RenderContent();
        RenderSpeaker();
        RenderChoices();
    }

    void RenderContent()
    {
        contentLabel.text = currentText;

        contentLabel.RemoveFromClassList(".god-content");
        contentLabel.RemoveFromClassList(".evil-content");

        if (CurrentSpeaker == Speaker.God)
        {
            contentLabel.AddToClassList(".god-content");
        }
        else if (CurrentSpeaker == Speaker.Boss)
        {
            contentLabel.AddToClassList(".evil-content");
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
            choices[i].RegisterCallbackOnce(OnChoiceSelected(i));
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

    void RenderSpeaker()
    {
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
        SetupCat();
        RenderStory();
    }
    void Awake()
    {
        god.SetActive(false);
        player.SetActive(false);
        boss.SetActive(false);

        story = new(storyJson);
    }
    void Start()
    {
        manager = LevelManagerController.Instance;

        ui = dialog.GetComponent<UIDocument>();
        var root = ui.rootVisualElement;

        contentLabel = root.Q<Label>("content");

        choices.Add(root.Q<Button>("choice1"));
        choices.Add(root.Q<Button>("choice2"));

        OpenStory();

        LevelStateController.OnStateChange += ToggleDialogByState;
    }

    void OnDestroy()
    {
        LevelStateController.OnStateChange -= ToggleDialogByState;
    }
}
