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
        steps.Add(new TutorialEvent(TutorialActionState.ONCLICK, "Welcome to Demonology!"));
        steps.Add(new TutorialEvent(TutorialActionState.ONCLICK, "In this game, you are an amateur demon summoner tasked with paying off their debts!"));
        steps.Add(new TutorialEvent(TutorialActionState.ONCLICK, "You need to accumulate ten thousand dollars before 30 days have passed"));
        steps.Add(new TutorialEvent(TutorialActionState.ONCLICK,  "DebtButton", "Once you make money through summoning demons, you can pay off your debt by clicking the debt button"));
        steps.Add(new TutorialEvent(TutorialActionState.ONCLICK, "As you summon demons, your reputation will increase, which will garner you more money per demon summoned."));
        steps.Add(new TutorialEvent(TutorialActionState.ONCLICK, "You can buy items here to enhance your summons and upgrade your character"));
        steps.Add(new TutorialEvent(TutorialActionState.ONCLICK, "And if you need help, press the arrow button at the bottom of the screen to pull up the Necronomicon, which has details about the kinds of demons!"));
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