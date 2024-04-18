using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.Linq;
using System.Collections.Generic;
public class Order : MonoBehaviour
{
    private Customer associatedCustomer;
    private GameObject orderObject;
    private String color;
    private String size;
    private String type;
    private float orderValue;
    private int xpEarned;
    private int moneyDisplay; // 1 is smallest, 6 is largest
    private int riskValue; // 0 to 100 %
    private bool isRiskyOrder;
    
    public void Initialize(Customer customer, GameObject order, OrderData orderData)
    {
        associatedCustomer = customer;
        orderObject = order;

        type = WeightedRandomSelection(orderData.type);
        color = WeightedRandomSelection(orderData.color);
        size = WeightedRandomSelection(orderData.size);

        // calculate risk value & order value (can move this later)
        riskValue = 0;
        moneyDisplay = 1;
        orderValue = Random.Range(1.00f, 3.00f); //base price
        xpEarned = Random.Range(2, 4); //base XP

        //trait calculus
        ParseType(type);
        ParseColor(color);
        ParseSize(size);

        //other calculus
        // randomly get high risk, high rewards orders
        if(Random.Range(0, 20) == 5)
        {
            riskValue += 50;
            orderValue *= 2;
            xpEarned *= 2;
            isRiskyOrder = true;
        }
        else
        {
            isRiskyOrder = false;
        }

        if(orderValue < 4.0f) moneyDisplay = 1;
        else if(orderValue < 8.0f) moneyDisplay = 2;
        else if(orderValue < 12.0f) moneyDisplay = 3;
        else if(orderValue < 18.0f) moneyDisplay = 4;
        else moneyDisplay = 5;

        if(riskValue > 100) riskValue = 100;

        // multiply XP by 'cost'
        xpEarned *= moneyDisplay;

        // add reputation level scalar
        orderValue *= GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().GetReputationLevel();
        
    }

    private string WeightedRandomSelection(List<Dictionary<string, float>> trait)
    {
        string[] traitNames = new string[trait.Count];
        float[] traitValues = new float[trait.Count];

        //convert dictionary into arrays
        for(int i = 0; i < trait.Count; i++)
        {
            foreach(var kvp in trait[i])
            {
                traitNames[i] = kvp.Key;
                traitValues[i] = kvp.Value;
            }
        }

        //calculate denominator (sum of values)
        float denominator = 0.0f;
        for(int i = 0; i < traitValues.Length; i++)
        {
            denominator += traitValues[i];
        }

        //normalize values
        for(int i = 0; i < traitValues.Length; i++)
        {
            traitValues[i] = traitValues[i] / denominator;
        }

        //make weighted random selection
        float random = Random.Range(0.0f, 1.0f);
        string selection = "";
        float current = 0.0f;
        for(int i = 0; i < traitValues.Length; i++)
        {
            current += traitValues[i];
            if(random < current || i == traitValues.Length - 1) 
            {
                selection = traitNames[i];
                break;
            }
        }

        return selection;
    }

    private void ParseType(string type)
    {
        switch(type)
        {
            case("imp"):
                riskValue += 0;
                orderValue += 0.5f;
                xpEarned += 1;
                break;
            case("worm"):
                riskValue += 10;
                orderValue += 2.0f;
                xpEarned += 1;
                break;
            case("golem"):
                riskValue += 0;
                orderValue += 2.5f;
                xpEarned += 2;
                break;
            case("humanoid"):
                riskValue += 20;
                orderValue += 3.0f;
                xpEarned += 3;
                break;
            case("chimera"):
                riskValue += 25;
                orderValue += 5.0f;
                xpEarned = (int)(xpEarned * 1.25);
                break;
            case("serpent"):
                riskValue += 35;
                orderValue += 7.0f;
                xpEarned += 3;
                break;
            case("curse"):
                riskValue += 75;
                orderValue *= 3;
                xpEarned *= 2;
                break;
            default:
                break;
        }
    }

    private void ParseColor(string color)
    {
        switch(color)
        {
            case("blue"):
                riskValue += 0;
                orderValue += 0.1f;
                xpEarned += 1;
                break;
            case("green"):
                riskValue += 0;
                orderValue += 0.25f;
                xpEarned += 1;
                break;
            case("yellow"):
                riskValue += 0;
                orderValue += 0.3f;
                xpEarned += 1;
                break;
            case("brown"):
                riskValue += 5;
                orderValue += 0.45f;
                xpEarned += 1;
                break;
            case("purple"):
                riskValue += 10;
                orderValue += 0.75f;
                xpEarned += 2;
                break;
            case("orange"):
                riskValue += 20;
                orderValue += 1.5f;
                xpEarned += 3;
                break;
            case("white"):
                riskValue += 10;
                orderValue += 2.0f;
                xpEarned += 3;
                break;
            case("red"):
                riskValue += 30;
                orderValue += 3.0f;
                xpEarned += 4;
                break;
            case("black"):
                riskValue += 40;
                orderValue += 5.0f;
                xpEarned += 5;
                break;
            default:
                break;
        }
    }

    private void ParseSize(string size)
    {
        switch(size)
        {
            case("tiny"):
                riskValue += 0;
                orderValue += 0.1f;
                xpEarned += 1;
                break;
            case("small"):
                riskValue += 5;
                orderValue += 0.25f;
                xpEarned += 1;
                break;
            case("medium"):
                riskValue += 10;
                orderValue += 0.5f;
                xpEarned += 2;
                break;
            case("large"):
                riskValue += 15;
                orderValue += 1.0f;
                xpEarned += 3;
                break;
            case("huge"):
                riskValue += 25;
                orderValue += 2.0f;
                xpEarned += 4;
                break;
            default:
                break;
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
                text.text = "$";
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

    public void SetNewOrderObject(GameObject newOrderObject)
    {
        orderObject = newOrderObject;
    }

    public int GetRiskValue()
    {
        return riskValue;
    }

    public int GetXpEarned()
    {
        return xpEarned;
    }

    public void SetRiskValue(int riskValue)
    {
        this.riskValue = riskValue;
    }

    public void RefreshRisk()
    {
        TMP_Text[] textArray = orderObject.GetComponentsInChildren<TMP_Text>();
        foreach(var text in textArray) {
            if(text.name == "OrderRisk")
            {
                text.text = riskValue.ToString() + "%";
                if(isRiskyOrder)
                {
                    text.color = Color.red;
                }
            }
        }
    }
}
