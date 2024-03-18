using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleGameManager : MonoBehaviour
{
    public GameObject prefabOrder;
    public GameObject ordersObject;
    public GameObject candleHolders;
    private Order currentOrder;

    // Start is called before the first frame update
    void Start()
    {
        // bring in order from last scene
        currentOrder = GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().GetCurrentOrder();
        GameObject newOrder = Instantiate(prefabOrder, new Vector2(0, 0), Quaternion.identity);
        currentOrder.SetNewOrderObject(newOrder);
        currentOrder.visualizeOrder();
        newOrder.transform.SetParent(ordersObject.transform);
    }

    public void CheckCandles()
    {
        // 22 candles in total.
        // INDICES (inclusive):
        // 0-3 inner circle (4)
        // 4-9 middle circle (6)
        // 10-21 outer circle (12)
        CandleSlot[] candleSlots = candleHolders.GetComponentsInChildren<CandleSlot>();
        Debug.Log(candleSlots.Length);
        foreach(var candleSlot in candleSlots)
        {
            //Debug.Log(candleSlot.gameObject.name);
            if(candleSlot.HasCandle())
            {
                Debug.Log(candleSlot.gameObject.name + " has a candle!");
            }
        }
    }

}
