using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHandler : MonoBehaviour, IDataPersistence
{
    private int day;
    public int Day { get { return day; } set { this.day = value; } }
    private float money;
    public float Money { get { return money; } set { this.money = value; } }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void LoadData(GameData data)
    {
        Debug.Log("Loading data");
        this.money = data.money;
        this.day = data.day;
        Debug.Log("Loaded Stats Handler Money: " + this.money);
        Debug.Log("Loaded Stats Handler Day: " + this.day);
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("Saving data");
        data.money = this.money;
        data.day = this.day;
    }
    
}
