using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletinManager : MonoBehaviour
{
    [SerializeField] private BillBoardArticleDisplayer billBoard;

    [SerializeField] private GameObject billBoardCanvas;

    [SerializeField] private List<ArticleScriptableObject> articles;

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

    public void AddArticle(ArticleScriptableObject article)
    {
        articles.Add(article);
    }
    public void RemoveArticle(ArticleScriptableObject article)
    {
        articles.Remove(article);
    }

    public void UpdateBillBoardUI()
    {
        billBoardCanvas.SetActive(true);
        billBoard.SetArticles(articles);
    }
}
