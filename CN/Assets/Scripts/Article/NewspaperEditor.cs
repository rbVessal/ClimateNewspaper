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
            slot.onRemovedItemEvent.AddListener(OnRemovedArticle);
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

    public void UpdateEconomyUI(GameObject articleGameObject, GameObject articleSlotGameObject, bool wasArticleAdded)
    {
        // Make the economy UI update based on whatever article just dropped
        if (articleGameObject != null)
        {
            ArticleDisplay articleDisplay = articleGameObject.GetComponent<ArticleDisplay>();
            if (articleDisplay != null)
            {
                ArticleScriptableObject articleSO = articleDisplay.article;
                if (articleSO != null)
                {
                    EconomyManager economyManager = FindAnyObjectByType<EconomyManager>();
                    if (economyManager != null)
                    {
                        //Debug.Log("Editor - economy manager");

                        ArticleValues articleValues = CalculateArticleValues(articleSO, articleSlotGameObject.transform.parent.gameObject, articleSlotGameObject);

                        if (wasArticleAdded)
                        {
                            economyManager.UpdateEconomy(articleValues.money, articleValues.reach, articleValues.climate);
                        }
                        else
                        {
                            economyManager.UpdateEconomy(-articleValues.money, -articleValues.reach, -articleValues.climate);
                        }
                    }
                }
            }
        }
    }

    private ArticleValues CalculateArticleValues(ArticleScriptableObject articleSO, GameObject pageGameObject, GameObject articleSlotGameObject)
    {
        ArticleValues articleValues = new ArticleValues();

        float pageMultiplier = 1.0f;
        CoBonusStats pageMultiplierBonusStats = pageGameObject.GetComponent<CoBonusStats>();
        if(pageMultiplierBonusStats != null) 
        {
            pageMultiplier = pageMultiplierBonusStats.multiplier;
        }

        float articleSlotMultiplier = 1.0f;
        CoBonusStats articleBonusStats = articleSlotGameObject.GetComponent<CoBonusStats>();
        if(articleBonusStats != null) 
        {
            articleSlotMultiplier = articleBonusStats.multiplier;
        }

        articleValues.money = (int)(articleSO.moneyChange * pageMultiplier * articleSlotMultiplier);
        articleValues.reach = (int)(articleSO.reachChange * pageMultiplier * articleSlotMultiplier);
        articleValues.climate = articleSO.climateChange * pageMultiplier * articleSlotMultiplier;

        return articleValues;
    }

    // Delegate method for on dropped article event from CoDropItemSlot
    public void OnDroppedArticle(GameObject articleGameObject, GameObject slotGameObject)
    {
        Debug.Log("NewspaperEditor - dropped article");
        // Make the economy UI update based on whatever article just dropped
        UpdateEconomyUI(articleGameObject, slotGameObject, true);
    }

    // Delegate method for when an article is unslotted from CoDropItemSlot
    public void OnRemovedArticle(GameObject articleGameObject, GameObject slotGameObject)
    {
        Debug.Log("NewspaperEditor - removed article");

        UpdateEconomyUI(articleGameObject, slotGameObject, false);
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
