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

    [SerializeField] private GameObject lossCanvas;
    [SerializeField] private GameObject gameMenuCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        //StartNewDay();
        lossCanvas.SetActive(false);
    }
    
    public void StartNewDay()
    {
        dayNumber += 1;
        //allocate articles on billboard
        if(dayNumber==0) FindObjectOfType<ArticleManager>().ChooseTutorialArticles(); // if its tutorial day
        else FindObjectOfType<ArticleManager>().ChooseArticlesRandomlyForBillboard();

        //begin at the billboard
        GameStateManager.Main.ChangeStateToBillboard(true);
        
        
        //pop up any text menus, other stuff if required
        
        //Send out day started event.
        StartDay?.Invoke();
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
