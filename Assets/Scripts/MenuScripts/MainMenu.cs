using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dayText;
    private float money;
    private int day;

    public void Awake()
    {
        this.money = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().Money;
        this.day = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().Day;
        SetMoney(this.money);
        SetDay(this.day);
    }

    public void SetDay(int value)
    {
        dayText.text = value.ToString();
    }

    public void SetMoney(float value)
    {
        moneyText.text = value.ToString("F2");
    }

    public void StartNewDay()
    {
        SceneManager.LoadScene("LobbyScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
