using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Tutorial
{
    public List<TutorialEvent> steps;
    public int currentStep;

    protected abstract void AddSteps();

    public abstract void CompleteStep();

    public abstract int GetCurrentStep();

    public abstract KeyCode GetKeyCode();

    public abstract string GetText();

    public abstract string GetObjectTag(); 

    public abstract TutorialActionState GetStep(int step);
}
