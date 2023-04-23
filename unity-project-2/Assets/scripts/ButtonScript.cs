using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void startgame()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void starttutorial()
    {
        SceneManager.LoadScene("tutorialSkeleton");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("MenuSkeleton");
    }

    public void creditsButton()
    {
        SceneManager.LoadScene("CreditsButton");
    }


    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }

}
