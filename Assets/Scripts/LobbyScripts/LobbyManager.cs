using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private StatsHandler statsHandler;
    public GameObject customerPrefab;
    public GameObject prefabOrder;
    public GameObject resultsPanel;
    private GameObject ordersObject;
    private float money;
    private int day;

    void Start() 
    {
        statsHandler = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>();

        // load in lobby handler data
        List<Customer> loadedCustomers = GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().GetCustomers();
        List<Order> loadedOrders = GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().GetOrders();
        float loadedMoney = GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().GetCurrentMoney();

        // reload lobby data
        ordersObject = GameObject.Find("OrdersList");

        // money
        if(loadedMoney != 0)
        {
            money = loadedMoney;
        }
        else
        {
            money = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().GetMoney();
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

        // orders
        if(loadedOrders.Count > 0)
        {
            foreach(var order in loadedOrders)
            {
                GameObject newOrderObj = Instantiate(prefabOrder, new Vector2(0, 0), Quaternion.identity);
                Order newOrder = newOrderObj.GetComponent<Order>();
                newOrder.SetNewOrder(order, newOrderObj);
                newOrder.visualizeOrder();
                newOrder.transform.SetParent(ordersObject.transform);

                if(newOrder.IsCurrentOrder())
                {
                    GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().SetCurrentOrder(newOrder);
                }
            }
        }
        else
        {
            loadedOrders = new List<Order>();
        }

        // reload stats
        Refresh();

        // order complete animation?

        // complete order
        Order currentOrder = GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().GetCurrentOrder();
        bool isCurrentOrderComplete = GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().IsOrderComplete();
        if(isCurrentOrderComplete)
        {
            FinishOrder(currentOrder);
        }
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

    public void SaveIntermediate()
    {
        List<Customer> currentCustomers = GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().GetCustomers();
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
        GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().GetCustomers().Add(newCustomer);
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
            Order newOrder = customer.StartOrder(order);
            
            newOrder.visualizeOrder();
            order.transform.SetParent(GameObject.Find("OrdersList").transform);
            GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().GetOrders().Add(newOrder);
        }
 
    }

    public void HandleOrderClick(GameObject clickedOrder)
    {
        Order order = clickedOrder.GetComponent<Order>();

        // save the current state before leaving
        SaveIntermediate();

        // start order minigame here
        GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().InitializeCandleMinigame(order);
        GameObject.FindWithTag("SceneHandler").GetComponent<SceneHandler>().UseInstruction(SceneHandlerInstruction.CHANGESCENE, "CandleScene");

        // complete order by coming back to AWAKE() with bool isOrderComplete = true
    }

    public void FinishOrder(Order orderToFinish)
    {
        // remove from lists
        GameObject orderObject = orderToFinish.GetGameObject();
        Customer associatedCustomer = orderToFinish.GetAssociatedCustomer();
        GameObject associatedCustomerObject = associatedCustomer.GetGameObject();
        GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().GetCustomers().Remove(associatedCustomer);
        GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().GetOrders().Remove(orderToFinish);
        
        if(SummoningResults(orderToFinish))
        {
            // give player money
            statsHandler.AddMoney(orderToFinish.GetOrderValue());
            Refresh();
        }


        // remove the customer & order
        Destroy(associatedCustomerObject);
        Destroy(orderObject);

        // reset current order
        GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().SetCurrentOrder(null);
        GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().SetOrderComplete(false);
    }

    public bool SummoningResults(Order currentOrder)
    {
        string currentDemonSize = GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().GetCurrentDemonSize();
        string currentDemonColor = GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().GetCurrentDemonColor();
        string currentDemonType = GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().GetCurrentDemonType();

        Debug.Log(currentDemonSize);
        Debug.Log(currentDemonColor);
        Debug.Log(currentDemonType);

        string results = "FAILURE";
        bool isCorrect = false;
        if(currentDemonSize == currentOrder.GetOrderSize() && currentDemonColor == currentOrder.GetOrderColor() && currentDemonType == currentOrder.GetOrderType())
        {
            isCorrect = true;
            results = "SUCCESS";
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
        GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().SaveState(new List<Customer>(), new List<Order>(), 0);
        GameObject.FindWithTag("DataHandler").GetComponent<DataPersistenceManager>().SaveGame();
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
        List<Customer> customerList = GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().GetCustomers();
        foreach(var customer in customerList)
        {
            GameObject customerObj = customer.GetGameObject();
            SceneManager.MoveGameObjectToScene(customerObj, SceneManager.GetActiveScene());
        }
        FinishDay();
        GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().SetCurrentOrder(null);
        GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().SetOrderComplete(false);
        GameObject.FindWithTag("SceneHandler").GetComponent<SceneHandler>().UseInstruction(SceneHandlerInstruction.CHANGESCENE, "MainMenu");
    }

}
