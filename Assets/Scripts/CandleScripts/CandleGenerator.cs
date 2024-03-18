using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CandleGenerator : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    public GameObject candlePrefab;
    private GameObject newCandle;

    private RectTransform rectTransform;
    private RectTransform newCandleRectTransform;
    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CanvasGroup newCandleCanvasGroup = newCandleRectTransform.GetComponent<CanvasGroup>();
        newCandleCanvasGroup.alpha = 1f;
        newCandleCanvasGroup.blocksRaycasts = true;
    }
}
