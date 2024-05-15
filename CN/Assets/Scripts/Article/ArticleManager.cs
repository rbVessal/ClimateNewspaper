using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArticleManager : MonoBehaviour
{

    [Tooltip("Articles for the entire game")]
    [SerializeField] private List<ArticleScriptableObject> allArticles;

    [SerializeField]
    private List<ArticleScriptableObject> leftOverArticles;

    [Tooltip("Articles that go to the billboard")]
    [SerializeField] List<ArticleScriptableObject> billBoardArticles;

    [Tooltip("Articles that are on the PC")] 
    [SerializeField] private List<ArticleScriptableObject> computerArticles;

    [SerializeField] private int billBoardArticleMax;

    [SerializeField] private BillBoardArticleDisplayer billBoard;

    private NewspaperManager newspaper;

    // Start is called before the first frame update
    void Start()
    {
        leftOverArticles=new List<ArticleScriptableObject>(allArticles);
        newspaper = FindObjectOfType<NewspaperManager>();
        
        //ChooseArticlesRandomlyForBillboard(0);
    }

    //randomly add articles to the billboard
    public void ChooseArticlesRandomlyForBillboard(int numberToAdd = 1)
    {

        for (int i = 1; i <= numberToAdd; i++)
        {
            ArticleScriptableObject article = leftOverArticles[Random.Range(0, leftOverArticles.Count)];
            if(AddToBillBoard(article))
                leftOverArticles.Remove(article);

        }
    }


    //called when the day starts to add articles to the ones on the billboard
    //can be used for just adding back from the computer as well
    public bool AddToBillBoard(ArticleScriptableObject article)
    {
        if (FindObjectOfType<BillBoardArticleDisplayer>().count < billBoardArticleMax)
        {
            billBoardArticles.Add(article);
            FindObjectOfType<BillBoardArticleDisplayer>().AddArticleToDisplay(article);
            return true;
        }
        else
        {
            Debug.Log("Max bill board articles reached! No space available.");
            return false;
        }
    }

    public bool RemoveFromBillBoard(ArticleScriptableObject article)
    {
        return billBoardArticles.Remove(article);
    }

    
    public bool AddToComputer(ArticleScriptableObject article)
    {
        NewspaperManager newspaperManager = FindAnyObjectByType<NewspaperManager>();
        if (newspaperManager != null)
        {
            if (computerArticles.Count < newspaperManager.GetMaxSlots())
            {
                computerArticles.Add(article);
                //TODO add to the actual computer class
                newspaper.AddArticle(article);
                return true;
            }
            else
            {
                Debug.Log("Maximum articles reached! No space available.");
                return false;
            }
        }

        return false;
    }

    public bool RemoveFromComputer(ArticleScriptableObject article)
    {

        if (computerArticles.Remove(article))
        {
            //TODO remove from the actual computer class
            newspaper.RemoveArticle(article);
            return true;
        }
        else
        {
            Debug.Log("Article does not exist on computer!");
            return false;
        }
    }

    public void ClearAllComputerArticles()
    {
        // TODO:  Send remaining computer articles back to bulletin board
        computerArticles.Clear();
    }

}

