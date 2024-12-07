using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    public GameObject exitPanel;

    private void Start()
    {
        if(Time.timeScale == 0) { Time.timeScale = 1; }
        
    }

    public void exitGame()
    {
        exitPanel.SetActive(true);
        
    }
    public void status(string status)
    {
        if(status == "Yes")
        {
            Application.Quit();
        }
        else
        {
            exitPanel.SetActive(false);
        }
    }
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
}
