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
    #endregion

    void Awake()
    {
        ConstructDictionary();
    }

    void ConstructDictionary()
    {
        availabilityDictionary  = new Dictionary<GameObject, string[]>
        {
            {bookHandlerObj, new string[] {"MainMenu", "CandleScene"}},
            {summonBookButton, new string[] {"MainMenu", "CandleScene"}}
        };
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SummonBook();
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
