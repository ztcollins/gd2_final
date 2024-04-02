using UnityEngine;
using UnityEngine.EventSystems;

public class EventClickOrder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject gameManager;
    private GameObject thisOrder;
    private LobbyManager lobbyManager;

    private void Awake()
    {
        gameManager = GameObject.Find("LobbyManager");
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
        thisOrder = eventData.pointerEnter;
        lobbyManager.HandleOrderClick(thisOrder);
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
