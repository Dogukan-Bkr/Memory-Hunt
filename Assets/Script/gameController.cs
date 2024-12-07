using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    int firstValue, rnSucces,randomNumber,createNumber,totalSpriteNumber;
    public int winCondition;
    GameObject btn;
    GameObject chosenBtn;
    public Sprite defaultSprite;
    public AudioSource[] audios;
    public GameObject[] buttons;
    public TextMeshProUGUI time;
    public GameObject[] endGamePanels;
    public Slider timeSlider;
    
    public float totalTime;
    float min,sec,remainTime;
    bool timer;

    public GameObject Grid;
    public GameObject Pool;
    bool createStatus;

    void Start()
    {
        
        firstValue = 0;
        timer = true;
        createStatus = true;
        remainTime = 0;
        createNumber = 0;
        totalSpriteNumber = Pool.transform.childCount;
        timeSlider.value = remainTime;
        timeSlider.maxValue = totalTime;

        StartCoroutine(createRandomSprite());

        
    }

    private void Update()
    {
        if(timer && totalTime > 0.01f && remainTime!=totalTime) 
        {
            remainTime += Time.deltaTime;
            timeSlider.value = remainTime;
            if(timeSlider.maxValue == timeSlider.value)
            {
                timer = false;
                gameOver();
            }

            totalTime -= Time.deltaTime;
            min = Mathf.FloorToInt(totalTime / 60);
            sec = Mathf.FloorToInt(totalTime % 60);

            time.text = string.Format("{0:00}:{1:00}", min, sec);
        }
        else
        {
            timer = false;
            gameOver();
        }
        
    }


    IEnumerator createRandomSprite()
    {
        yield return new WaitForSeconds(.1f);
        while (createStatus)
        {
            randomNumber = Random.Range(0, Pool.transform.childCount - 1);
            if(Pool.transform.GetChild(randomNumber).gameObject != null)
            {
                Pool.transform.GetChild(randomNumber).transform.SetParent(Grid.transform);
                createNumber++;
                if(createNumber == totalSpriteNumber)
                {
                    createStatus = false;
                    Destroy(Pool.gameObject);
                }
            }
            
        }


    }

    public void stopGame()
    {
        endGamePanels[2].SetActive(true);
        Time.timeScale = 0;
    }
    public void continueGame()
    {
        endGamePanels[2].SetActive(false);
        Time.timeScale = 1;
    }
    void gameOver()
    {
        endGamePanels[0].SetActive(true);
    }
    void win()
    {
        endGamePanels[1].SetActive(true);
    }
    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void realObj(GameObject obj)
    {
        btn = obj;
        btn.GetComponent<Image>().sprite = btn.GetComponentInChildren<SpriteRenderer>().sprite;
        btn.GetComponent<Image>().raycastTarget = false;
        audios[0].Play();
    }

    void buttonStatus(bool status)
    {
        foreach (var item in buttons)
        {
            if (item != null) 
            {
                item.GetComponent<Image>().raycastTarget = status;
            }
            
        }
    }
    public void clickButton(int value)
    {
        
        control(value);

    }

    void control(int value)
    {
        if (firstValue == 0)
        {
            firstValue = value;
            chosenBtn = btn;
        }
        else
        {
            StartCoroutine(checkIt(value));
        }
    }

    IEnumerator checkIt(int value)
    {
        buttonStatus(false);
        yield return new WaitForSeconds(1);

        if (firstValue == value)
        {
            rnSucces++;
            chosenBtn.GetComponent<Image>().enabled = false;
            btn.GetComponent<Image>().enabled = false;
            firstValue = 0;
            buttonStatus(true);
            if(winCondition == rnSucces)
            {
                win();
            }
        }
        else
        {
            audios[1].Play();
            chosenBtn.GetComponent<Image>().sprite = defaultSprite;
            btn.GetComponent<Image>().sprite = defaultSprite;
            firstValue = 0;
            chosenBtn = null;
            buttonStatus(true);
        }
    }

}
