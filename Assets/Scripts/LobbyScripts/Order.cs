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
                orderValue += 0;
                xpEarned += 0;
                break;
            case("worm"):
                riskValue += 0;
                orderValue += 0;
                xpEarned += 0;
                break;
            case("golem"):
                riskValue += 0;
                orderValue += 0;
                xpEarned += 0;
                break;
            case("humanoid"):
                riskValue += 0;
                orderValue += 0;
                xpEarned += 0;
                break;
            case("chimera"):
                riskValue += 0;
                orderValue += 0;
                xpEarned += 0;
                break;
            case("serpent"):
                riskValue += 0;
                orderValue += 0;
                xpEarned += 0;
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
                break;
            case("green"):
                break;
            case("yellow"):
                break;
            case("brown"):
                break;
            case("purple"):
                break;
            case("orange"):
                break;
            case("white"):
                break;
            case("red"):
                break;
            case("black"):
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
                break;
            case("small"):
                break;
            case("medium"):
                break;
            case("large"):
                break;
            case("huge"):
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
