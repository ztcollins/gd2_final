using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class BookHandler : MonoBehaviour
{
    public int pageNo = 0; //relatively unimportant for actual logic. pageArrayIndex is more important
    public List<int> pageArray;
    public int pageArrayIndex = 0;
    public bool isActive = false;

    //page header number
    int TYPEPAGENO = 1;
    int COLORPAGENO = 9;
    int SIZEPAGENO = 19;

    int PAGECOUNT = 25;

    #region References
        [SerializeField] private Animator bookAnimator;
        [SerializeField] private Image pageImage;
        [SerializeField] private GameObject bookCanvas;
        [SerializeField] private GameObject rightPage;
        [SerializeField] private GameObject tornPage;
        [SerializeField] private GameObject nextPage;
        [SerializeField] private GameObject previousPage;
        [SerializeField] private GameObject typeBookmarkR;
        [SerializeField] private GameObject sizeBookmarkR;
        [SerializeField] private GameObject colorBookmarkR;
        [SerializeField] private GameObject typeBookmarkL;
        [SerializeField] private GameObject sizeBookmarkL;
        [SerializeField] private GameObject colorBookmarkL;
        [SerializeField] private Image summonBookButtonImage;     
        [SerializeField] private Sprite summonBookUp;
        [SerializeField] private Sprite summonBookDown;
    #endregion

    void Awake()
    {
        LoadPageArray();
        SetPage(pageArray[0]);
        bookAnimator.SetBool("summon", false);
        isActive = false;
    }

    void LoadPageArray()
    {
        pageArray = new List<int>();
        for(int i = 0; i < PAGECOUNT; i++)
        {
            pageArray.Add(i);
        }
        pageArray.Add(999);
        pageArray.Sort();
    }

    public void BookmarkType()
    {
        SetPage(TYPEPAGENO);
    }

    public void BookmarkSize()
    {
        SetPage(SIZEPAGENO);
    }

    public void BookmarkColor()
    {
        SetPage(COLORPAGENO);
    }

    public void SetPage(int pageIndex)
    {
        pageArrayIndex = pageIndex;
        if(pageArrayIndex == pageArray.Count - 1) //greater than max
        {
            SetBookState(BookState.MAX);
            SetPageImage(999);
            pageNo = 999;
        }
        else if(pageArrayIndex <= 0) //less than min
        {
            SetBookState(BookState.MIN);
            SetPageImage(0);
            pageNo = 0;
        }
        else
        {
            SetBookState(BookState.DEFAULT);
            SetPageImage(pageArray[pageArrayIndex]);
            pageNo = pageArray[pageArrayIndex];
        }
        SetBookmarks(pageNo);
    }

    private void SetBookState(BookState bookState)
    {
        switch(bookState)
        {
            case(BookState.MIN):
                rightPage.SetActive(true);
                tornPage.SetActive(false);
                nextPage.SetActive(true);
                previousPage.SetActive(false);
                break;
            case(BookState.DEFAULT):
                rightPage.SetActive(true);
                tornPage.SetActive(false);
                nextPage.SetActive(true);
                previousPage.SetActive(true);
                break;
            case(BookState.MAX):
                rightPage.SetActive(false);
                tornPage.SetActive(true);
                nextPage.SetActive(false);
                previousPage.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void SetBookmarks(int pageNo)
    {
        typeBookmarkL.SetActive(pageNo >= TYPEPAGENO);
        typeBookmarkR.SetActive(!(pageNo >= TYPEPAGENO));

        sizeBookmarkL.SetActive(pageNo >= SIZEPAGENO);
        sizeBookmarkR.SetActive(!(pageNo >= SIZEPAGENO));

        colorBookmarkL.SetActive(pageNo >= COLORPAGENO);
        colorBookmarkR.SetActive(!(pageNo >= COLORPAGENO));
    }

    public void NextPage()
    {
        if(++pageArrayIndex >= pageArray.Count) pageArrayIndex = pageArray.Count - 1;

        SetPage(pageArrayIndex);
    }

    public void PrevPage()
    {
        if(--pageArrayIndex <= 0) pageArrayIndex = 0;

        SetPage(pageArrayIndex);
    }

    public void SummonBook()
    {
        if(isActive)
        {
            bookAnimator.SetBool("summon", false);
            summonBookButtonImage.sprite = summonBookUp;
        }
        else
        {
            bookAnimator.SetBool("summon", true);
            summonBookButtonImage.sprite = summonBookDown;
        }
        isActive = !isActive;
    }

    public void SetPageImage(int pageNumber)
    {
        pageImage.sprite = Resources.Load<Sprite>("Art/Book/Pages/" + pageNumber);
    }

    public void ClearPage()
    {
        pageImage.sprite = Resources.Load<Sprite>("Art/Book/Pages/999");
    }
}
