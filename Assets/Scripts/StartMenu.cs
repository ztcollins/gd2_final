using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void NewGame()
    {
        GameObject.Find("SceneManager").GetComponent<DataPersistenceManager>().CreateNewSave();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("StartScreen");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("StartScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
