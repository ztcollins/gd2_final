using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyHandler : MonoBehaviour
{
    private List<Customer> customerList;
    private List<Order> orderList;
    private float currentMoney;
    public static LobbyHandler instance { get; private set; }
    
    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Found more than one Order Manager in the scene.");
        }
        instance = this;

        customerList = new List<Customer>();
        orderList = new List<Order>();
    }

    public void SaveState(List<Customer> currentCustomers, List<Order> currentOrders, float currentMoney)
    {
        this.customerList = currentCustomers;
        this.orderList = currentOrders;
        this.currentMoney = currentMoney;
    }

    public List<Customer> GetCustomers()
    {
        return this.customerList;
    }

    public List<Order> GetOrders()
    {
        return this.orderList;
    }

    public void SetOrders(List<Order> newList)
    {
        this.orderList = newList;
    }

    public float GetCurrentMoney()
    {
        return this.currentMoney;
    }

    public void SetCurrentMoney(float money)
    {
        this.currentMoney = money;
    }

}
