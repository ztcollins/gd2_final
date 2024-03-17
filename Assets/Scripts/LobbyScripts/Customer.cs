using UnityEngine;

public class Customer : MonoBehaviour
{
    private GameObject customerObject;
    private GameObject associatedOrderObject;
    private bool hasBeenClicked;
    private Order customersOrder;
    private Vector2 position;

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

    public void Initialize(GameObject newCustomer, Vector2 position) {
        hasBeenClicked = false;
        customerObject = newCustomer;
        this.position = position;
    }

    public bool HasBeenClicked() {
        return hasBeenClicked;
    }

    public GameObject GetGameObject() {
        return customerObject;
    }

    public Vector2 GetPosition()
    {
        return position;
    }

}
