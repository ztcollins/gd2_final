using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float money;
    public int day;
    public int reputationLevel;
    public int currentXP;
    public int requiredXP;
    public Dictionary<string, int> items;

    // initalize values for new game
    public GameData()
    {
        this.money = 0;
        this.day = 1;
        this.reputationLevel = 1;
        this.currentXP = 0;
        this.requiredXP = 100;
        this.items = new Dictionary<string, int>
        {
            // starting items
            { "candles", 50 },
        };
    }
}
