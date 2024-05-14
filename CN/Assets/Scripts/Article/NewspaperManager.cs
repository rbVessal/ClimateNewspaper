using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


public class NewspaperManager : MonoBehaviour
{
    [SerializeField] List<ArticleDisplay> displays;

    [SerializeField] private List<ArticleScriptableObject> articles;
    [SerializeField] private ArticleScriptableObject emptyArticle;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateArticles()
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

    public void AddArticle(ArticleScriptableObject article)
    {
        articles.Add(article);
    }

    public void RemoveArticle(ArticleScriptableObject article)
    {
        articles.Remove(article);
    }
}
