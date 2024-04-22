using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
private bool hasItem;
private string placedItem;

    void Start()
    {
        hasItem = false;
        placedItem = "";
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null && eventData.pointerDrag.tag != "ItemGenerator") {
            eventData.pointerDrag.transform.SetParent(this.gameObject.transform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            hasItem = true;
            placedItem = eventData.pointerDrag.GetComponent<EventDragItem>().GetItem();
            Debug.Log(placedItem + " has been placed!");
        }
    }

    public bool HasItem()
    {
        return hasItem;
    }

    public void SetNoItem()
    {
        hasItem = false;
        placedItem = "";
        Debug.Log("item slot cleared.");
    }

    public string GetPlacedItem()
    {
        return placedItem;
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
