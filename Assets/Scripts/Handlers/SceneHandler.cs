using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    // TO USE:
    // Add a CustomButton.cs script to a button object, and change the sceneToLoad field to the "SceneName" you want to load
    // We do it this way to we can keep persistent objects (handlers) between scenes and the button can still call correct
    // function on runtime without reference on compile time
    public string currentScene;
    public string startScene;

    #region References
        [SerializeField] private UIHandler UIHandler;
    #endregion
    void Awake()
    {
        ChangeScene(startScene);
    }
   
    public void UseInstruction(SceneHandlerInstruction instruction, string instructionText)
    {
        switch(instruction)
        {
            case(SceneHandlerInstruction.EXIT):
                //savedata?
                Exit();
                break;
            case(SceneHandlerInstruction.CHANGESCENE):
                ChangeScene(instructionText);
                break;
            case(SceneHandlerInstruction.CONTINUE):
                ChangeScene("Hub");
                break;
            case(SceneHandlerInstruction.NEWGAME):
                GameObject.FindWithTag("DataHandler").GetComponent<DataPersistenceManager>().CreateNewSave();
                ChangeScene("Hub"); //change to intro animation or something eventually?
                break;
            case(SceneHandlerInstruction.FINISHORDER):
                GameObject.FindWithTag("OrderHandler").GetComponent<OrderHandler>().SetOrderComplete(true);
                GameObject.FindWithTag("SceneHandler").GetComponent<SceneHandler>().UseInstruction(SceneHandlerInstruction.CHANGESCENE, "LobbyScene");
                break;
            default:
                Debug.Log("Instruction invalid or unspecified or something");
                return;
        }
    }

    void ChangeScene(string sceneName)
    {
        if(sceneName == null) 
        {
            Debug.Log("sceneName is null!");
            return;
        }
        
        SceneManager.LoadScene(sceneName);
        currentScene = sceneName;
        //BroadcastMessage("OnSceneChange", currentScene); MAY WANT TO EXPAND TO THIS, HAS SOME PROBLEMS RIGHT NOW THO
        UIHandler.OnSceneChange(currentScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
