using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    public string currentScene;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        ChangeScene("MainMenu");
    }
   
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        currentScene = sceneName;
    }

    public void ChangeScene(string sceneName, int transitionDuration)
    {
        Debug.Log("TODO");
    }
}
