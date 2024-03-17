using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleOrderManager : MonoBehaviour
{
    private Order currentOrder;
    private bool isOrderComplete;
    public static CandleOrderManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Found more than one Candle Order Manager in the scene.");
        }
        instance = this;
    }

    public void InitializeCandleMinigame(Order order)
    {
        currentOrder = order;
        isOrderComplete = false;
    }

    public Order GetCurrentOrder()
    {
        return currentOrder;
    }

    public bool IsOrderComplete()
    {
        return isOrderComplete;
    }

    public void SetOrderComplete()
    {
        isOrderComplete = true;
    }
}
