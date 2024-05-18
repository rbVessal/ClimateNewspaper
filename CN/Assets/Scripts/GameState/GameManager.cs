using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action StartDay;
    [SerializeField] private int dayNumber = -1;
    [SerializeField] public int articlesToAddPerDay = 4;

    // Start is called before the first frame update
    void Start()
    {
        //StartNewDay();
        
    }
    
    public void StartNewDay()
    {
        dayNumber += 1;
        //allocate articles on billboard
        if(dayNumber==0) FindObjectOfType<ArticleManager>().ChooseTutorialArticles(); // if its tutorial day
        else FindObjectOfType<ArticleManager>().ChooseArticlesRandomlyForBillboard(articlesToAddPerDay);

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
}
