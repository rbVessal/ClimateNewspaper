using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperEditor : MonoBehaviour
{
    [SerializeField] public GameObject FrontPage;
    [SerializeField] public GameObject BackPage;

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

    // Delegate method for on dropped article event from CoDragDrop
    public void OnDroppedArticle(GameObject gameObject)
    {
        if (gameObject != null)
        {
            ArticleScriptableObject articleScriptObject = gameObject.GetComponent<ArticleScriptableObject>();
            if (articleScriptObject != null)
            {
                // TODO:  Make the economy UI update based on whatever article just dropped
            }
        }
    }

    public int GetMaxSlots()
    { 
        int maxSlots = 0;

        CoDropItemSlot[] dropItemSlots = GetComponentsInChildren<CoDropItemSlot>();
        maxSlots = dropItemSlots.Length;

        Debug.LogFormat("Max slots: {0}", maxSlots);

        return maxSlots;
    }

}
