using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleGameManager : MonoBehaviour
{
    public GameObject prefabOrder;
    public GameObject ordersObject;

    private Order currentOrder;

    // Start is called before the first frame update
    void Start()
    {
        currentOrder = GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().GetCurrentOrder();
        Debug.Log(currentOrder.GetOrderColor());
        GameObject newOrder = Instantiate(prefabOrder, new Vector2(0, 0), Quaternion.identity);
        currentOrder.SetNewOrderObject(newOrder);
        currentOrder.visualizeOrder();
        newOrder.transform.SetParent(ordersObject.transform);
    }

}
