using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuiButtonHandler : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        //Can only do this because the GameManager script is attached to the same object
        //as this script.
        gameManager = GetComponent<GameManager>();
    }
    public void loadGame()
    {
        //reload game
        SceneManager.LoadScene("Game Scene");
        Time.timeScale = 1;
        
    }

    public void resumeGame()
    {
        gameManager.resumeGame();
    }

    public void exitGame()
    {
        //this only works on a build
        Application.Quit();
    }

    public void displayScores()
    {
        gameManager.displayScores();
    }



    //Score Screen buttons


    public void BackToMain()
    {
        gameManager.BackToMain();
    }
}
