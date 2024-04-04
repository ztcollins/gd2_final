using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public GameObject customerPrefab;
    public GameObject orderCardPrefab;
    public GameObject resultsPanel;
    private GameObject orderListObject;
    public List<Order> orderList;
    private GameObject customerListObject;
    public List<Customer> customerList;
    
    private float money;
    private int day;
    private bool isDayFinished;

    #region References
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI dayText;
                         private OrderHandler orderHandler;
                         private SceneHandler sceneHandler;
                         private DataHandler dataHandler;
                         private LobbyHandler lobbyHandler;
                         private StatsHandler statsHandler;
    #endregion

    void Awake() 
    {
        FindReferences();
        Refresh();
    }

    public void Refresh()
    {
        money = statsHandler.GetMoney();
        day = statsHandler.GetDay();
        dayText.text = "Day " + statsHandler.GetDay().ToString();
        moneyText.text = "$" + statsHandler.GetMoney().ToString("F2");

        if((customerList = lobbyHandler.GetCustomers()) == null)
        {
            customerList = new List<Customer>();
            Debug.Log("FAIL!");
            CreateCustomerList();
        }
        Debug.Log(customerList);

        if((orderList = lobbyHandler.GetOrders()) == null)
        {
            orderList = new List<Order>();
        }
        Debug.Log(customerList.Count);
        SaveState();
        orderListObject.SetActive(true);
        customerListObject.SetActive(true);
        RenderCustomerList();
        RenderOrderList();
    }

        public void RenderOrderList()
    {
        foreach(Order order in orderList)
        {
            order.RenderOrder(orderListObject);
        }
    }

    void RenderCustomerList()
    {
        int index = 0;
        foreach(Customer customer in customerList)
        {
            GameObject instantiatedCustomer = Instantiate(customerPrefab, new Vector2((index * 3) - 6, 0), Quaternion.identity, customerListObject.transform);
            instantiatedCustomer.GetComponent<Customer>().SetData(customer);
            index++;
        }
    }

    void CreateCustomerList()
    {
        int customerCount = Random.Range(2,6);
        Debug.Log(customerCount);
        for(int i = 0; i < customerCount; i++)
        {
            CreateCustomer();
        }
    }

    public Customer CreateCustomer() 
    {
        Order cOrder = GenerateOrder();
        Customer newCustomer = new Customer(cOrder);
        newCustomer.order.customer = newCustomer;
        Debug.Log("Adding...");
        customerList.Add(newCustomer);
        return newCustomer;
    }

    private Order GenerateOrder()
    {
        //filler fields until loading orders works
        string[] colors = {"red", "green", "blue"};
        string[] sizes = {"small", "medium", "large"};
        string[] types = {"humanoid", "worm", "imp"};
        float value = Random.Range(1.00f, 3.00f);
        Order newOrder = new Order(colors[Random.Range(0,3)], sizes[Random.Range(0,3)], types[Random.Range(0,3)], value);
        return newOrder;
    }

    private void FindReferences()
    {
        orderHandler = GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>();
        lobbyHandler = GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>();
        sceneHandler = GameObject.FindWithTag("SceneHandler").GetComponent<SceneHandler>();
        dataHandler = GameObject.FindWithTag("DataHandler").GetComponent<DataHandler>();
        statsHandler = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>();
        customerListObject = GameObject.Find("CustomerList");
        orderListObject = GameObject.Find("OrderList");
    }
    public void SaveState()
    {
        lobbyHandler.SaveState(customerList, orderList, money);
    }

    public void HandleCustomerClick(GameObject clickedCustomer)
    {
        Customer customer = clickedCustomer.GetComponent<Customer>();
        if(!customer.hasBeenClicked)
        {
            customer.hasBeenClicked = true;
            orderList.Add(customer.order);
            customer.order.RenderOrder(orderListObject);
            clickedCustomer.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void HandleOrderClick(GameObject clickedOrder)
    {
        Order order = clickedOrder.GetComponent<EventClickOrder>().order;

        // save the current state before leaving
        SaveState();

        // start order minigame here
        orderHandler.InitializeCandleMinigame(order);
        sceneHandler.UseInstruction(SceneHandlerInstruction.CHANGESCENE, "CandleScene");

        // complete order by coming back to AWAKE() with bool isOrderComplete = true
    }

    public void FinishOrder(Order orderToFinish)
    {      
        if(SummoningResults(orderToFinish)) statsHandler.AddMoney(orderToFinish.value);

        customerList.Remove(orderToFinish.customer);
        orderList.Remove(orderToFinish);
        
        Debug.Log(orderToFinish.customer.gameObject.transform.GetSiblingIndex());
        Destroy(orderToFinish.customer.gameObject);
        
        

        // reset current order
        orderHandler.SetCurrentOrder(null);
        orderHandler.SetOrderComplete(false);

        SaveState();
        customerListObject.SetActive(true);
        Debug.Log("Your mom");
        //may need to Refresh();
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
        if(currentDemonSize == currentOrder.size && currentDemonColor == currentOrder.color && currentDemonType == currentOrder.type)
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
                        text.text = currentOrder.value.ToString("F2");
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
                    text.text = currentOrder.size;
                    break;
                case "RequestColor" :
                    text.text = currentOrder.color;
                    break;
                case "RequestType" :
                    text.text = currentOrder.type;
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
        lobbyHandler.SaveState(null, null, 0);
        dataHandler.SaveGame();
    }



    public void Back()
    {
        Debug.Log("LEAVING LOBBY");
        FinishDay();
        Debug.Log("customers can be destroyed!");
        foreach(var customer in customerList)
        {
            GameObject customerObj = customer.gameObject;
            SceneManager.MoveGameObjectToScene(customerObj, SceneManager.GetActiveScene());
        }
        GameObject.FindWithTag("LobbyHandler").GetComponent<LobbyHandler>().SaveState(null, null, 0);
        GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().SetCurrentOrder(null);
        GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().SetOrderComplete(false);
        GameObject.FindWithTag("SceneHandler").GetComponent<SceneHandler>().UseInstruction(SceneHandlerInstruction.CHANGESCENE, "MainMenu");
    }

}
