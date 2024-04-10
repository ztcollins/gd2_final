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
        // expand this out
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/temp/perfume");
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
        this.itemCount--;
        SetCounter(this.itemCount);

        // item effects here (expand)
        if(this.itemName == "cheap perfume")
        {
            GameObject.Find("CandleGameManager").GetComponent<CandleGameManager>().ChangeRisk(20, false);
        }
    }

    public void SetCounter(int newNumber)
    {
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "x" + newNumber;
    }

}
