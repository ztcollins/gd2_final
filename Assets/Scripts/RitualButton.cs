using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RitualButton : MonoBehaviour
{
    RitualButtonState state = RitualButtonState.UNCLICKED;

    #region References
        [SerializeField] CandleGameManager candleGameManager;
    #endregion
    
    public void ClickIncrement()
    {
        switch(state)
        {
            case(RitualButtonState.UNCLICKED):
                candleGameManager.CheckCandles();
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/UI/beginRitualOn");
                state = RitualButtonState.CONFIRM;
                break;
            case(RitualButtonState.CONFIRM):
                candleGameManager.CheckCandles();
                GameObject.FindWithTag("SceneHandler").GetComponent<SceneHandler>().UseInstruction(SceneHandlerInstruction.FINISHORDER, "");
                break;
            default:
                Debug.Log("INVALID STATE IN RITUALBUTTON");
                break;
        }
    }
}

