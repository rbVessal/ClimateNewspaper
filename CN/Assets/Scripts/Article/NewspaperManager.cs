using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


public class NewspaperManager : MonoBehaviour
{
    [SerializeField] List<ArticleDisplay> displays;

    [SerializeField] private List<ArticleScriptableObject> articles;
    [SerializeField] private ArticleScriptableObject emptyArticle;

    [SerializeField] private NewspaperEditor editor;

    private void Start()
    {
        foreach(ArticleDisplay display in displays) 
        {
            display.gameObject.SetActive(false);
        }
    }

    public int GetMaxSlots()
    {
        int maxSlots = 0;

        if(editor != null) 
        { 
            maxSlots = editor.GetMaxSlots();
        }

        return maxSlots;
    }

    public void UpdateArticles()
    {
        if (articles.Count > 0)
        {
            int i = 0;
            foreach (var article in articles)
            {
                displays[i].article = article;
                displays[i].gameObject.SetActive(true);

                i++;
            }

            for (; i < displays.Count; i++)
            {
                displays[i].gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (ArticleDisplay display in displays)
            { 
                display.gameObject.SetActive(false);
            }
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

    public List<ArticleScriptableObject> GetArticles() => articles;
   

    public void ClearAllArticles()
    {
        // TODO: Send unused articles back to the bulletin board

        foreach (ArticleDisplay display in displays)
        {
            display.article = null;
        }

        articles.Clear();
    }
}
