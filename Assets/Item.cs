using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private string itemName;
    private int itemCount;

    public void SetItem(string itemName, int itemCount)
    {
        this.itemName = itemName;
        this.itemCount = itemCount;
        
        SetCounter(this.itemCount);
        GetComponent<Image>().sprite = GetSprite(itemName);
    }

    public string GetName()
    {
        return this.itemName;
    }

    public int GetCount()
    {
        return this.itemCount;
    }

    public void Useitem()
    {
        if(this.itemCount > 0)
        {
            this.itemCount--;
            SetCounter(this.itemCount);

            // item effects here (expand)
            if(this.itemName == "cheap perfume")
            {
                GameObject.Find("CandleGameManager").GetComponent<CandleGameManager>().ChangeRisk(20, false);
            }
        }
    }

    public void SetCounter(int newNumber)
    {
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "x" + newNumber;
    }

    public Sprite GetSprite(string itemName)
    {
        if(itemName == "cheap perfume")
        {
            return Resources.Load<Sprite>("Art/temp/perfume");
        }
        else if(itemName == "holy water")
        {
            return Resources.Load<Sprite>("Art/temp/holyWaterTempAsset");
        }
        else
        {
            return Resources.Load<Sprite>("Art/Sprites/placeholder");
        }
    }

}
