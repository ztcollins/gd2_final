using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHandler : MonoBehaviour
{
    int MAX_PAGE_NO = 10; //arbitrary, depends on data of user and pages unlocked
    public int pageNo = 0;

    bool isActive = false;

    public GameObject book;

    void Awake()
    {
        SetPage(pageNo);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isActive = !isActive;
            book.SetActive(isActive);
            //to be improved with animations
        }
    }

    public void SetPage(int pageNo)
    {
        Debug.Log(pageNo);
        if(pageNo >= MAX_PAGE_NO) //greater than max
        {
            book.transform.GetChild(1).gameObject.SetActive(false);
            book.transform.GetChild(3).gameObject.SetActive(true);
            book.transform.GetChild(5).gameObject.SetActive(false);
            this.pageNo = MAX_PAGE_NO;
        }
        else if(pageNo <= 0) //less than min
        {
            book.transform.GetChild(3).gameObject.SetActive(false);
            book.transform.GetChild(6).gameObject.SetActive(false);
            this.pageNo = 0;
        }
        else
        {
            book.transform.GetChild(1).gameObject.SetActive(true);
            book.transform.GetChild(3).gameObject.SetActive(false);
            book.transform.GetChild(5).gameObject.SetActive(true);
            book.transform.GetChild(6).gameObject.SetActive(true);
            this.pageNo = pageNo;
        }
    }

    public void NextPage()
    {
        SetPage(++pageNo);
        Debug.Log("next page");
    }

    public void PrevPage()
    {
        SetPage(--pageNo);
        Debug.Log("prev page");
    }
}
