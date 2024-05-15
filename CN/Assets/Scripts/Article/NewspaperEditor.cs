using System;
using System.Collections;
using System.Collections.Generic;
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
            //slot.onRemovedItemEvent.AddListener(OnRemovedArticle);
        }
    }

    public void OnPublishButtonClicked()
    {
        // Clear all of the newspaper article display properly
        NewspaperManager newsPaperManager = FindObjectOfType<NewspaperManager>();
        if (newsPaperManager != null)
        {
            newsPaperManager.ClearAllArticles();
        }

        // Clear all of the computer articles stored in the article manager
        ArticleManager articleManager = FindObjectOfType<ArticleManager>();
        if (articleManager != null) 
        {
            articleManager.ClearAllComputerArticles();
        }

        // Do proper cleanup of resetting the article display and removing events
        CoDropItemSlot[] dropItemSlots = GetComponentsInChildren<CoDropItemSlot>();
        foreach (CoDropItemSlot dropItemSlot in dropItemSlots)
        {
            dropItemSlot.ClearItemInSlot();
        }

        // Now we can finally start the new day
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.StartNewDay();
        }
    }

    public void UpdateEconomyUI(bool wasArticleAdded)
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
        UpdateEconomyUI(true);
    }

    // Delegate method for when an article is unslotted from CoDropItemSlot
    public void OnRemovedArticle(GameObject gameObject)
    {
        Debug.Log("NewspaperEditor - removed article");

        UpdateEconomyUI(false);
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
