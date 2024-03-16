using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour, IDataPersistence
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
        GameObject.FindWithTag("DataHandler").GetComponent<DataPersistenceManager>().BeginLoading();
        isDayFinished = false;
        ordersObject = GameObject.Find("OrdersList");
        customerList = new List<Customer>();
        orderList = new List<Order>();
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
        GameObject.Find("SceneManager").GetComponent<DataPersistenceManager>().SaveGame();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("lobbyScene");
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

    public void LoadData(GameData data)
    {
        Debug.Log("Loading Lobby data");
        this.money = data.money;
        this.day = data.day;
        SetMoney(this.money);
        SetDay(this.day);
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("Saving Lobby data");
        data.money = this.money;
        data.day = this.day;
    }
}
