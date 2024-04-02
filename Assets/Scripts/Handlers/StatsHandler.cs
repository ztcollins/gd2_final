using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHandler : MonoBehaviour, IDataPersistence
{
    public int day;
    public float money;
    
    public void NextDay()
    {
        day++;
    }

    public void SetDay(int day)
    {
        Debug.Log("SET DAY");
        this.day = day;
    }

    public int GetDay()
    {
        return day;
    }

    public void AddMoney(float money)
    {
        this.money += money;
    }

    public void SetMoney(float money)
    {
        Debug.Log("SET MONEY");
        this.money = money;
    }

    public float GetMoney()
    {
        return money;
    }

    public void LoadData(GameData data)
    {
        this.money = data.money;
        this.day = data.day;
    }

    public void SaveData(ref GameData data)
    {
        data.money = this.money;
        data.day = this.day;
    }
    
}
