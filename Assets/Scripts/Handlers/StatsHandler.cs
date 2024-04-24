using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class StatsHandler : MonoBehaviour, IDataPersistence
{
    #region References
        [SerializeField] private TextMeshProUGUI dayText;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI reputationLevelText;
        [SerializeField] private XpBar xpBar;
        [SerializeField] private TextAsset orderJson;
        [SerializeField] private TextAsset eventJson;
    #endregion

    public OrderData orderData;
    public EventCard eventCard;
    public int day;
    public float money;
    public int reputationLevel;
    public int currentXP;
    public int requiredXP;


    public void NextDay()
    {
        day++;
        orderData = ParseOrderData(day);
        eventCard = ParseEventCard(day);
    }

    public void PrevDay()
    {
        day--;
        orderData = ParseOrderData(day);
        eventCard = ParseEventCard(day);
    }

    public void SetDay(int day)
    {
        this.day = day;
        orderData = ParseOrderData(day);
        eventCard = ParseEventCard(day);
        Debug.Log(orderData.ToString());
        Debug.Log(eventCard.ToString());
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

    public int GetReputationLevel()
    {
        return reputationLevel;
    }

    public void SetReputationLevel(int newLevel)
    {
        this.reputationLevel = newLevel;
    }

    public int GetCurrentXP()
    {
        return currentXP;
    }

    public void SetCurrentXP(int newXP)
    {
        this.currentXP = newXP;
    }

    public void AddCurrentXp(int xpToAdd)
    {
        this.currentXP += xpToAdd;
        if(this.currentXP > this.requiredXP)
        {
            this.currentXP -= this.requiredXP;
            this.AddReputationLevel(1);
            // increase xp?
        }
    }

    public void SubtractXp(int xpToRemove)
    {
        if(this.currentXP - xpToRemove < 0)
        {
            this.currentXP = 0;
        }
        else
        {
            this.currentXP = this.currentXP - xpToRemove;
        }
    }

    public void AddRequiredXp(int xpToAdd)
    {
        this.requiredXP += xpToAdd;
    }

    public void AddReputationLevel(int levelsToAdd)
    {
        this.reputationLevel += levelsToAdd;
    }

    public int GetRequiredXP()
    {
        return requiredXP;
    }

    public void SetRequiredXP(int newRequiredXP)
    {
        this.requiredXP = newRequiredXP;
    }

    public void LoadData(GameData data)
    {
        this.money = data.money;
        this.day = data.day;
        this.currentXP = data.currentXP;
        this.requiredXP = data.requiredXP;
        this.reputationLevel = data.reputationLevel;
    }

    public void SaveData(ref GameData data)
    {
        data.money = money;
        data.day = day;
        data.currentXP = currentXP;
        data.requiredXP = requiredXP;
        data.reputationLevel = reputationLevel;
    }

    public void VisualizeCurrentValues()
    {
        dayText.text = "Day " + day.ToString();
        moneyText.text = "$" + money.ToString("F2");
        reputationLevelText.text = "Level " + reputationLevel.ToString();
        xpBar.SetMaxXp(requiredXP);
        xpBar.SetXp(currentXP);
        xpBar.Render();
    }

    private OrderData ParseOrderData(int day)
    {
        OrderDataList orderDataList = JsonConvert.DeserializeObject<OrderDataList>(orderJson.ToString());
        try 
        {
            Debug.Log(orderDataList.days[day] == null);
            return orderDataList.days[day] != null ? orderDataList.days[day] : orderDataList.days[0];
        }
        catch
        {
            Debug.Log("could not load orders for day " + day);
            return orderDataList.days[0];
        }
    }

    private EventCard ParseEventCard(int day)
    {
        EventCardList eventCardList = JsonConvert.DeserializeObject<EventCardList>(eventJson.ToString());
        try
        {
            return eventCardList.eventCards[day] != null ? eventCardList.eventCards[day] : eventCardList.eventCards[0];
        }
        catch
        {
            Debug.Log("could not event for day " + day);
            return eventCardList.eventCards[0];
        }
    }

    public OrderData GetOrderData()
    {
        return orderData;
    }

    public EventCard GetEventCard()
    {
        return eventCard;
    }    
}
