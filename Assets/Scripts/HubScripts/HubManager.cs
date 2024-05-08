using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using JetBrains.Annotations;
using UnityEngine.TextCore.Text;

public class HubManager : MonoBehaviour
{
    private float money;
    private float tuition;
    private int day;
    private Dictionary<string, int> items;
    private Dictionary<string, bool> upgrades;
    private Image selectedButton;
    EventCard eventCard;

    #region References
        [SerializeField] Image newsButton;
        [SerializeField] Image shopButton;
        [SerializeField] Image debtButton;
        [SerializeField] GameObject newsPanel;
        [SerializeField] GameObject shopPanel;
        [SerializeField] GameObject debtPanel;
        [SerializeField] GameObject itemsContent;
        [SerializeField] GameObject upgradesContent;
        [SerializeField] TMP_FontAsset itemsFont;
        [SerializeField] TextMeshProUGUI newsTitle;
        [SerializeField] TextMeshProUGUI newsDescription;
        [SerializeField] TextMeshProUGUI tuitionAmount;
    #endregion

    public void Awake()
    {
        Refresh();
    }

    public void Refresh()
    {
        money = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().GetMoney();
        tuition = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().GetTuition();
        day = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().GetDay();
        items = GameObject.FindWithTag("ItemHandler").GetComponent<ItemHandler>().GetItems();
        upgrades = GameObject.FindWithTag("ItemHandler").GetComponent<ItemHandler>().GetUpgrades();


        eventCard = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().GetEventCard();
        GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().VisualizeCurrentValues();
        tuitionAmount.text = "$" + tuition.ToString("F2");
        SetItems(items);
        SetUpgrades(upgrades);
    }

    public void NewsClicked()
    {
        newsPanel.SetActive(true);
        debtPanel.SetActive(false);
        shopPanel.SetActive(false);
        UnselectButton();
        SelectButton(newsButton);

        newsTitle.text = eventCard.cardTitle;
        newsDescription.text = eventCard.cardDescription;
    }

    public void ShopClicked()
    {
        shopPanel.SetActive(true);
        debtPanel.SetActive(false);
        newsPanel.SetActive(false);
        UnselectButton();
        SelectButton(shopButton);
    }

    public void DebtClicked()
    {
        debtPanel.SetActive(true);
        shopPanel.SetActive(false);
        newsPanel.SetActive(false);
        UnselectButton();
        SelectButton(debtButton);
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
            selectedButton.color = new Color32(98, 98, 98, 255);
        }
    }

    public void ClosePanels()
    {
        shopPanel.SetActive(false);
        newsPanel.SetActive(false);
        debtPanel.SetActive(false);
    }

    public void BuyItem(string itemUnparsed)
    {
        money = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().GetMoney();
        string[] splitString = itemUnparsed.Split(",");
        string itemToBuy = splitString[0];
        float price = float.Parse(splitString[1]);
        
        if(money >= price)
        {
            // subtract money
            money -= price;
            GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().SetMoney(money);

            // add new item
            GameObject.FindWithTag("ItemHandler").GetComponent<ItemHandler>().AddGeneric(itemToBuy);
            this.Refresh();
        }
        else
        {
            // cant buy
            Debug.Log("not enough money to buy " + itemToBuy + "!");
        }

    }

    public void BuyUpgrade(string upgradeUnparsed)
    {
        money = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().GetMoney();
        string[] splitString = upgradeUnparsed.Split(",");
        string upgradeToBuy = splitString[0];
        float price = float.Parse(splitString[1]);
        
        if(money >= price)
        {
            // subtract money
            money -= price;
            GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().SetMoney(money);

            // add new item
            GameObject.FindWithTag("ItemHandler").GetComponent<ItemHandler>().AddUpgrade(upgradeToBuy);
            this.Refresh();
        }
        else
        {
            // cant buy
            Debug.Log("not enough money to buy " + upgradeToBuy + "!");
        }

    }

    public void PayTuition(float payment)
    {
        money = GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().GetMoney();

        if(money >= payment)
        {
            // subtract money
            money -= payment;
            GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().SetMoney(money);

            // subtract payment from tuition
            tuition -= payment;
            GameObject.FindWithTag("StatsHandler").GetComponent<StatsHandler>().SetTuition(tuition);
            this.Refresh();

            if(tuition <= 0)
            {
                // win game
                Debug.Log("you win!");
            }
        }
        else
        {
            // cant pay
            Debug.Log("not enough money to pay " + payment + " for tuition!");
        }
    }

    public void SetItems(Dictionary<string, int> items)
    {
        //remove old items
        for(var i = itemsContent.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(itemsContent.transform.GetChild(i).gameObject);
        }

        //set new items
        foreach(var item in items.Keys)
        {
            GameObject itemObject = new GameObject(item);
            itemObject.transform.parent = itemsContent.transform;

            TextMeshProUGUI newText = itemObject.AddComponent<TextMeshProUGUI>();
            newText.text = item + ": " + items[item];
            newText.fontSize = 24;
            newText.color = Color.white;
            newText.alpha = 255;
            newText.font = this.itemsFont;
        }
    }

    public void SetUpgrades(Dictionary<string, bool> upgrades)
    {
        //remove old upgrades
        for(var i = upgradesContent.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(upgradesContent.transform.GetChild(i).gameObject);
        }

        //set new items
        foreach(var upgrade in upgrades.Keys)
        {
            GameObject upgradeObject = new GameObject(upgrade);
            upgradeObject.transform.parent = upgradesContent.transform;

            TextMeshProUGUI newText = upgradeObject.AddComponent<TextMeshProUGUI>();
            newText.text = upgrade;
            newText.fontSize = 24;
            newText.color = Color.white;
            newText.alpha = 255;
            newText.font = this.itemsFont;
        }
    }

}
