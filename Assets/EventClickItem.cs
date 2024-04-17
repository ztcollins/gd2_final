using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventClickItem : MonoBehaviour, IPointerClickHandler
{
    private GameObject thisItem;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("item clicked");
        thisItem = eventData.pointerEnter;
        GameObject.FindGameObjectWithTag("ItemHandler").GetComponent<ItemHandler>().HandleItemClicked(thisItem.GetComponent<Item>());
    }

}
