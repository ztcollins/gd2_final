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
    public GameObject candleHolders;
    private Order currentOrder;
    private string currentSize;
    private string currentColor;
    private string currentType;

    // Start is called before the first frame update
    void Start()
    {
        // bring in order from last scene
        currentOrder = GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().GetCurrentOrder();
        GameObject newOrder = Instantiate(prefabOrder, new Vector2(0, 0), Quaternion.identity);
        currentOrder.SetNewOrderObject(newOrder);
        currentOrder.visualizeOrder();
        newOrder.transform.SetParent(ordersObject.transform);
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

        SetDemonStrings(innerCandleCode, middleCandleCode, outerCandleCode);

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

}
