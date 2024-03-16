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
    
    public void Initialize(Customer customer, GameObject order) {
        associatedCustomer = customer;
        orderObject = order;

        String[] colors = {"red", "green", "blue"};
        String[] sizes = {"small", "medium", "large"};
        String[] types = {"human", "worm", "imp"};

        int colorChoice = Random.Range(0,2);
        int sizeChoice = Random.Range(0,2);
        int typeChoice = Random.Range(0,2);
        color = colors[colorChoice];
        size = sizes[sizeChoice];
        type = types[typeChoice];

        orderValue = Random.Range(1.00f, 3.00f);
    }

    public void visualizeOrder() {
        TMP_Text[] textArray = orderObject.GetComponentsInChildren<TMP_Text>();
        foreach(var text in textArray) {
            if(text.name == "OrderType") {
                text.text = type;
            }
            else if(text.name == "OrderColor") {
                text.text = color;
            }
            else if(text.name == "OrderSize") {
                text.text = size;
            }
        }
    }

    public GameObject GetGameObject() {
        return orderObject;
    }

    public Customer GetAssociatedCustomer() {
        return associatedCustomer;
    }

    public String GetOrderColor() {
        return color;
    }

    public String GetOrderSize() {
        return size;
    }

    public String GetOrderType() {
        return type;
    }

    public float GetOrderValue() {
        return orderValue;
    }
}
