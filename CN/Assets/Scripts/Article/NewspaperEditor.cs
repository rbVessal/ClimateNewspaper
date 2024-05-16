using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NewspaperEditor : MonoBehaviour
{
    [SerializeField] public GameObject FrontPage;
    [SerializeField] public GameObject BackPage;

    private void Start()
    {
        CoDropItemSlot[] dropItemSlots = GetComponentsInChildren<CoDropItemSlot>();
        foreach(CoDropItemSlot slot in dropItemSlots)
        {
            slot.onDroppedItemEvent.AddListener(OnDroppedArticle);
            slot.onRemovedItemEvent.AddListener(OnRemovedArticle);
        }
    }

    public void OnPublishButtonClicked()
    {

        NewspaperManager newsPaperManager = FindObjectOfType<NewspaperManager>();
        ArticleManager articleManager = FindObjectOfType<ArticleManager>();
        CoDropItemSlot[] dropItemSlots = GetComponentsInChildren<CoDropItemSlot>();
        List<ArticleScriptableObject> articlesOnComputer =new List<ArticleScriptableObject>( newsPaperManager.GetArticles());
        List<ArticleScriptableObject> slottedArticles=new List<ArticleScriptableObject>();
        
        // Do proper cleanup of resetting the article display and removing events
        foreach (CoDropItemSlot dropItemSlot in dropItemSlots)
        {
            GameObject articleObject = dropItemSlot.GetSlottedArticle();
            if (articleObject != null)
            {
                slottedArticles.Add(articleObject.GetComponent<ArticleDisplay>().article);
            }
            dropItemSlot.ClearItemInSlot();
        }
        Debug.Log("---------------SLOTTED--------------");
        foreach (var article in slottedArticles)
        {
            Debug.Log("ARTICLE IN SLOT: "+article.name);
        }
        Debug.Log("---------------SLOTTED--------------");
        foreach (var article in articlesOnComputer)
        {
            Debug.Log(article.name);
            if (!slottedArticles.Contains(article))
            {
                Debug.Log("Article removed");
                articleManager.RemoveFromComputer(article);

            }
        }
        
        // Clear all of the newspaper article display properly
        if (newsPaperManager != null)
        {
            newsPaperManager.ClearAllArticles();
        }

        // Clear all of the computer articles stored in the article manager
        if (articleManager != null) 
        {
            articleManager.ClearAllComputerArticles();
        }
        
        // Now we can finally start the new day
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.StartNewDay();
        }
    }

    public void UpdateEconomyUI(GameObject gameObject, bool wasArticleAdded)
    {
        // Make the economy UI update based on whatever article just dropped
        if (gameObject != null)
        {
            ArticleDisplay articleDisplay = gameObject.GetComponent<ArticleDisplay>();
            if (articleDisplay != null)
            {
                ArticleScriptableObject articleSO = articleDisplay.article;
                if (articleSO != null)
                {
                    EconomyManager economyManager = FindAnyObjectByType<EconomyManager>();
                    if (economyManager != null)
                    {
                        Debug.Log("Editor - economy manager");
                        if (wasArticleAdded)
                        {
                            economyManager.UpdateEconomy(articleSO.moneyChange, articleSO.reachChange, articleSO.climateChange);
                        }
                        else
                        {
                            economyManager.UpdateEconomy(-articleSO.moneyChange, -articleSO.reachChange, -articleSO.climateChange);
                        }
                    }
                }
            }
        }
    }

    // Delegate method for on dropped article event from CoDropItemSlot
    public void OnDroppedArticle(GameObject gameObject)
    {
        Debug.Log("NewspaperEditor - dropped article");
        // Make the economy UI update based on whatever article just dropped
        UpdateEconomyUI(gameObject, true);
    }

    // Delegate method for when an article is unslotted from CoDropItemSlot
    public void OnRemovedArticle(GameObject gameObject)
    {
        Debug.Log("NewspaperEditor - removed article");

        UpdateEconomyUI(gameObject, false);
    }

    public int GetMaxSlots()
    { 
        int maxSlots = 0;

        CoDropItemSlot[] dropItemSlots = GetComponentsInChildren<CoDropItemSlot>();
        maxSlots = dropItemSlots.Length;

       // Debug.LogFormat("Max slots: {0}", maxSlots);

        return maxSlots;
    }

}
