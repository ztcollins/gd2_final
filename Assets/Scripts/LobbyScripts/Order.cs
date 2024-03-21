using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Order : MonoBehaviour
{
    private Customer associatedCustomer;
    private GameObject orderObject;
    private String color;
    private String size;
    private String type;
    private float orderValue;
    private bool isCurrentOrder;
    
    public void Initialize(Customer customer, GameObject order)
    {
        associatedCustomer = customer;
        orderObject = order;
        isCurrentOrder = false;

        String[] colors = {"red", "green", "blue"};
        String[] sizes = {"small", "medium", "large"};
        String[] types = {"humanoid", "worm", "imp"};

        int colorChoice = Random.Range(0,3);
        int sizeChoice = Random.Range(0,3);
        int typeChoice = Random.Range(0,3);
        color = colors[colorChoice];
        size = sizes[sizeChoice];
        type = types[typeChoice];

        orderValue = Random.Range(1.00f, 3.00f);
    }

    public void visualizeOrder() {
        TMP_Text[] textArray = orderObject.GetComponentsInChildren<TMP_Text>();
        foreach(var text in textArray) {
            if(text.name == "OrderType")
            {
                text.text = type;
            }
            else if(text.name == "OrderColor")
            {
                text.text = color;
            }
            else if(text.name == "OrderSize")
            {
                text.text = size;
            }
        }
    }

    public void SetNewOrder(Order orderToCopy, GameObject newOrderObj)
    {
        this.associatedCustomer = orderToCopy.associatedCustomer;
        this.orderObject = newOrderObj;
        this.color = orderToCopy.color;
        this.type = orderToCopy.type;
        this.size = orderToCopy.size;
        this.orderValue = orderToCopy.orderValue;
        this.isCurrentOrder = orderToCopy.isCurrentOrder;
    }

    public GameObject GetGameObject()
    {
        return orderObject;
    }

    public Customer GetAssociatedCustomer()
    {
        return associatedCustomer;
    }

    public String GetOrderColor()
    {
        return color;
    }

    public String GetOrderSize()
    {
        return size;
    }

    public String GetOrderType()
    {
        return type;
    }

    public float GetOrderValue()
    {
        return orderValue;
    }

    public bool IsCurrentOrder()
    {
        return isCurrentOrder;
    }

    public void SetCurrentOrder()
    {
        isCurrentOrder = true;
    }

    public void SetNewOrderObject(GameObject newOrderObject)
    {
        orderObject = newOrderObject;
    }
}
