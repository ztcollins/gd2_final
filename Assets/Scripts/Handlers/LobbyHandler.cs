using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyHandler : MonoBehaviour
{
    public bool isNewDay = true;
    public List<Customer> customerList;
    public List<Order> orderList;
    private float currentMoney;

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

    public float GetCurrentMoney()
    {
        return this.currentMoney;
    }

    public void Reset()
    {
        isNewDay = true;
        customerList = null;
        orderList = null;
    }

}
