using UnityEngine;

public class Customer : MonoBehaviour
{
    public bool hasBeenClicked;
    public Order order;

    public Customer(Order order)
    {
        hasBeenClicked = false;
        this.order = order;
    }

    public void SetData(Customer customer)
    {
        this.hasBeenClicked = customer.hasBeenClicked;
        this.order = customer.order;
    }

    // public Order StartOrder(GameObject order) {
    //     hasBeenClicked = true;
    //     this.order = order;
    //     Order newOrder = order.GetComponent<Order>();
    //     newOrder.Initialize(this, order);
    //     customersOrder = newOrder;
    //     return customersOrder;
    // }
}
