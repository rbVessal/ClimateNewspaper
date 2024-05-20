using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class NewspaperEditor : MonoBehaviour
{
    public static event Action PublishClicked;
    [SerializeField] public GameObject FrontPage;
    [SerializeField] public GameObject BackPage;
    [SerializeField] private GameObject PublishButton;

    private void Start()
    {
        CoDropItemSlot[] dropItemSlots = GetComponentsInChildren<CoDropItemSlot>();
        foreach(CoDropItemSlot slot in dropItemSlots)
        {
            slot.onDroppedItemEvent.AddListener(OnDroppedArticle);
            slot.onRemovedItemEvent.AddListener(OnRemovedArticle);
        }

        SetPublishButtonInteractability(false);
    }

    private void UpdateHeader()
    {
        Page page = FrontPage.GetComponent<Page>();
        if (page != null) 
        {
            page.UpdateWeek();
        }
    }

    public void OnPublishButtonClicked()
    {
        if (AreAllArticleSlotsFilled())
        {
            NewspaperManager newsPaperManager = FindObjectOfType<NewspaperManager>();
            ArticleManager articleManager = FindObjectOfType<ArticleManager>();
            CoDropItemSlot[] dropItemSlots = GetComponentsInChildren<CoDropItemSlot>();
            List<ArticleScriptableObject> articlesOnComputer = new List<ArticleScriptableObject>(newsPaperManager.GetArticles());
            List<ArticleScriptableObject> slottedArticles = new List<ArticleScriptableObject>();

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
                Debug.Log("ARTICLE IN SLOT: " + article.name);
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

            // Update the day text on the newspaper
            UpdateHeader();

            // Now we can finally start the new day
            //GameManager gameManager = FindObjectOfType<GameManager>();
            //if (gameManager != null)
            //{
            //    gameManager.StartNewDay();
            //}
            //Change state to town
            GameStateManager.Main.ChangeStateToTown();

            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.EnableStartDayButton(true);
            articleManager.ClearUsedBillBoardArticlesOnDayZero();
            PublishClicked?.Invoke();
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

        // Check to see if we should enable the publish button based on if all of the article slots are full or not
        if (AreAllArticleSlotsFilled())
        { 
           SetPublishButtonInteractability(true);
        }
    }

    // Delegate method for when an article is unslotted from CoDropItemSlot
    public void OnRemovedArticle(GameObject articleGameObject, GameObject slotGameObject)
    {
        Debug.Log("NewspaperEditor - removed article");

        UpdateEconomyUI(articleGameObject, slotGameObject, false);

        if (!AreAllArticleSlotsFilled())
        {
            SetPublishButtonInteractability(false);
        }
    }

    private void SetPublishButtonInteractability(bool interactable)
    {
        Button button = PublishButton.GetComponent<Button>();
        if (button != null)
        {
            button.interactable = interactable;
        }
    }

    private bool AreAllArticleSlotsFilled()
    {
        bool areAllArticleSlotsFilled = false;

        int numberOfSlotsFilled = 0;
        CoDropItemSlot[] dropItemSlots = GetComponentsInChildren<CoDropItemSlot>();
        foreach (CoDropItemSlot slot in dropItemSlots)
        {
            if (slot.isOccupied)
            {
                numberOfSlotsFilled++;
            }
        }

        if(numberOfSlotsFilled == dropItemSlots.Length) 
        {
            areAllArticleSlotsFilled = true;
        }

        return areAllArticleSlotsFilled;
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
