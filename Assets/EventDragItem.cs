using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject thisItem;
    private TextMeshProUGUI itemCounter;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Dictionary<string, int> items;
    private string itemString;
    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.Find("CandleGameCanvas").GetComponent<Canvas>();
        items = GameObject.FindWithTag("ItemHandler").GetComponent<ItemHandler>().GetItems();
    }

    public void SetItem(string itemString)
    {
        this.itemString = itemString;
    }

    public string GetItem()
    {
        return itemString;
    }

    public void SetText(TextMeshProUGUI textToSet)
    {
        itemCounter = textToSet;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("item clicked");
        //thisItem = eventData.pointerEnter;
        //GameObject.FindGameObjectWithTag("ItemHandler").GetComponent<ItemHandler>().HandleItemClicked(thisItem.GetComponent<Item>());
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
            if(currentlyHovered.tag == "ItemHolder")
            {
                currentlyHovered.GetComponent<ItemSlot>().SetNoItem();
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
            if(currentlyHovered.tag == "ItemHolder")
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
            }
            else
            {
                items[itemString] += 1;
                itemCounter.text = "x" + items[itemString];
                GameObject.Destroy(this.gameObject);
            }
        }
        else
        {
            items[itemString] += 1;
            itemCounter.text = "x" + items[itemString];
            GameObject.Destroy(this.gameObject);
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("im being dropped!");
    }

}
