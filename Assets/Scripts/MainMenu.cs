using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewDay()
    {
        SceneManager.LoadScene("lobbyScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
