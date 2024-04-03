using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public GameObject customerPrefab;
    public GameObject prefabOrder;
    public GameObject resultsPanel;
    private GameObject orderListObject;
    private GameObject customerListObject;
    private List<Customer> customerList;
    private List<Order> orderList;
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
        isDayFinished = false;

        Refresh();

        // order complete animation?

        // complete order
        if(orderHandler.IsOrderComplete())
        {
            FinishOrder(orderHandler.GetCurrentOrder());
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

    public void Refresh()
    {
        money = statsHandler.GetMoney();
        day = statsHandler.GetDay();
        dayText.text = "Day " + statsHandler.GetDay().ToString();
        moneyText.text = "$" + statsHandler.GetMoney().ToString("F2");

        ClearLists();
        
        if((customerList = lobbyHandler.GetCustomers()) == null)
        {
            customerList = new List<Customer>();
            CreateCustomerList();
        }
        RenderCustomerList();
        customerListObject.SetActive(true);

        if((orderList = lobbyHandler.GetOrders()) == null)
        {
            orderList = new List<Order>();
        }
        RenderOrderList();
        orderListObject.SetActive(true);
    }

    private void ClearLists()
    {

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

    private void LoadEvent()
    {

    }

    public void SaveIntermediate()
    {
        lobbyHandler.SaveState(customerList, orderList, money);
        customerListObject.SetActive(false);
    }

    public void HandleCustomerClick(GameObject clickedCustomer)
    {
        Customer customer = clickedCustomer.GetComponent<Customer>();
        clickedCustomer.GetComponent<SpriteRenderer>().color = Color.red;

        if(!customer.hasBeenClicked)
        {
            orderList.Add(customer.order);
            customer.order.RenderOrder(orderListObject);
        }
 
    }

    public void HandleOrderClick(GameObject clickedOrder)
    {
        Order order = clickedOrder.GetComponent<Order>();

        // save the current state before leaving
        SaveIntermediate();

        // start order minigame here
        orderHandler.InitializeCandleMinigame(order);
        sceneHandler.UseInstruction(SceneHandlerInstruction.CHANGESCENE, "CandleScene");

        // complete order by coming back to AWAKE() with bool isOrderComplete = true
    }

    public void FinishOrder(Order orderToFinish)
    {        
        customerList.Remove(orderToFinish.customer);
        orderList.Remove(orderToFinish);
        
        if(SummoningResults(orderToFinish))
        {
            // give player money
            statsHandler.AddMoney(orderToFinish.value);
        }


        // remove the customer & order
        Destroy(orderToFinish.customer.gameObject);
        

        // reset current order
        orderHandler.SetCurrentOrder(null);
        orderHandler.SetOrderComplete(false);

        SaveIntermediate();
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

    public void RenderOrderList()
    {
        foreach(Order order in orderList)
        {
            order.RenderOrder(orderListObject);
            if(order.isCurrentOrder) orderHandler.SetCurrentOrder(order);
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
