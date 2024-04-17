using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    const int MAX = -27;
    const int MIN = -740;
    
    int maxXp;
    int currentXp;


    public void SetMaxXp(int xp)
    {
        maxXp = xp;
    }

    public void SetXp(int xp)
    {
        currentXp = xp;
    }

    public void Render()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2((MIN + Math.Abs((((float)currentXp / maxXp) * (MIN - MAX)))), 0);
    }
}
