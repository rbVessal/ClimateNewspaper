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

    public int count = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddArticleToDisplay(ArticleScriptableObject article)
    {
        foreach (var display in displays)
        {
            if (display.added)
            {
                display.article = article;
                display.ResetButtons();
                break;
            }
            else if (display.article == emptyArticle)
            {
                display.article = article;
                display.ResetButtons();
                count++;
                break;
            }
        }
        
    }

    public void SetArticles(List<ArticleScriptableObject> articles)
    {
        int i = 0;
        foreach (var article in articles)
        {
            displays[i].article = article;
            i++;
        }

        for (; i < displays.Count; i++)
        {
            displays[i].article = emptyArticle;
        }
    }

    public void ResetButtons()
    {
        foreach (var display in displays)
        {
            display.ResetButtons();
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
