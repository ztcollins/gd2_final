using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemHandler : MonoBehaviour, IDataPersistence
{
    private Dictionary<string, int> items;
    public void ItemDebug()
    {
        Debug.Log("ITEM DEBUG: " + items["candles"]);
    }

    public void AddCandle()
    {
        items["candles"] += 1;
    }

    public void AddGeneric(string itemToAdd)
    {
        if(items.Keys.Contains(itemToAdd))
        {
            items[itemToAdd] += 1;
        }
        else
        {
            items.Add(itemToAdd, 1);
        }
    }

    public void DecreaseGeneric(string itemToRemove)
    {
        if(items.Keys.Contains(itemToRemove) && items[itemToRemove] > 0)    
        {
            items[itemToRemove] -= 1;
        }
    }

    public Dictionary<string, int> GetItems()
    {
        return items;
    }

    public void SetItems(Dictionary<string, int> items)
    {
        this.items = items;
    }

    public void LoadData(GameData data)
    {
        this.items = data.items;
    }

    public void SaveData(ref GameData data)
    {
        data.items = items;
    }

    public void HandleItemClicked(Item itemClicked)
    {

    }

}
