using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsPanel : MonoBehaviour
{
    void Awake()
    {

    }

    void LoadStats()
    {
        //GameObject.FindWithTag("DataManager").GetComponent<DataPersistenceManager>().BeginLoading();
    }

    public void LoadData(GameData data)
    {
        Debug.Log("Loading main menu data");
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = data.day.ToString();
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = data.money.ToString();
    }
}
