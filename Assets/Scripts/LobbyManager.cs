using System;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{

    public GameObject customerPrefab;
    public GameObject prefabOrder;
    private GameObject ordersObject;
    private List<Customer> customerList;
    private List<Order> orderList;

    // Start is called before the first frame update
    void Start()
    {
        ordersObject = GameObject.Find("OrdersList");
        customerList = new List<Customer>();
        orderList = new List<Order>();
        Tester();
    }

    public Customer AddCustomer() {
        Customer newCustomer = new Customer();
        GameObject instantiatedCustomer = Instantiate(customerPrefab, new Vector2(0, 0), Quaternion.identity);    
        newCustomer.Initialize(instantiatedCustomer);
        customerList.Add(newCustomer);
        return newCustomer;
    }

    public void HandleCustomerClick(GameObject clickedCustomer)
    {
        Debug.Log(clickedCustomer.name + " has been clicked!");

        Customer customer = clickedCustomer.GetComponent<Customer>();

        if(customer.HasBeenClicked()) {
            // do nothing
        }
        else {
            
            // add customer to order queue
            GameObject order = Instantiate(prefabOrder, new Vector2(0, 0), Quaternion.identity);
            Order newOrder = customer.StartOrder(order);
            orderList.Add(newOrder);
            newOrder.visualizeOrder();
            order.transform.SetParent(ordersObject.transform);
        }
 
    }

    public void HandleOrderClick(GameObject clickedOrder)
    {
        Debug.Log(clickedOrder + " has been clicked!");

        Order order = clickedOrder.GetComponent<Order>();

        //start order minigame here
    }

    // test code here
    void Tester() {
        AddCustomer();
    }
    
}
