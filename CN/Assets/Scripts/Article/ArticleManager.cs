using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArticleManager : MonoBehaviour
{

    [Tooltip("Articles for the entire game")]
    [SerializeField] private List<ArticleScriptableObject> allArticles;

    private List<ArticleScriptableObject> leftOverArticles;

    [Tooltip("Articles that go to the billboard")]
    [SerializeField] List<ArticleScriptableObject> billBoardArticles;

    [Tooltip("Articles that are on the PC")] 
    [SerializeField] private List<ArticleScriptableObject> computerArticles;

    [SerializeField] private int billBoardArticleMax;
    [SerializeField] private int computerArticleMax;


    private BillBoardArticleDisplayer billBoard;
    //TODO: Add PC display

    // Start is called before the first frame update
    void Start()
    {
        billBoard = FindObjectOfType<BillBoardArticleDisplayer>();
        leftOverArticles=new List<ArticleScriptableObject>(allArticles);
        //TODO add PC reference so it can send data to there

        ChooseArticlesRandomlyForBillboard(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    //randomly add articles to the billboard
    void ChooseArticlesRandomlyForBillboard(int numberToAdd = 1)
    {
        for (int i = 1; i <= numberToAdd; i++)
        {
            ArticleScriptableObject article = leftOverArticles[Random.Range(0, leftOverArticles.Count)];
            leftOverArticles.Remove(article);
            AddToBillBoard(article);
        }
    }


    //called when the day starts to add articles to the ones on the billboard
    //can be used for just adding back from the computer as well
    public void AddToBillBoard(ArticleScriptableObject article)
    {
        if (billBoardArticles.Count <= billBoardArticleMax)
        {
            billBoardArticles.Add(article);
            billBoard.AddArticleToDisplay(article);
        }
        else
        {
            Debug.Log("Max bill board articles reached! No space available.");
        }
    }

    public bool RemoveFromBillBoard(ArticleScriptableObject article)
    {
        return billBoardArticles.Remove(article);
    }

    
    public bool AddToComputer(ArticleScriptableObject article)
    {
        if (computerArticles.Count < computerArticleMax)
        {
            computerArticles.Add(article);
            //TODO add to the actual computer class
            return true;
        }
        else
        {
            Debug.Log("Maximum articles reached! No space available.");
            return false;
        }
        
    }

    public bool RemoveFromComputer(ArticleScriptableObject article)
    {

        if (computerArticles.Remove(article))
        {
            //TODO remove from the actual computer class
            return true;
        }
        else
        {
            Debug.Log("Article does not exist on computer!");
            return false;
        }
    }
}

