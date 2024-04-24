using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    public PointerState state = PointerState.RIGHT;

    #region References
        [SerializeField] RectTransform rectTransform;
        [SerializeField] TutorialHandler tutorialHandler;
        [SerializeField] UIHandler uiHandler;
    #endregion

    void Awake()
    {
        Debug.Log(state);
    }

    public void SetState(PointerState pointerState)
    {
        switch(pointerState)
        {
            case(PointerState.RIGHT):
                rectTransform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case(PointerState.UP):
                rectTransform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case(PointerState.LEFT):
                rectTransform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case(PointerState.DOWN):
                break;
                rectTransform.rotation = Quaternion.Euler(0, 0, 270);
            default:
                break;
        }
    }
}
