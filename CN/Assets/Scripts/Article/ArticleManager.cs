using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArticleManager : MonoBehaviour
{

    [Tooltip("Articles for the entire game")]
    [SerializeField] private List<ArticleScriptableObject> allArticles;
    
    [Tooltip("Ads for the entire game")]
    [SerializeField] private List<ArticleScriptableObject> allAdvertisements;
    
    [SerializeField] private List<ArticleScriptableObject> leftOverAdvertisements;

    [SerializeField] private List<ArticleScriptableObject> leftOverArticles;

    [Tooltip("Articles that go to the billboard")]
    [SerializeField] List<ArticleScriptableObject> billBoardArticles;

    [Tooltip("Articles that are on the PC")] 
    [SerializeField] private List<ArticleScriptableObject> computerArticles;

    [SerializeField] private List<ArticleScriptableObject> dayZeroArticles;

    [SerializeField] private int billBoardArticleMax;

    [SerializeField] private BulletinManager billBoard;

    private NewspaperManager newspaper;

    private void Awake()
    {
        leftOverArticles = new List<ArticleScriptableObject>(allArticles);
        leftOverAdvertisements = new List<ArticleScriptableObject>(allAdvertisements);
    }

    // Start is called before the first frame update
    void Start()
    {
        newspaper = FindObjectOfType<NewspaperManager>();
        
        //ChooseArticlesRandomlyForBillboard(0);
    }

    //randomly add articles to the billboard
    public void ChooseArticlesRandomlyForBillboard(int numberToAdd = 1)
    {
        int countAds=0;
        int countArticles=0;

        //count the current number
        foreach (var article in billBoardArticles)
        {
            if (article.articleCategory == ArticleCategory.Article)
            {
                countArticles++;
            }
            else
            {
                countAds++;
            }
        }
        //do ads first
        for (; countAds < 2; countAds++)
        {
            if (leftOverAdvertisements.Count > 0)
            {
                ArticleScriptableObject article = leftOverAdvertisements[Random.Range(0, leftOverAdvertisements.Count)];
                if (AddToBillBoard(article))
                    leftOverAdvertisements.Remove(article);
            }
            else
            {
                Debug.Log("No more ads remaining!");
            }
        }
        
        //then do articles
        
        for (int i = countArticles; i <= 6; i++)
        {
            if (leftOverArticles.Count > 0)
            {
                ArticleScriptableObject article = leftOverArticles[Random.Range(0, leftOverArticles.Count)];
                if (AddToBillBoard(article))
                    leftOverArticles.Remove(article);
            }
            else
            {
                Debug.Log("No more articles remaining!");
            }
        }
    }

    public void ChooseAdsRandomlyForBillBoard(int numberToAdd = 1)
    {
        
    }

    public void ChooseTutorialArticles()
    {
        foreach (var article in dayZeroArticles)
        {
            AddToBillBoard(article);
        }
    }

    //called when the day starts to add articles to the ones on the billboard
    //can be used for just adding back from the computer as well
    public bool AddToBillBoard(ArticleScriptableObject article)
    {
        if (billBoardArticles.Count < billBoardArticleMax)
        {
            billBoardArticles.Add(article);
            FindObjectOfType<BulletinManager>().AddArticle(article);
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
        //billBoard.RemoveArticle(article);
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

        foreach (var article in computerArticles)
        {
            billBoardArticles.Remove(article);
            billBoard.RemoveArticle(article);
        }
        computerArticles.Clear();
        
        
    }

    public void ClearUsedBillBoardArticlesOnDayZero()
    {
        if (FindObjectOfType<GameManager>().GetDay() == 0)
        {
            billBoardArticles.Clear();
            foreach (var article in dayZeroArticles)
            {
                billBoard.RemoveArticle(article);
            }
        }
    }

}

