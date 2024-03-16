using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleGameManager : MonoBehaviour
{

    private Order currentOrder;

    // Start is called before the first frame update
    void Start()
    {
        currentOrder = GameObject.Find("SceneManager").GetComponent<CandleOrderManager>().GetCurrentOrder();
        Debug.Log(currentOrder.GetOrderColor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
