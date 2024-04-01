using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CandleSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
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
            eventData.pointerDrag.transform.SetParent(this.gameObject.transform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            hasCandle = true;
        }
    }

    public bool HasCandle()
    {
        return hasCandle;
    }

    public void SetNoCandle()
    {
        hasCandle = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            GetComponent<Image>().color = Color.white;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.clear;
    }
}
