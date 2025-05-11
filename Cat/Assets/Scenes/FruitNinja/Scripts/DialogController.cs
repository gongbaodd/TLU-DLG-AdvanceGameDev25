using UnityEngine;
using Ink.Runtime;
using Assets.Prefabs.Cat.Scripts;
using UnityEngine.UIElements;

public class DialogController : MonoBehaviour
{
    [SerializeField] GameObject god;
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    [SerializeField] string storyJson;
    Story story;
    UIDocument ui;

    enum Speaker { God, Boss }
    Speaker CurrentSpeaker {
        get {
            string speaker = (string)story.variablesState["speaker"];

            if (speaker == "god") return Speaker.God;
            else return Speaker.Boss;
        }
    }

    void SetupCat() {
        player.SetActive(true);
        var catCtrl = player.GetComponentInChildren<CatController>();
        catCtrl.ChooseAnimationLayer(CatController.AnimationLayer.FIGHT);
    }

    void RenderStory() {
        var text = story.Continue();
        print(text);
        var root = ui.rootVisualElement;

        var content = root.Q<Label>("content");
        content.text = text;

        var choice1 = root.Q<Button>("choice1");
        var choice2 = root.Q<Button>("choice2");

        var currentChoices = story.currentChoices;

        if (currentChoices.Count == 1) {
            choice1.text = currentChoices[0].text;
            choice2.AddToClassList("hidden");
        } else {
            choice1.text = currentChoices[0].text;
            choice2.text = currentChoices[1].text;
            choice2.RemoveFromClassList("hidden");
        }
    }

    void Awake()
    {
        god.SetActive(false);
        player.SetActive(false);
        boss.SetActive(false);

        story = new(storyJson);

        ui = GetComponent<UIDocument>();
    }
    void Start()
    {
        SetupCat();
        RenderStory();
    }

    void Update()
    {
        god.SetActive(CurrentSpeaker == Speaker.God);
        boss.SetActive(CurrentSpeaker == Speaker.Boss);
    }
}
