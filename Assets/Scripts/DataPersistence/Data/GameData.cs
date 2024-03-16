using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float money;
    public int day;

    // initalize values for new game
    public GameData()
    {
        this.money = 0;
        this.day = 1;
    }
}
