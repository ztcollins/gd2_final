using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    public PointerRotationState rotationState = PointerRotationState.RIGHT;

    private Coroutine animationCoroutine;
    bool isAnimating = false;


    #region References
        [SerializeField] public RectTransform rectTransform;
        [SerializeField] TutorialHandler tutorialHandler;
        [SerializeField] UIHandler uiHandler;
        [SerializeField] AnimationCurve curve;
    #endregion

    void Awake()
    {
        
    }

    public void SetRotation(PointerRotationState rotation)
    {
        switch(rotation)
        {
            case(PointerRotationState.RIGHT):
                rectTransform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case(PointerRotationState.UP):
                rectTransform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case(PointerRotationState.LEFT):
                rectTransform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case(PointerRotationState.DOWN):
                rectTransform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            default:
                break;
        }
        rotationState = rotation;
    }

    public void SetPosition(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }

    public void StartAnimation(PointerAnimationState animation)
    {
        if(isAnimating) return;
        switch(animation)
        {
            case(PointerAnimationState.UNDULATE):
                isAnimating = true;
                animationCoroutine = StartCoroutine(UndulateAnimation());
                break;
            default:
                break;
        }
    }

    public void StopAnimation()
    {
        if(!isAnimating) return;
        StopCoroutine(animationCoroutine);
        isAnimating = false;
    }

    IEnumerator UndulateAnimation()
    {
        bool movingRight = true;
        float duration = 1.0f;
        float distance = 75f;
        float distanceX = 0f;
        float distanceY = 0f;
        Vector3 start = rectTransform.localPosition;

        switch(rotationState)
        {
            case(PointerRotationState.RIGHT):
                distanceX = -distance;
                distanceY = 0;
                break;
            case(PointerRotationState.UP):
                distanceX = 0;
                distanceY = -distance;
                break;
            case(PointerRotationState.LEFT):
                distanceX = distance;
                distanceY = 0;
                break;
            case(PointerRotationState.DOWN):
                distanceX = 0;
                distanceY = distance;
                break;
            default:
                distanceX = -distance;
                distanceY = 0;
                break;
        }
        Vector3 end = start + new Vector3(distanceX, distanceY, 0f);
        
        while(true)
        {
            float time = 0f;

            while(time < 1f)
            {
                Debug.Log(start + ", " + end);
                time += Time.deltaTime / duration;
                float ease = curve.Evaluate(time);
                rectTransform.localPosition = movingRight ? Vector3.Lerp(start, end, ease) : Vector3.Lerp(end, start, ease);
                yield return null;
            }

            movingRight = !movingRight;
            yield return null;
        }
    }
}
