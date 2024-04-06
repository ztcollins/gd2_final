using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{
    private float money;
    private int day;
    private Image selectedButton;

    #region References
        [SerializeField] TextMeshProUGUI moneyText;
        [SerializeField] TextMeshProUGUI dayText;
        [SerializeField] Image newsButton;
        [SerializeField] Image shopButton;
        [SerializeField] GameObject newsPanel;
        [SerializeField] GameObject shopPanel;
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

    public void NewsClicked()
    {
        newsPanel.SetActive(true);
        shopPanel.SetActive(false);
        UnselectButton();
        SelectButton(newsButton);

    }

    public void ShopClicked()
    {
        shopPanel.SetActive(true);
        newsPanel.SetActive(false);
        UnselectButton();
        SelectButton(shopButton);
    }

    public void SelectButton(Image buttonToSelect)
    {
        if(selectedButton != buttonToSelect)
        {
            buttonToSelect.color = Color.white;
            selectedButton = buttonToSelect;
        }
        else
        {
            UnselectButton();
            ClosePanels();
            selectedButton = null;
        }

    }

    public void UnselectButton()
    {
        if(selectedButton)
        {
            selectedButton.color = new Color32(89, 89, 93, 255);
        }
    }

    public void ClosePanels()
    {
        shopPanel.SetActive(false);
        newsPanel.SetActive(false);
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
