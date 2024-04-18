using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrderData
{
    public int dayNo;
    public List<Dictionary<string, float>> type;
    public List<Dictionary<string, float>> color;
    public List<Dictionary<string, float>> size;
    public float bonusMult;
    public float difficulty;
    public float orderFrequency;
    public string debugInfo;

    public string ToString()
    {
        string returnString = "";
        returnString += "Day " + dayNo + " : " + debugInfo +"\n";
        returnString += "Types: \n[";
        for(int i = 0; i < type.Count; i++)
        {
            returnString += "\n";
            foreach(var kvp in type[i])
            {
                returnString += kvp.Key + ": " + kvp.Value;
            }
        }
        returnString += "]\n";
        returnString += "Colors: \n[";
        for(int i = 0; i < color.Count; i++)
        {
            returnString += "\n";
            foreach(var kvp in color[i])
            {
                returnString += kvp.Key + ": " + kvp.Value;
            }
        }
        returnString += "]\n";
        returnString += "Sizes: \n[";
        for(int i = 0; i < size.Count; i++)
        {
            returnString += "\n";
            foreach(var kvp in size[i])
            {
                returnString += kvp.Key + ": " + kvp.Value;
            }
        }
        returnString += "]\n";
        returnString += "Bonus Mult: " + bonusMult + ", Difficulty: " + difficulty + ", Order Frequency: " + orderFrequency + "\n";
        return returnString;
    }

}

[System.Serializable]
public class OrderDataList
{
    public OrderData[] days;
}
