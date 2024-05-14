using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private int dayNumber=1;
    [SerializeField] private int articlesToAddPerDay;

    // Start is called before the first frame update
    void Start()
    {
        //StartNewDay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void StartNewDay()
    {
        //begin at the billboard
        GameStateManager.Main.ChangeStateToBillboard();
        
        //allocate articles on billboard
        FindObjectOfType<ArticleManager>().ChooseArticlesRandomlyForBillboard(articlesToAddPerDay);
        
        //pop up any text menus, other stuff if required
        
    }
}
