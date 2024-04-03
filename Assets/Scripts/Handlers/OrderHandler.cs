using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderHandler : MonoBehaviour
{
    public Order currentOrder;

    public GameObject persistentParent;
    private bool isOrderComplete;
    private string size;
    private string color;
    private string type;
    private bool isDemonSet;
    public static OrderHandler instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Found more than one Order Manager in the scene.");
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.size = "invalid";
        this.color = "invalid";
        this.type = "invalid";
        this.isDemonSet = false;
    }

    public void InitializeCandleMinigame(Order order)
    {
        order.isCurrentOrder = true;
        SetCurrentOrder(order);
        isOrderComplete = false;
    }

    public void SetCurrentOrder(Order order)
    {
        currentOrder = order;
        Debug.Log("SET CURRENT ORDER");
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

    public void SetCurrentDemon(string size, string color, string type)
    {
        this.size = size;
        this.color = color;
        this.type = type;
        isDemonSet = true;
    }

    public string GetCurrentDemonSize()
    {
        return this.size;
    }

    public string GetCurrentDemonColor()
    {
        return this.color;
    }

    public string GetCurrentDemonType()
    {
        return this.type;
    }

    public bool IsDemonSet()
    {
        return isDemonSet;
    }
}
