using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void startgame()
    {
        SceneManager.LoadScene("Level_1");
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
        SceneManager.LoadScene("Credit Scene");
    }

    public void tutorialMap()
    {
        SceneManager.LoadScene("Tutorial_map");
    }


    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }

}
