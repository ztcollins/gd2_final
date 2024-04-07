using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    Dictionary<GameObject, string[]> availabilityDictionary;

    #region References
        [SerializeField] private BookHandler bookHandler;
        [SerializeField] private GameObject bookHandlerObj;
        [SerializeField] private GameObject summonBookButton;
        [SerializeField] private GameObject statsPanel;
        [SerializeField] private GameObject debugMenu;
    #endregion

    void Awake()
    {
        ConstructDictionary();
    }

    void ConstructDictionary()
    {
        availabilityDictionary  = new Dictionary<GameObject, string[]>
        {
            {bookHandlerObj, new string[] {"Hub", "CandleScene", "LobbyScene"}},
            {summonBookButton, new string[] {"Hub", "CandleScene", "LobbyScene"}},
            {statsPanel, new string[] {"Hub", "LobbyScene"}}
        };
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SummonBook();
        }
        if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && bookHandler.isActive)
        {
            bookHandler.PrevPage();
        }
        if((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && bookHandler.isActive)
        {
            bookHandler.NextPage();
        }
        if(Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Tilde)) //REMOVE BEFORE REAL BUILD!!!!!!!
        {
            debugMenu.SetActive(!debugMenu.activeSelf);
        }
    }

    public void SummonBook()
    {
        bookHandler.SummonBook();
    }

    public void OnSceneChange(string currentScene)
    {
        Debug.Log("Changed Scene");
        foreach (KeyValuePair<GameObject, string[]> kvp in availabilityDictionary)
        {
            if(kvp.Value.Contains(currentScene)) kvp.Key.SetActive(true);
            else kvp.Key.SetActive(false);
        }
    }

}
