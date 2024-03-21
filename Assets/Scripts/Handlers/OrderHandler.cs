using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderHandler : MonoBehaviour
{
    private Order currentOrder;
    private bool isOrderComplete;
    public static OrderHandler instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Found more than one Order Manager in the scene.");
        }
        instance = this;
    }

    public void InitializeCandleMinigame(Order order)
    {
        currentOrder = order;
        currentOrder.SetCurrentOrder();
        isOrderComplete = false;
    }

    public void SetCurrentOrder(Order order)
    {
        currentOrder = order;
    }

    public Order GetCurrentOrder()
    {
        return currentOrder;
    }

    public bool IsOrderComplete()
    {
        return isOrderComplete;
    }

    public void SetOrderComplete(bool isComplete)
    {
        isOrderComplete = isComplete;
    }
}
