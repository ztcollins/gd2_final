using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CandleGenerator : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler
{
    [SerializeField] private Canvas canvas;
    public GameObject candlePrefab;
    private GameObject newCandle;

    private RectTransform rectTransform;
    private RectTransform newCandleRectTransform;
    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //DragDrop newDrag = newCandle.GetComponent<DragDrop>();
        //newCandle.transform.SetSiblingIndex(0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        newCandleRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        newCandle = Instantiate(candlePrefab, rectTransform.position, Quaternion.identity);
        newCandle.transform.SetParent(canvas.transform);
        newCandleRectTransform = newCandle.GetComponent<RectTransform>();
    }
}
