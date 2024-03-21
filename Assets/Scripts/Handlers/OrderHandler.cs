using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderHandler : MonoBehaviour
{
    private Order currentOrder;
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
<<<<<<< HEAD
        DontDestroyOnLoad(this.gameObject);

        this.size = "invalid";
        this.color = "invalid";
        this.type = "invalid";
        this.isDemonSet = false;
=======
>>>>>>> book
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
