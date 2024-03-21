using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    Image pageImage;
    public Sprite tempSprite;
    public Sprite spriteSwag;

    public List<int> availablePages;

    void GetListPages()
    {
        availablePages = new List<int>();
        availablePages.Add(0);
        availablePages.Add(1);
        availablePages.Sort();
    }

    void Awake()
    {
        pageImage = gameObject.GetComponent<Image>();
        GetListPages();
    }
    public void LoadPage(int pageNo)
    {
        pageImage.sprite = Resources.Load<Sprite>("Art/Book/Pages/" + pageNo);
    }

    public void ClearPage()
    {
        pageImage.sprite = Resources.Load<Sprite>("Art/Book/Pages/-1");
    }
}
