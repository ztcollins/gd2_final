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
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        ChangeScene(startScene);
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
