using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{

    public GameObject customerPrefab;
    public GameObject prefabOrder;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dayText;
    private GameObject ordersObject;
    private List<Customer> customerList;
    private List<Order> orderList;
    private float money;
    private int day;
    private Boolean isDayFinished;

    // Start is called before the first frame update
    void Start()
    {
        //setup the scene
        this.money = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().Money;
        this.day = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().Day;
        SetMoney(this.money);
        SetDay(this.day);
        isDayFinished = false;
        ordersObject = GameObject.Find("OrdersList");
        customerList = new List<Customer>();
        orderList = new List<Order>();

        //load in customers
        Tester();
    }

    void Update()
    {
        if(customerList.Count == 0 && !isDayFinished)
        {
            Debug.Log("DAY FINISHED!");
            isDayFinished = true;
            FinishDay();
        }
    }

    public Customer AddCustomer(Vector2 position) {
        
        GameObject instantiatedCustomer = Instantiate(customerPrefab, position, Quaternion.identity);    
        Customer newCustomer = instantiatedCustomer.GetComponent<Customer>();
        newCustomer.Initialize(instantiatedCustomer);
        customerList.Add(newCustomer);
        return newCustomer;
    }

    public void HandleCustomerClick(GameObject clickedCustomer)
    {
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
        Order order = clickedOrder.GetComponent<Order>();

        //start order minigame here
        //GameObject.Find("SceneManager").GetComponent<CandleOrderManager>().InitializeCandleMinigame(order);
        //SceneManager.LoadScene("CandleScene", LoadSceneMode.Additive);
        //SceneManager.UnloadSceneAsync("lobbyScene");

        //complete order
        FinishOrder(order);
    }

    public void FinishOrder(Order orderToFinish)
    {
        //remove from lists
        GameObject orderObject = orderToFinish.GetGameObject();
        Customer associatedCustomer = orderToFinish.GetAssociatedCustomer();
        GameObject associatedCustomerObject = associatedCustomer.GetGameObject();
        customerList.Remove(associatedCustomer);
        orderList.Remove(orderToFinish);

        //give player money
        money += orderToFinish.GetOrderValue();
        SetMoney(money);

        //remove the customer & order
        Destroy(associatedCustomerObject);
        Destroy(orderObject);
    }

    public void FinishDay()
    {
        this.day++;
        GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().Day = this.day;
        GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().Money = this.money;
        GameObject.FindWithTag("DataHandler").GetComponent<DataPersistenceManager>().SaveGame();
        GameObject.FindWithTag("SceneHandler").GetComponent<SceneHandler>().UseInstruction(SceneHandlerInstruction.CHANGESCENE, "MainMenu");
    }

    public void SetMoney(float value)
    {
        moneyText.text = value.ToString("F2");
    }

     public void SetDay(int value)
    {
        dayText.text = value.ToString();
    }

    // test code here
    void Tester() {
        AddCustomer(new Vector2(0,0));
        AddCustomer(new Vector2(0,2));
    }

}
