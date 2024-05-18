using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NewspaperEditor : MonoBehaviour
{
    public static event Action PublishClicked;
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
        //GameManager gameManager = FindObjectOfType<GameManager>();
        //if (gameManager != null)
        //{
        //    gameManager.StartNewDay();
        //}
        //Change state to town
        GameStateManager.Main.ChangeStateToTown();
        PublishClicked?.Invoke();
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

                        ArticleValues articleValues = economyManager.CalculateArticleValues(articleSO, articleSlotGameObject.transform.parent.gameObject, articleSlotGameObject);

                        if (wasArticleAdded)//dragged in
                        {
                            //economyManager.UpdateEconomy(articleValues.money, articleValues.reach, articleValues.climate);
                            economyManager.AddToTemporaryEconomy(articleValues);
                        }
                        else//dragged out
                        {
                            //economyManager.UpdateEconomy(-articleValues.money, -articleValues.reach, -articleValues.climate);
                            economyManager.SubtractFromTemporaryEconomy(articleValues);
                        }
                    }
                }
            }
        }
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
