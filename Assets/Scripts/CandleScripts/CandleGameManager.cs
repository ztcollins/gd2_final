using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

public class CandleGameManager : MonoBehaviour
{
    public GameObject prefabOrder;
    public GameObject ordersObject;
    public GameObject prefabItem;
    public GameObject itemsObject;
    public GameObject candleHolders;
    public Canvas canvas;

    private Order currentOrder;
    private string currentSize;
    private string currentColor;
    private string currentType;

    // Start is called before the first frame update
    void Start()
    {
        // bring in order from last scene
        currentOrder = GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().GetCurrentOrder();
        Debug.Log("DEBUG RISK VAL: " + currentOrder.GetRiskValue());
        GameObject newOrder = Instantiate(prefabOrder, new Vector2(0, 0), Quaternion.identity);
        currentOrder.SetNewOrderObject(newOrder);
        currentOrder.visualizeOrder();
        newOrder.transform.SetParent(ordersObject.transform, false);

        // handle items
        this.InitializeItems();
    }

    public void CheckCandles()
    {
        // 22 candles in total.
        // INDICES (inclusive):
        // 0-3 inner circle (4)
        // 4-9 middle circle (6)
        // 10-21 outer circle (12)
        CandleSlot[] candleSlots = candleHolders.GetComponentsInChildren<CandleSlot>();
        
        int[] allCandles = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
        int[] innerCandles;
        int[] middleCandles;
        int[] outerCandles;

        for(int i = 0; i < candleSlots.Length; i++)
        {
            if(candleSlots[i].HasCandle())
            {
                allCandles[i] = 1;
            }
        }

        innerCandles = allCandles[0..4];
        middleCandles = allCandles[4..10];
        outerCandles = allCandles[10..22];

        int innerCandleCode = CalculateCode(innerCandles);
        int middleCandleCode = CalculateCode(middleCandles);
        int outerCandleCode = CalculateCode(outerCandles);
    
        int specialDemon = SpecialDemonCheck(allCandles);

        SetDemonStrings(innerCandleCode, middleCandleCode, outerCandleCode);

        GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().SetCurrentDemon(currentSize, currentColor, currentType);
    }

    public void SetDemonStrings(int codeSize, int codeColor, int codeType)
    {
        // inner candle string
        var enumDisplayStatus = (SizeCodes)codeSize;
        currentSize = enumDisplayStatus.ToString().ToLower();
        bool containsInt = currentSize.All(char.IsDigit);
        if(containsInt)
        {
            currentSize = "invalid";
        }

        // middle candle string
        var enumDisplayStatus2 = (ColorCodes)codeColor;
        currentColor = enumDisplayStatus2.ToString().ToLower();
        containsInt = currentColor.All(char.IsDigit);
        if(containsInt)
        {
            currentColor = "invalid";
        }

        // outer candle string
        var enumDisplayStatus3 = (TypeCodes)codeType;
        currentType = enumDisplayStatus3.ToString().ToLower();
        containsInt = currentType.All(char.IsDigit);
        if(containsInt)
        {
            currentType = "invalid";
        }

        Debug.Log(currentSize);
        Debug.Log(currentColor);
        Debug.Log(currentType);
    }

    public int CalculateCode(int[] inputArray)
    {
        Array.Reverse(inputArray);
        int code = 0;
        for(int i = 0; i < inputArray.Length; i++)
        {
            if(inputArray[i] == 1)
            {
                code += (int) Math.Pow(2, i);
            }
        }
        return code;
    }

    public int SpecialDemonCheck(int[] inputArray)
    {
        Array.Reverse(inputArray);
        int code = 0;
        for(int i = 0; i < inputArray.Length; i++)
        {
            if(inputArray[i] == 1)
            {
                code += (int) Math.Pow(2, i);
            }
        }

        int[] specialCodes = (int[])Enum.GetValues(typeof(SpecialCodes));
        for(int i = 0; i < specialCodes.Length; i++)
        {
            if(code == specialCodes[i]) return specialCodes[i];
        }

        return 0;
    }

    public void InitializeItems()
    {

        var items = GameObject.FindWithTag("ItemHandler").GetComponent<ItemHandler>().GetItems();

        foreach(var itemKey in items.Keys)
        {
            if(itemKey != "candles")
            {
                int itemCount = items[itemKey];
                if(itemCount > 0)
                {
                    GameObject newItem = GameObject.Instantiate(prefabItem, new Vector2(0, 0), Quaternion.identity);
                    
                    // initialize item object
                    newItem.GetComponent<Item>().SetItem(itemKey, itemCount);
                    newItem.GetComponent<ItemGenerator>().SetCanvas(canvas);
                    newItem.GetComponent<ItemGenerator>().SetItem(itemKey);

                    // attach to items list
                    newItem.transform.SetParent(itemsObject.transform, false);
                }
            }
        }
    }

    public void UsePlacedItem()
    {
        string itemPlaced = GameObject.FindWithTag("ItemHolder").GetComponent<ItemSlot>().GetPlacedItem();
        
        if(itemPlaced != "")
        {
            GameObject.FindGameObjectWithTag("ItemHandler").GetComponent<ItemHandler>().DecreaseGeneric(itemPlaced);
            Debug.Log("using " + itemPlaced);
                        // item effects here (expand)
            if(itemPlaced == "cheap perfume")
            {
                GameObject.Find("CandleGameManager").GetComponent<CandleGameManager>().ChangeRisk(10, false);
            }

            if(itemPlaced == "holy water")
            {
                GameObject.Find("CandleGameManager").GetComponent<CandleGameManager>().ChangeRisk(100, false);
            }
        }
        GameObject.Destroy(GameObject.FindWithTag("ItemHolder"));
        
    }

    public void ChangeRisk(int amount, bool positive)
    {
        int currentRisk = currentOrder.GetRiskValue();
        if(positive)
        {
            if(currentRisk + amount >= 100)
            {
                currentOrder.SetRiskValue(100);
            }
            else
            {
                currentOrder.SetRiskValue(currentOrder.GetRiskValue() + amount);
            }

        }
        else
        {
            if(currentRisk - amount <= 0)
            {
                currentOrder.SetRiskValue(0);
            }
            else
            {
                currentOrder.SetRiskValue(currentOrder.GetRiskValue() - amount);
            }
            
        }
        currentOrder.RefreshRisk();
        
    }

}
