using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubManager : MonoBehaviour
{
    private float money;
    private int day;

    #region References
        [SerializeField] TextMeshProUGUI moneyText;
        [SerializeField] TextMeshProUGUI dayText;
    #endregion

    public void Awake()
    {
        Refresh();
    }

    public void Refresh()
    {
        money = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().GetMoney();
        day = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().GetDay();
        SetMoney(money);
        SetDay(day);
    }

    public void SetDay(int value)
    {
        dayText.text = "Day " + value.ToString();
    }

    public void SetMoney(float value)
    {
        moneyText.text = "$" + value.ToString("F2");
    }
}
