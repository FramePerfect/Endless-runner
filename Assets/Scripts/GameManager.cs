using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Drag and drop connection for Hierarchy
    public GameObject pauseMenu;
    public GameObject scoreMenu;
    //public TMP_Text finalScore;
    private PlayerScore playerScore;
    public TMP_Text score1txt;
    public TMP_Text score2txt;
    public TMP_Text score3txt;
    public TMP_Text score4txt;
    public TMP_Text score5txt;
    public Button resumebtn;

    private void Start()
    {
        playerScore = gameObject.GetComponent<PlayerScore>();
    }
    private void Update()
    {
        pauseButtonPress();
    }
    public void pauseButtonPress()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            showPauseMenu();
            pauseGame();
        }
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        hidePauseMenu();
        
    }

    public void showPauseMenu()
    {
        pauseMenu.SetActive(true);
    }
    public void showScoreMenu()
    {
        scoreMenu.SetActive(true);
    }
    public void hidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
    public void hideScoreMenu()
    {
        scoreMenu.SetActive(false);
    }
    public void gameOver()
    {
        Time.timeScale = 0;
        showPauseMenu();
        UpdateScores();
        resumebtn.interactable = false;
    }
    public void displayScores()
    {
        hidePauseMenu();
        showScoreMenu();
    }
    public void BackToMain()
    {
        hideScoreMenu();
        showPauseMenu();
    }
    public void showScores()
    {
        if(playerScore.getScore() > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", playerScore.getScore());
            Debug.Log("New highscore");
        }
       // finalScore.text = "Final Score: " + playerScore.getScore().ToString();
    }
    public void UpdateScores()
    {
        if(playerScore.getScore() > PlayerPrefs.GetInt("Score 1"))
        {
            PlayerPrefs.SetInt("Score 1",playerScore.getScore());
        }
        else if(playerScore.getScore() > PlayerPrefs.GetInt("Score 2"))
        {
            PlayerPrefs.SetInt("Score 2",playerScore.getScore());
        }
        else if(playerScore.getScore() > PlayerPrefs.GetInt("Score 3"))
        {
            PlayerPrefs.SetInt("Score 3",playerScore.getScore());
        }
        else if(playerScore.getScore() > PlayerPrefs.GetInt("Score 4"))
        {
            PlayerPrefs.SetInt("Score 4",playerScore.getScore());
        }
        else if(playerScore.getScore() > PlayerPrefs.GetInt("Score 5"))
        {
            PlayerPrefs.SetInt("Score 5",playerScore.getScore());
        }
        score1txt.text = PlayerPrefs.GetInt("Score 1").ToString();
        score2txt.text = PlayerPrefs.GetInt("Score 2").ToString();
        score3txt.text = PlayerPrefs.GetInt("Score 3").ToString();
        score4txt.text = PlayerPrefs.GetInt("Score 4").ToString();
        score5txt.text = PlayerPrefs.GetInt("Score 5").ToString(); 
    }
}