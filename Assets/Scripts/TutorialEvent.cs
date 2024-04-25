using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvent : MonoBehaviour
{
    public KeyCode key;
    public TutorialActionState action;
    public string objectTag;
    public string text = "";

    public TutorialEvent(KeyCode key, string text)
    {
        action = TutorialActionState.ONKEY;
        this.key = key;
        this.text = text;
    }

    public TutorialEvent(string objectTag, string text)
    {
        action = TutorialActionState.ONCLICK;
        this.objectTag = objectTag;
        this.text = text;
    }

    public TutorialEvent(KeyCode key, string objectTag, string text)
    {
        action = TutorialActionState.ONKEY;
        this.objectTag = objectTag;
        this.key = key;
        this.text = text;
    }

    public TutorialEvent(TutorialActionState action)
    {
        this.action = action;
    }

    public TutorialEvent()
    {
        action = TutorialActionState.ONCLICK;
        text = "TODO";
    }

    public TutorialEvent(TutorialActionState action, KeyCode key, string objectTag, string text)
    {
        this.action = action;
        this.key = key;
        this.objectTag = objectTag;
        this.text = text;
    }

    public TutorialEvent(TutorialActionState action, KeyCode key, string text)
    {
        this.action = action;
        this.key = key;
        this.text = text;
    }

    public TutorialEvent(TutorialActionState action, string text)
    {
        this.action = action;
        this.text = text;
    }

    public void SetText(string text)
    {
        this.text = text;
    }
}
