using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventCard
{
    public int cardID;
    public string cardTitle;
    public string cardDescription;
    public float[] values;

    public string ToString()
    {
        string valuesString = "[";
        for(int i = 0; i < values.Length; i++)
        {
            valuesString += values[i].ToString("F2") + ",";
        }
        valuesString += "]";
        return "cardID: " + cardID + "\n" + "cardTitle: " + cardTitle + "\n" + "cardDescription: " + cardDescription + "\n" + "values: " + valuesString + "\n";
    }
}

[System.Serializable]
public class EventCardList
{
    public EventCard[] eventCards;
}
