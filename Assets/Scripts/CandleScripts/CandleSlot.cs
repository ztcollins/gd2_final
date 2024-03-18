using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CandleSlot : MonoBehaviour, IDropHandler
{
    private bool hasCandle;

    void Start()
    {
        hasCandle = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        Debug.Log(eventData.pointerDrag.name);
        
        if(eventData.pointerDrag != null && eventData.pointerDrag.tag != "CandleGenerator") {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            hasCandle = true;
        }
    }

    public bool HasCandle()
    {
        return hasCandle;
    }
}
