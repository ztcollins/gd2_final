using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IDataPersistence
{

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dayText;
    private float money;
    private int day;

    public void Awake()
    {
        Debug.Log("entering awake.");
        GameObject.Find("SceneManager").GetComponent<DataPersistenceManager>().BeginLoading();
        Debug.Log("Money = " + money);
        Debug.Log("Day = " + day);
    }

    public void LoadData(GameData data)
    {
        Debug.Log("Loading main menu data");
        this.money = data.money;
        this.day = data.day;
        SetMoney(this.money);
        SetDay(this.day);
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("Saving main menu data");
        data.money = this.money;
        data.day = this.day;
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
        SceneManager.LoadScene("lobbyScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
