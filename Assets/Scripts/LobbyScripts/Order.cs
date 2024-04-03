using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Order
{ 
    public string color;
    public string size;
    public string type;
    public float value;
    public Customer customer;
    public bool isCurrentOrder;

    #region References
    #endregion
    
    public Order(string color, string size, string type, float value)
    {
        this.color = color;
        this.size = size;
        this.type = type;
        this.value = value;
        Debug.Log("BUILT");
    }

    public string ToString()
    {
        return color + " " + size + " " + type + " " + value.ToString("F2");
    }

    public void SetData(Order order)
    {
        this.color = order.color;
        this.size = order.size;
        this.type = order.type;
        this.value = order.value;
        this.isCurrentOrder = order.isCurrentOrder;
    }

    public GameObject RenderOrder(GameObject parent)
    {
        GameObject orderCard = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/OrderCard"), new Vector2(0, 0), Quaternion.identity, parent.transform);

        TMP_Text[] textArray = orderCard.GetComponentsInChildren<TMP_Text>();
        foreach(var text in textArray) 
        {
            switch(text.name)
            {
                case("OrderType"):
                    text.text = type;
                    break;
                case("OrderColor"):
                    text.text = color;
                    break;
                case("OrderSize"):
                    text.text = size;
                    break;
                default:
                    break;
            }
        }
        return orderCard;
    }
}
