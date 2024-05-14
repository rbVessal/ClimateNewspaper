using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardArticleDisplayer : MonoBehaviour
{


    [Tooltip("The UI displaying the article")]
    public List<ArticleDisplay> displays;
    [Tooltip("The articles to be displayed")]
    public List<ArticleScriptableObject> articlesToDisplay;

    [Tooltip("Default article when nothing is to be displayed")]
    [SerializeField] private ArticleScriptableObject emptyArticle;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddArticleToDisplay(ArticleScriptableObject article)
    {
        foreach (var display in displays)
        {
            
            if (display.added|| display.article==emptyArticle)
            {
                display.article = article;
                display.ResetButtons();
                break;
            }
        }
        
    }
    
    
    
    //in case we want to remove articles through another script and not from the article display prefab itself
    public void RemoveArticleFromDisplay(int displayNumber)
    {
        displays[displayNumber].ChangeArticle(emptyArticle);
        
    }
    public void RemoveArticleFromDisplay(ArticleScriptableObject article)
    {
        foreach (var display in displays)
        {
            if (display.article == article)
            {
                display.article = emptyArticle;
            }
        }
    }

   
}
