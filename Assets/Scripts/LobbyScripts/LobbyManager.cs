using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LobbyManager : MonoBehaviour
{

    public GameObject customerPrefab;
    public GameObject prefabOrder;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dayText;
    public GameObject resultsPanel;
    private GameObject ordersObject;
    private List<Customer> customerList;
    private List<Order> orderList;
    private float money;
    private int day;
    private bool isDayFinished;

    void Awake() {

        // load in lobby handler data
        List<Customer> loadedCustomers = GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().GetCustomers();
        List<Order> loadedOrders = GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().GetOrders();
        float loadedMoney = GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().GetCurrentMoney();

        // reload lobby data
        ordersObject = GameObject.Find("OrdersList");
        isDayFinished = false;

        // money
        if(loadedMoney != 0)
        {
            money = loadedMoney;
        }
        else
        {
            money = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().Money;
        }

        // customers
        if(loadedCustomers != null)
        {
            customerList = loadedCustomers;
            foreach(var customer in customerList)
            {
                GameObject customerObj = customer.GetGameObject();
                customerObj.SetActive(true);
            }
        }
        else
        {
            customerList = new List<Customer>();
            CreateNewCustomers();
        }

        // orders
        if(loadedOrders != null)
        {
            orderList = new List<Order>();
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
                
                orderList.Add(newOrder);
            }
        }
        else
        {
            orderList = new List<Order>();
        }

        // reload stats
        this.day = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().Day;
        SetMoney(this.money);
        SetDay(this.day);

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
        if(customerList.Count == 0 && !isDayFinished)
        {
            Debug.Log("DAY FINISHED!");
            isDayFinished = true;
            FinishDay();
        }
    }

    public void SaveIntermediate()
    {
        GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().SaveState(customerList, orderList, money);
        foreach(var customer in customerList)
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
        customerList.Add(newCustomer);
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
            orderList.Add(newOrder);
            newOrder.visualizeOrder();
            order.transform.SetParent(GameObject.Find("OrdersList").transform);
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
        customerList.Remove(associatedCustomer);
        orderList.Remove(orderToFinish);
        
        if(SummoningResults(orderToFinish))
        {
            // give player money
            money += orderToFinish.GetOrderValue();
            SetMoney(money);
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
        this.day++;
        GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().Day = this.day;
        GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().Money = this.money;
        GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().SaveState(null, null, 0);
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
        Debug.Log("customers can be destroyed!");
        foreach(var customer in customerList)
        {
            GameObject customerObj = customer.GetGameObject();
            SceneManager.MoveGameObjectToScene(customerObj, SceneManager.GetActiveScene());
        }
        GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().SaveState(null, null, 0);
        GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().SetCurrentOrder(null);
        GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().SetOrderComplete(false);
    }

}