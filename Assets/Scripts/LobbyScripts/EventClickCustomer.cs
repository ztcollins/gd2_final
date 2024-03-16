using UnityEngine;
using UnityEngine.EventSystems;

public class EventClickCustomer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject gameManager;
    private GameObject thisCustomer;
    private LobbyManager lobbyManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        lobbyManager = gameManager.GetComponent<LobbyManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //empty
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //empty
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //get the current clicked customer and then handle the click event
        thisCustomer = eventData.pointerEnter;
        lobbyManager.HandleCustomerClick(thisCustomer);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //empty
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //empty
    }
}
