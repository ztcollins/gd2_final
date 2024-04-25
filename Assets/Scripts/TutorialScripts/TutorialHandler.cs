using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    Tutorial currentTutorial;
    public bool hasStep = false;
    public TutorialActionState action;

    #region References
        [SerializeField] Pointer pointer;
        [SerializeField] RectTransform rectTransform;
        [SerializeField] GameObject overlayObj;
        [SerializeField] DialogueHandler dialogueHandler;
    #endregion

    // void Update() future functionality?
    // {
    //     CheckTutorials();
    // }

    void Awake()
    {
        overlayObj.SetActive(false);
        StartTutorial();
    }

    void Update()
    {
        CheckTutorials();
        EvaluateCondition(action);
    }

    //public void StartTutorial(Tutorial tutorial) 
    public void StartTutorial()
    {
        currentTutorial = new IntroTutorial();
    }

    public void CheckTutorials()
    {
        if(currentTutorial == null || hasStep) return;
        int step = currentTutorial.GetCurrentStep();
        action = currentTutorial.GetStep(step);

        switch(action)
        {
            case(TutorialActionState.ONCLICK):
                DisplayDialogue();
                DisplayArrow();
                ShowOverlay();
                hasStep = true;
                break;
            case(TutorialActionState.ONKEY):
                DisplayDialogue();
                DisplayArrow();
                ShowOverlay();
                hasStep = true;
                break;
            case(TutorialActionState.COMPLETE):
                hasStep = false;
                currentTutorial = null;
                break;
        }
    }

    private void DisplayDialogue()
    {
        dialogueHandler.SetDialogue(currentTutorial.GetText());
    }

    private void DisplayArrow()
    {
        if(currentTutorial.GetObjectTag() == null) return;
        Debug.Log(currentTutorial.GetObjectTag());
        Debug.Log("OBJ, " + GameObject.FindWithTag(currentTutorial.GetObjectTag()) + ", ?");
        AnimateArrow(PointerAnimationState.UNDULATE, GameObject.FindWithTag(currentTutorial.GetObjectTag()));
    }

    private void ShowOverlay()
    {
        overlayObj.SetActive(true);
    }

    private void EvaluateCondition(TutorialActionState action)
    {
        if(action == null) return;
        switch(action)
        {
            case(TutorialActionState.ONCLICK):
                if(Input.GetMouseButtonDown(0)) ProgressTutorial();
                break;
            case(TutorialActionState.ONKEY):
                if(Input.GetKeyDown(currentTutorial.GetKeyCode())) ProgressTutorial();
                break;
            default:
                break;
        }
    }

    private void ProgressTutorial()
    {
        overlayObj.SetActive(false);
        currentTutorial.CompleteStep();
        Debug.Log("done");
        hasStep = false;
    }



    public void AnimateArrow(PointerAnimationState animation, GameObject toPointObject)
    {
        PointArrow(toPointObject);
        pointer.StartAnimation(animation);
    }

    public void AnimateArrow(PointerAnimationState animation, GameObject toPointObject, PointerRotationState rotation)
    {
        PointArrow(toPointObject, rotation);
        pointer.StartAnimation(animation);
    }

    public void PointArrow(GameObject toPointObject)
    {
        PointArrow(toPointObject, CalculateRotation(toPointObject));

    }

    public void PointArrow(GameObject toPointObject, PointerRotationState direction)
    {
        RectTransform objectRectTransform = toPointObject.GetComponent<RectTransform>();

        float objWidth = objectRectTransform.rect.width;
        float objHeight = objectRectTransform.rect.height;

        Vector2 modifiedVector;

        switch(direction)
        {
            case(PointerRotationState.RIGHT):
                modifiedVector = new Vector2(-(objWidth / 2), 0);
                break;
            case(PointerRotationState.UP):
                modifiedVector = new Vector2(0 , -(objHeight / 2));
                break;
            case(PointerRotationState.LEFT):
                modifiedVector = new Vector2((objWidth / 2), 0);
                break;
            case(PointerRotationState.DOWN):
                modifiedVector = new Vector2(0, (objHeight / 2));
                break;
            default:
                modifiedVector = Vector2.zero;
                break;
        }

        Vector2 worldCoord = objectRectTransform.TransformPoint(modifiedVector);
        Vector2 canvasCoord = pointer.rectTransform.InverseTransformPoint(worldCoord);

        pointer.SetPosition(canvasCoord);
        pointer.SetRotation(direction);
    }

    private PointerRotationState CalculateRotation(GameObject toPointAt)
    {
        PointerRotationState returnState = PointerRotationState.RIGHT;
        int width = Screen.width;
        int height = Screen.height;

        RectTransform toPointRectTransform = toPointAt.GetComponent<RectTransform>();
        Vector2 worldCoord = toPointRectTransform.TransformPoint(toPointRectTransform.rect.position);

        bool right = worldCoord.x > width / 2 ? true : false; // FIND QUADRANT
        bool top = worldCoord.y > height / 2 ? true : false;

        if(right && top)
        {
            if(((Math.Abs((width / 2) - worldCoord.x)) / (width / 2)) > ((Math.Abs((height / 2) - worldCoord.y)) / (height / 2))) // WIDTH is more extreme
            {
                returnState = PointerRotationState.RIGHT;
            }
            else returnState = PointerRotationState.UP;
        }
        else if (right && !top)
        {
            if(((Math.Abs((width / 2) - worldCoord.x)) / (width / 2)) > ((Math.Abs((height / 2) - worldCoord.y)) / (height / 2))) // WIDTH is more extreme
            {
                returnState = PointerRotationState.RIGHT;
            }
            else returnState = PointerRotationState.DOWN;
        }
        else if (!right && top)
        {
            if(((Math.Abs((width / 2) - worldCoord.x)) / (width / 2)) > ((Math.Abs((height / 2) - worldCoord.y)) / (height / 2))) // WIDTH is more extreme
            {
                returnState = PointerRotationState.LEFT;
            }
            else returnState = PointerRotationState.UP;
        }
        else if (!right && !top)
        {
            if(((Math.Abs((width / 2) - worldCoord.x)) / (width / 2)) > ((Math.Abs((height / 2) - worldCoord.y)) / (height / 2))) // WIDTH is more extreme
            {
                returnState = PointerRotationState.LEFT;
            }
            else returnState = PointerRotationState.DOWN;
        }

        return returnState;
    }

    public void StopArrow()
    {
        pointer.StopAnimation();
    }

}
