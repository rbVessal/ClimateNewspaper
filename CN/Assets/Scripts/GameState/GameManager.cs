using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action StartDay;
    [SerializeField] private int dayNumber = -1;
    [SerializeField] public int articlesToAddPerDay = 4;
    [SerializeField] public int maxWeeks = 3;
    [SerializeField] public float winEnvScore = 40;

    [SerializeField] private GameObject lossCanvas;
    [SerializeField] private GameObject winEnvCanvas;
    [SerializeField] private GameObject loseEnvCanvas;
    [SerializeField] private GameObject gameMenuCanvas;
    [SerializeField] private GameObject economyCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        //StartNewDay();
        lossCanvas.SetActive(false);
    }
    
    public void StartNewDay()
    {
        dayNumber += 1;


        if (dayNumber < maxWeeks + 1)
        {
            //allocate articles on billboard
            if (dayNumber == 0) FindObjectOfType<ArticleManager>().ChooseTutorialArticles(); // if its tutorial day
            else FindObjectOfType<ArticleManager>().ChooseArticlesRandomlyForBillboard();

            //begin at the billboard
            GameStateManager.Main.ChangeStateToBillboard(true);


            //pop up any text menus, other stuff if required

            Debug.Log("Day Started");
            GameMenu gameMenu = gameMenuCanvas.GetComponent<GameMenu>();
            gameMenu.EnableNavigationButtons(true);
            gameMenu.EnableStartButton(false);

            //Send out day started event.
            StartDay?.Invoke();
        }
    }

    public bool TryShowEndScreen()
    {
        GameEconomy economy = FindObjectOfType<EconomyManager>().GetEconomy();
        if (economy.Money <= 0)
        {
            lossCanvas.SetActive(true);
            gameMenuCanvas.SetActive(false);
            economyCanvas.SetActive(false);

            return true;
        }

        if (dayNumber >= maxWeeks)
        {
            if (economy.ClimateImpact > winEnvScore)
            {
                winEnvCanvas.SetActive(true);
                gameMenuCanvas.SetActive(false);
                economyCanvas.SetActive(false);

                return true;
            }
            else
            {
                loseEnvCanvas.SetActive(true);
                gameMenuCanvas.SetActive(false);
                economyCanvas.SetActive(false);

                return true;
            }
        }

        return false;
    }

    public void EnableStartDayButton(bool enable)
    {
        GameMenu gameMenu = gameMenuCanvas.GetComponent<GameMenu>();
        gameMenu.EnableStartButton(enable);
    }

    public void SetDay(int newDayNumber)
    {
        dayNumber = newDayNumber;
    }

    public int GetDay() => dayNumber;

    public void CheckForLossCondition()
    {

        GameEconomy economy = FindObjectOfType<EconomyManager>().GetEconomy();
        Debug.Log(economy.Money);
        if (economy.Money <= 0)
        {
            lossCanvas.SetActive(true);
            gameMenuCanvas.SetActive(false);
            Debug.Log("Loss condition reached");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
