using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_manager : MonoBehaviour
{

    public GameObject mainMenuPanel;

    public TMPro.TextMeshProUGUI score;
    // Start is called before the first frame update
   
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
        mainMenuPanel.SetActive(false);


    }

}
