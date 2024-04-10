using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private string itemName;

    public void SetItem(string itemName)
    {
        this.itemName = itemName;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Assets/Resources/Art/temp/perfume.jpg");
    }

    public string GetName()
    {
        return itemName;
    }

}
