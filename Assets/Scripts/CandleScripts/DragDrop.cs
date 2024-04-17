using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Dictionary<string, int> items;
    private TextMeshProUGUI candleCounter;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.Find("CandleGameCanvas").GetComponent<Canvas>();
        items = GameObject.FindWithTag("ItemHandler").GetComponent<ItemHandler>().GetItems();
        candleCounter = GameObject.Find("CandleCounter").GetComponent<TextMeshProUGUI>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

        List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, results);
        if(results.Count > 0)
        {
            GameObject currentlyHovered = results.First().gameObject;
            if(currentlyHovered.tag == "CandleHolder")
            {
                currentlyHovered.GetComponent<CandleSlot>().SetNoCandle();
            }
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, results);
        
        if(results.Count > 0)
        {
            GameObject currentlyHovered = results.First().gameObject;
            if(currentlyHovered.tag == "CandleHolder")
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
            }
            else
            {
                items["candles"] += 1;
                candleCounter.text = "x" + items["candles"];
                GameObject.Destroy(this.gameObject);
            }
        }
        else
        {
            items["candles"] += 1;
            candleCounter.text = "x" + items["candles"];
            GameObject.Destroy(this.gameObject);
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("im being dropped!");
    }
}
