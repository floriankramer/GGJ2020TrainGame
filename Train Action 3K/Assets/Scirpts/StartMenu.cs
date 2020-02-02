using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartMen()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void WatchTutorial()
    {
        SceneManager.LoadScene(2);
    }
    public void CheckCredits()
    {
        SceneManager.LoadScene(3);
    }
    public void CheckReferences()
    {
        SceneManager.LoadScene(4);
    }
    public void Retry()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
