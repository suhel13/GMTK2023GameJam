using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_manager : MonoBehaviour
{

    public GameObject mainMenuPanel;
    public GameObject InGamePanel;
    public GameObject DiePanel;
    public GameObject PausePanel;
    public GameObject DescriptionPanel;

    public enum gameState { mainMenu, play, paused, endPanel}
    public gameState state;

    public TMPro.TextMeshProUGUI score;
    public TMPro.TextMeshProUGUI end_score;

    // Start is called before the first frame update
    private void Awake()
    {
        state = gameState.mainMenu;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + GameManager.Instance.score;
    }

    public void exit()
    {
        Application.Quit();
    }

    public void startGame()
    {
        Time.timeScale = 1;
        mainMenuPanel.SetActive(false);
        state = gameState.play;
    }

    public void showEndPanel()
    {
        InGamePanel.SetActive(false);
        DiePanel.SetActive(true);
        end_score.text = "Your Score " + GameManager.Instance.score;
        state = gameState.endPanel;
        
    }

    public void playAgain()
    {
        GameManager.Instance.score = 0;
        InGamePanel.SetActive(true);
        DiePanel.SetActive(false);
        state = gameState.play;
        GameManager.Instance.restartGame();
    }

    public void pauseGame ()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        state = gameState.paused;
    }

    public void continueGame()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        state = gameState.play;
    }

    public void esc()
    {
        if (state == gameState.paused)
            continueGame();
        else if (state == gameState.play)
            pauseGame();
    }

    public void showDescription ()
    {
        DescriptionPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void hideDescription()
    {
        DescriptionPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

}
