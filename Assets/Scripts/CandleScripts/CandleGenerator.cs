using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CandleGenerator : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] public TextMeshProUGUI candleCounter;
    public GameObject candlePrefab;
    private GameObject newCandle;
    private RectTransform rectTransform;
    private RectTransform newCandleRectTransform;
    private Dictionary<string, int> items;
    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        items = GameObject.FindWithTag("ItemHandler").GetComponent<ItemHandler>().GetItems();
        candleCounter.text = "x" + items["candles"].ToString();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        newCandleRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;  
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        newCandle = Instantiate(candlePrefab, rectTransform.position, Quaternion.identity);
        newCandle.transform.SetParent(canvas.transform);
        newCandleRectTransform = newCandle.GetComponent<RectTransform>();

        CanvasGroup newCandleCanvasGroup = newCandleRectTransform.GetComponent<CanvasGroup>();
        newCandleCanvasGroup.alpha = .6f;
        newCandleCanvasGroup.blocksRaycasts = false;

        ExecuteEvents.Execute<IEndDragHandler>(this.gameObject, eventData, ExecuteEvents.endDragHandler);
        eventData.pointerDrag = newCandle;
        ExecuteEvents.Execute<IBeginDragHandler>(newCandle, eventData, ExecuteEvents.beginDragHandler);

        // handle candle count & update text
        GameObject.FindWithTag("ItemHandler").GetComponent<ItemHandler>().DecreaseGeneric("candles");
        candleCounter.text = "x" + items["candles"].ToString();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CanvasGroup newCandleCanvasGroup = newCandleRectTransform.GetComponent<CanvasGroup>();
        newCandleCanvasGroup.alpha = 1f;
        newCandleCanvasGroup.blocksRaycasts = true;
    }
}
