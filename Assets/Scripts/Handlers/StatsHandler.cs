using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsHandler : MonoBehaviour, IDataPersistence
{
    #region References
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI reputationLevelText;
    [SerializeField] private XpBar xpBar;
    #endregion
    public int day;
    public float money;
    public int reputationLevel;
    public int currentXP;
    public int requiredXP;
    
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
    
}
