using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrderData
{
    public int dayNo;
    public float[] typeValues;
    public float[] colorValues;
    public float[] sizeValues;
    public float bonusMult;
    public float difficulty;
    public float orderFrequency;
    public string debugInfo;

    public string ToString()
    {
        return debugInfo;
    }

}

[System.Serializable]
public class OrderDataList
{
    public OrderData[] days;
}
