using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTutorial : Tutorial
{
    public List<TutorialEvent> steps;
    public int currentStep;
    public IntroTutorial()
    {
        steps = new List<TutorialEvent>();
        currentStep = 0;
        AddSteps();
    }

    protected override void AddSteps()
    {
        steps.Add(new TutorialEvent(TutorialActionState.ONCLICK, "TEST!"));
        steps.Add(new TutorialEvent(TutorialActionState.ONKEY, KeyCode.N, "TEST KEY"));
        steps.Add(new TutorialEvent(TutorialActionState.ONKEY, KeyCode.W, "XPBAR", "DOES THE ANIMATION WORK?"));
        steps.Add(new TutorialEvent(TutorialActionState.COMPLETE));
    }

    public override void CompleteStep()
    {
        currentStep++;
    }

    public override string GetObjectTag()
    {
        return steps[currentStep].objectTag;
    }

    public override TutorialActionState GetStep(int index)
    {
        return steps[index].action;
    }

    public override KeyCode GetKeyCode()
    {
        return steps[currentStep].key;
    }

    public override string GetText()
    {
        return steps[currentStep].text;
    }

    public override int GetCurrentStep()
    {
        return currentStep;
    }
}