using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemHandler : MonoBehaviour, IDataPersistence
{
    private Dictionary<string, int> items;
    private Dictionary<string, bool> upgrades;
    public void ItemDebug()
    {
        Debug.Log("ITEM DEBUG: " + items["candles"]);
    }

    public void AddCandle()
    {
        items["candles"] += 1;
    }

    public void AddUpgrade(string upgrade)
    {
        upgrades[upgrade] = true;
    }

    public Dictionary<string, bool> GetUpgrades()
    {
        return upgrades;
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

    public void AddGeneric(string itemToAdd, int amount)
    {
        if(items.Keys.Contains(itemToAdd))
        {
            items[itemToAdd] += amount;
        }
        else
        {
            items.Add(itemToAdd, amount);
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

    public void SetUpgrades(Dictionary<string, bool> upgrades)
    {
        this.upgrades = upgrades;
    }

    public void LoadData(GameData data)
    {
        this.items = data.items;
        this.upgrades = data.upgrades;
    }

    public void SaveData(ref GameData data)
    {
        data.items = items;
        data.upgrades = upgrades;
    }

    public void HandleItemClicked(Item itemClicked)
    {
        itemClicked.Useitem();
        DecreaseGeneric(itemClicked.GetName());
    }

    public void EvaluateHubUpgrades()
    {
        Dictionary<string, bool> upgrades = GameObject.FindGameObjectWithTag("ItemHandler").GetComponent<ItemHandler>().GetUpgrades();
        float money = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().GetMoney();

        foreach(var upgradeName in upgrades.Keys)
        {
            if(upgrades[upgradeName])
            {
                // item effects here (expand)
                if(upgradeName == "bank interest")
                {
                    money += 5.00f;
                }

                if(upgradeName == "candle delivery")
                {

                    GameObject.FindWithTag("ItemHandler").GetComponent<ItemHandler>().AddGeneric("candles", 50);
                }
            }
        }

        GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().SetMoney(money);
    }

}
