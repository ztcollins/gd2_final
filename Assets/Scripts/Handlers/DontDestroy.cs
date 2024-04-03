using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //May need ID system if some objects have the same name
    void Awake()
    {
        Object[] objs = Object.FindObjectsOfType<DontDestroy>();
        for(int i = 0; i < objs.Length; i++)
        {
            if(objs[i] != this && objs[i].name == gameObject.name) 
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
