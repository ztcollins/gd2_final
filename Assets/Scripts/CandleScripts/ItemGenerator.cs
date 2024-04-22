using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    
    [SerializeField] public TextMeshProUGUI itemCounter;
    public GameObject itemPrefab;
    private GameObject newItem;
    private Canvas canvas;
    private RectTransform rectTransform;
    private RectTransform newItemRectTransform;
    private Dictionary<string, int> items;
    private string itemToGenerate;
    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        items = GameObject.FindWithTag("ItemHandler").GetComponent<ItemHandler>().GetItems();
        //itemCounter.text = "x" + items["candles"].ToString();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        newItemRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;  
    }

    public void SetCanvas(Canvas newCanvas)
    {
        canvas = newCanvas;
    }

    public void SetItem(string itemToSet)
    {
        itemToGenerate = itemToSet;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Item staticItem = new Item();
        newItem = Instantiate(itemPrefab, rectTransform.position, Quaternion.identity);
        newItem.GetComponent<Image>().sprite = staticItem.GetSprite(itemToGenerate);
        newItem.GetComponent<EventDragItem>().SetItem(itemToGenerate);
        newItem.GetComponent<EventDragItem>().SetText(itemCounter);
        newItem.transform.SetParent(canvas.transform);
        newItemRectTransform = newItem.GetComponent<RectTransform>();

        CanvasGroup newItemCanvasGroup = newItemRectTransform.GetComponent<CanvasGroup>();
        newItemCanvasGroup.alpha = .6f;
        newItemCanvasGroup.blocksRaycasts = false;

        ExecuteEvents.Execute<IEndDragHandler>(this.gameObject, eventData, ExecuteEvents.endDragHandler);
        eventData.pointerDrag = newItem;
        ExecuteEvents.Execute<IBeginDragHandler>(newItem, eventData, ExecuteEvents.beginDragHandler);

        // handle candle count & update text
        GameObject.FindWithTag("ItemHandler").GetComponent<ItemHandler>().DecreaseGeneric(itemToGenerate);
        itemCounter.text = "x" + items[itemToGenerate].ToString();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CanvasGroup newItemCanvasGroup = newItemRectTransform.GetComponent<CanvasGroup>();
        newItemCanvasGroup.alpha = 1f;
        newItemCanvasGroup.blocksRaycasts = true;
    }
}
