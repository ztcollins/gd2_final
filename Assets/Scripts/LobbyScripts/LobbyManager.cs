using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private OrderData orderData;
    public GameObject customerPrefab;
    public GameObject prefabOrder;
    public GameObject resultsPanel;
    private GameObject ordersObject;
    private float money;
    private int day;

    #region References
        StatsHandler statsHandler;
        LobbyHandler lobbyHandler;
        OrderHandler orderHandler;
        SceneHandler sceneHandler;
        DataPersistenceManager dataHandler;
    #endregion

    void Start() 
    {
        FindReferences();
        ParseDay();

        // load in lobby handler data
        List<Customer> loadedCustomers = lobbyHandler.GetCustomers();
        List<Order> loadedOrders = lobbyHandler.GetOrders();
        float loadedMoney = lobbyHandler.GetCurrentMoney();

        // load the orders list (empty)
        ordersObject = GameObject.Find("OrdersList");

        // money
        if(loadedMoney != 0)
        {
            money = loadedMoney;
        }
        else
        {
            money = statsHandler.GetMoney();
        }

        // customers
        if(loadedCustomers.Count > 0)
        {
            Debug.Log(loadedCustomers.Count);
            foreach(var customer in loadedCustomers)
            {
                GameObject customerObj = customer.GetGameObject();
                customerObj.SetActive(true);
            }
        }
        else
        {
            CreateNewCustomers();
        }

        // complete order
        Order currentOrder = orderHandler.GetCurrentOrder();
        bool isCurrentOrderComplete = orderHandler.IsOrderComplete();
        if(isCurrentOrderComplete)
        {
            FinishOrder(currentOrder);
            loadedOrders = lobbyHandler.GetOrders();
        }

        // orders
        if(loadedOrders.Count > 0)
        {
            List<Order> newRenderedOrders = new List<Order>();
            foreach(var order in loadedOrders)
            {
                GameObject newOrderObj = Instantiate(prefabOrder, new Vector2(0, 0), Quaternion.identity);
                Order newOrder = newOrderObj.GetComponent<Order>();
                newOrder.SetNewOrder(order, newOrderObj);
                newOrder.visualizeOrder();
                newOrder.transform.SetParent(ordersObject.transform, false);
                newRenderedOrders.Add(newOrder);
            }
            lobbyHandler.SetOrders(newRenderedOrders);
        }
        else
        {
            loadedOrders = new List<Order>();
        }

        // reload stats
        Refresh();
    }

    void FindReferences()
    {
        statsHandler = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>();
        lobbyHandler = GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>();
        orderHandler = GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>();
        sceneHandler = GameObject.FindWithTag("SceneHandler").GetComponent<SceneHandler>();
        dataHandler = GameObject.FindWithTag("DataHandler").GetComponent<DataPersistenceManager>();
    }

    void Update()
    {
        /*if(customerList.Count == 0 && !isDayFinished)
        {
            Debug.Log("DAY FINISHED!");
            isDayFinished = true;
            FinishDay();
        }*/
    }

    public void ParseDay()
    {
        orderData = statsHandler.GetOrderData();
        Debug.Log(orderData.ToString());
    }

    public void SaveIntermediate()
    {
        List<Customer> currentCustomers = lobbyHandler.GetCustomers();
        foreach(var customer in currentCustomers)
        {
            GameObject customerObj = customer.GetGameObject();
            DontDestroyOnLoad(customerObj);
            customerObj.SetActive(false);
        };
    }

    public Customer AddNewCustomer(Vector2 position) {
        GameObject instantiatedCustomer = Instantiate(customerPrefab, position, Quaternion.identity);    
        Customer newCustomer = instantiatedCustomer.GetComponent<Customer>();
        newCustomer.Initialize(instantiatedCustomer, position);
        lobbyHandler.GetCustomers().Add(newCustomer);
        return newCustomer;
    }

    public void HandleCustomerClick(GameObject clickedCustomer)
    {
        Customer customer = clickedCustomer.GetComponent<Customer>();
        clickedCustomer.GetComponent<SpriteRenderer>().color = Color.red;

        if(customer.HasBeenClicked()) {
            // do nothing
        }
        else {
            
            // add customer to order queue
            GameObject order = Instantiate(prefabOrder, new Vector2(0, 0), Quaternion.identity);
            Order newOrder = customer.StartOrder(order, orderData);
            
            newOrder.visualizeOrder();
            order.transform.SetParent(GameObject.Find("OrdersList").transform, false);
            lobbyHandler.GetOrders().Add(newOrder);
        }
 
    }

    public void HandleOrderClick(GameObject clickedOrder)
    {
        Order order = clickedOrder.GetComponent<Order>();

        // save the current state before leaving
        SaveIntermediate();

        // start order minigame here
        orderHandler.InitializeCandleMinigame(order);
        orderHandler.SetCurrentOrder(order);
        sceneHandler.UseInstruction(SceneHandlerInstruction.CHANGESCENE, "CandleScene");

        // complete order by coming back to AWAKE() with bool isOrderComplete = true
    }

    public void FinishOrder(Order orderToFinish)
    {
        // remove from lists
        GameObject orderObject = orderToFinish.GetGameObject();
        Customer associatedCustomer = orderToFinish.GetAssociatedCustomer();
        GameObject associatedCustomerObject = associatedCustomer.GetGameObject();
        lobbyHandler.GetCustomers().Remove(associatedCustomer);
        lobbyHandler.GetOrders().Remove(orderToFinish);
        
        if(SummoningResults(orderToFinish))
        {
            // give player money & xp
            statsHandler.AddMoney(orderToFinish.GetOrderValue());
            statsHandler.AddCurrentXp(orderToFinish.GetXpEarned());
            Refresh();
        }


        // remove the customer & order
        Destroy(associatedCustomerObject);
        Destroy(orderObject);

        // reset current order
        orderHandler.SetCurrentOrder(null);
        orderHandler.SetOrderComplete(false);
    }

    public bool SummoningResults(Order currentOrder)
    {
        string currentDemonSize = orderHandler.GetCurrentDemonSize();
        string currentDemonColor = orderHandler.GetCurrentDemonColor();
        string currentDemonType = orderHandler.GetCurrentDemonType();

        Debug.Log(currentDemonSize);
        Debug.Log(currentDemonColor);
        Debug.Log(currentDemonType);

        string results = "FAILURE";
        bool isCorrect = false;
        if(currentDemonSize == currentOrder.GetOrderSize() && currentDemonColor == currentOrder.GetOrderColor() && currentDemonType == currentOrder.GetOrderType())
        {
            isCorrect = true;
            results = "SUCCESS";
            if(Random.Range(0, 100) < currentOrder.GetRiskValue())
            {
                isCorrect = false;
                results = "DEMON ESCAPED";
            }
        }

        TextMeshProUGUI[] textToChange = resultsPanel.GetComponentsInChildren<TextMeshProUGUI>();

        foreach(var text in textToChange)
        {
            switch(text.tag)
            {
                case "MoneyEarned" :
                    if(isCorrect)
                    {
                        text.text = currentOrder.GetOrderValue().ToString("F2");
                    }
                    
                    break;
                case "ResultsTitle" :
                    text.text = results;
                    if(!isCorrect)
                    {
                        text.color = Color.red;
                    }
                    break;
                case "RequestSize" :
                    text.text = currentOrder.GetOrderSize();
                    break;
                case "RequestColor" :
                    text.text = currentOrder.GetOrderColor();
                    break;
                case "RequestType" :
                    text.text = currentOrder.GetOrderType();
                    break;
                case "SummonedSize" :
                    text.text = currentDemonSize;
                    break;
                case "SummonedColor" :
                    text.text = currentDemonColor;
                    break;
                case "SummonedType" :
                    text.text = currentDemonType;
                    break;
                case "SummonedRisk" :
                    text.text = currentOrder.GetRiskValue().ToString() + "%";
                    if(currentOrder.GetRiskValue() == 0)
                    {
                        text.color = Color.green;
                    }
                    break;
                case "ReputationMultiplier" :
                    text.text = "x" + statsHandler.GetReputationLevel().ToString();
                    break;
                case "XpEarned" :
                    if(isCorrect)
                    {
                        text.text = currentOrder.GetXpEarned().ToString() + " xp";
                    }
                    else
                    {
                        text.text = "0 xp";
                        text.color = Color.red;
                    }
                    break;
                default:
                    break;
            }
        }

        resultsPanel.SetActive(true);

        if(isCorrect)
        {
            return true;
        }
        return false;
    }

    public void CloseResults()
    {
        resultsPanel.SetActive(false);
    }

    public void FinishDay()
    {
        statsHandler.SetMoney(money);
        statsHandler.NextDay();
        lobbyHandler.SaveState(new List<Customer>(), new List<Order>(), 0);
        dataHandler.SaveGame();
    }

    public void Refresh()
    {
        money = statsHandler.GetMoney();
        day = statsHandler.GetDay();
        statsHandler.VisualizeCurrentValues();
    }

    // test code here
    void CreateNewCustomers()
    {
        Debug.Log("Creating new customers");
        int customerNumber = Random.Range(2,6);
        for(int i = 0; i < customerNumber; i++)
        {
            AddNewCustomer(new Vector2(i*2-6,0));
        }
        //AddNewCustomer(new Vector2(0,0));
        //AddNewCustomer(new Vector2(0,2));
    }

    public void Back()
    {
        Debug.Log("LEAVING LOBBY");
        List<Customer> customerList = lobbyHandler.GetCustomers();
        foreach(var customer in customerList)
        {
            GameObject customerObj = customer.GetGameObject();
            SceneManager.MoveGameObjectToScene(customerObj, SceneManager.GetActiveScene());
        }
        FinishDay();
        orderHandler.SetCurrentOrder(null);
        orderHandler.SetOrderComplete(false);
        sceneHandler.UseInstruction(SceneHandlerInstruction.CHANGESCENE, "MainMenu");
    }

}
