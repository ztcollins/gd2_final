using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float money;
    public int day;
    public Dictionary<string, int> items;

    // initalize values for new game
    public GameData()
    {
        this.money = 0;
        this.day = 1;
        this.items = new Dictionary<string, int>
        {
            // starting items
            { "candles", 100 },
        };
    }
}
