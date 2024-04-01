using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//MAKE SURE TO CHANGE SPRITE IMPORT SETTINGS ADVANCED>READ/WRITE ON

public class CustomButtonHitbox : MonoBehaviour
{
    void Awake()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }
}
