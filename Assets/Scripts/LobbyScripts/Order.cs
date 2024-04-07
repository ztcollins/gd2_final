using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.Collections;
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
    private int moneyDisplay; // 1 is smallest, 6 is largest
    private int riskValue; // 0 to 100 %
    private bool isCurrentOrder;
    private bool isRiskyOrder;
    
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
        
        // calculate risk value & order value (can move this later)
        riskValue = 0;
        moneyDisplay = 1;
        orderValue = Random.Range(1.00f, 3.00f); // orders are 1 to 3 dollars to start
        if(color == "red")
        {
            riskValue += 10;
            moneyDisplay += 2;
            orderValue *= 1.5f;
        }
        if(type == "humanoid")
        {
            riskValue += 10;
            moneyDisplay += 2;
            orderValue *= 1.5f;
        }
        if(color == "green")
        {
            riskValue += 5;
            moneyDisplay += 1;
            orderValue *= 1.25f;
        }
        if(type == "imp")
        {
            riskValue += 5;
            moneyDisplay += 1;
            orderValue *= 1.25f;
        }
        if(size == "large")
        {
            riskValue += 5;
            moneyDisplay += 1;
            orderValue *= 1.25f;
        }

        // randomly get high risk, high rewards orders
        if(Random.Range(0, 20) == 5)
        {
            riskValue += 50;
            orderValue *= 2;
            isRiskyOrder = true;
        }
        else
        {
            isRiskyOrder = false;
        }
        
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
            else if(text.name == "OrderRisk")
            {
                text.text = riskValue.ToString() + "%";
                if(isRiskyOrder)
                {
                    text.color = Color.red;
                }
            }
            else if(text.name == "OrderPay")
            {
                for(int i = 1; i < moneyDisplay; i++)
                {
                    text.text += "$";
                }
                if(isRiskyOrder)
                {
                    text.color = Color.red;
                }
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
        this.moneyDisplay = orderToCopy.moneyDisplay;
        this.isRiskyOrder = orderToCopy.isRiskyOrder;
        this.riskValue = orderToCopy.riskValue;
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

    public int GetRiskValue()
    {
        return riskValue;
    }
}
