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

    public void PrevDay()
    {
        day--;
    }

    public void SetDay(int day)
    {
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
        data.money = money;
        data.day = day;
    }

    public void ForceRefresh()
    {
        if(GameObject.Find("HubManager") != null) GameObject.Find("HubManager").GetComponent<HubManager>().Refresh();
        if(GameObject.Find("LobbyManager") != null) GameObject.Find("LobbyManager").GetComponent<LobbyManager>().Refresh();
    }
    
}
