using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    Dictionary<GameObject, string[]> availabilityDictionary;

    bool tutorial = false;

    #region References
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private BookHandler bookHandler;
        [SerializeField] private GameObject bookHandlerObj;
        [SerializeField] private GameObject summonBookButton;
        [SerializeField] private GameObject statsPanel;
        [SerializeField] private GameObject debugMenu;
        [SerializeField] private DialogueHandler dialogueHandler;
        [SerializeField] private TutorialHandler tutorialHandler;
        [SerializeField] private Pointer pointer;
        [SerializeField] private GameObject reputationBarObj;
        [SerializeField] private Image black;
        [SerializeField] private Animator animator;
        [SerializeField] private SceneHandler sceneHandler;
    #endregion

    void Awake()
    {
        ConstructDictionary();
    }

    void ConstructDictionary()
    {
        availabilityDictionary  = new Dictionary<GameObject, string[]>
        {
            {bookHandlerObj, new string[] {"Hub", "CandleScene", "LobbyScene"}},
            {summonBookButton, new string[] {"Hub", "CandleScene", "LobbyScene"}},
            {statsPanel, new string[] {"Hub", "LobbyScene"}}
        };
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SummonBook();
        }
        if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && bookHandler.isActive)
        {
            bookHandler.PrevPage();
        }
        if((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && bookHandler.isActive)
        {
            bookHandler.NextPage();
        }
        if(Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Tilde)) //REMOVE BEFORE REAL BUILD!!!!!!!
        {
            debugMenu.SetActive(!debugMenu.activeSelf);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            tutorialHandler.AnimateArrow(PointerAnimationState.UNDULATE, reputationBarObj);
        }
    }

    public void SummonBook()
    {
        bookHandler.SummonBook();
    }

    public void FadeToBlack()
    {
        Debug.Log("FADE TO BLACK");
        animator.SetBool("SetFadeToBlack", true);
    }

    public void FadeFromBlack()
    {
        Debug.Log("FADE FREOM BLACK");
        animator.SetBool("SetFadeToBlack", false);
    }

    public void LoadScene()
    {
        sceneHandler.SceneChange();
        sceneHandler.ExecuteSceneChange();
        if(tutorial) tutorialHandler.StartTutorial();
    }

    public void StartTutorial()
    {
        tutorial = true;
    }

    public void OnSceneChange(string currentScene)
    {
        Debug.Log("Changed Scene");
        foreach (KeyValuePair<GameObject, string[]> kvp in availabilityDictionary)
        {
            if(kvp.Value.Contains(currentScene)) kvp.Key.SetActive(true);
            else kvp.Key.SetActive(false);
        }
    }

}
