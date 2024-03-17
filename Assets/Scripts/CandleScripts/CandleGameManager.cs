using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleGameManager : MonoBehaviour
{

    private Order currentOrder;

    // Start is called before the first frame update
    void Start()
    {
        currentOrder = GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().GetCurrentOrder();
        Debug.Log(currentOrder.GetOrderColor());
    }

}
