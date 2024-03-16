using UnityEngine;

public class Customer : MonoBehaviour
{
    private GameObject customerObject;
    private GameObject associatedOrderObject;
    private bool hasBeenClicked;
    private Order customersOrder;

    public void Awake() {
        Debug.Log("customer awakens");
        hasBeenClicked = false;
    }

    public Order StartOrder(GameObject order) {
        hasBeenClicked = true;
        associatedOrderObject = order;
        Order newOrder = order.GetComponent<Order>();
        newOrder.Initialize(this, order);
        customersOrder = newOrder;
        return customersOrder;
    }

    public void Initialize(GameObject newCustomer) {
        hasBeenClicked = false;
        customerObject = newCustomer;
    }

    public bool HasBeenClicked() {
        return hasBeenClicked;
    }

    public GameObject GetGameObject() {
        return customerObject;
    }

}
