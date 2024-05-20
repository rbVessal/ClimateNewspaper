using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArticleDisplay : MonoBehaviour
{
    public ArticleScriptableObject article;
    private Texture2D art;
    private string headline;
    private string body;

    public Image artUI;
    public TMP_Text headlineUI;
    public TMP_Text bodyUI;
    
    private ArticleCloseUpDisplay closeUpDisplay;

    [SerializeField] private ArticleScriptableObject emptyArticle;

    [SerializeField] private Image panel;
    [SerializeField] private Color32 articleColor;
    [SerializeField] private Color32 adColor;
    
    public bool added = false;
    [SerializeField] private GameObject addButton;
    [SerializeField] private GameObject removeButton;
    
    
    // Start is called before the first frame update
    void Start()
    {
        closeUpDisplay = FindObjectOfType<ArticleCloseUpDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (article)
        {
            artUI.sprite = article.articleArt;
            bodyUI.text = article.bodyText;
            headlineUI.text = article.headline;
            if (article.articleCategory == ArticleCategory.Article)
            {
                panel.color = articleColor;
            }
            else if(article.articleCategory==ArticleCategory.Advertisement)
            {
                panel.color = adColor;
            }
        }
        

    }


    public void ChangeArticle(ArticleScriptableObject newArticle)
    {
        article = newArticle;
        UpdateUI();
    }

    
    public void MoveToComputer()
    {
        Debug.Log("move to computer called");
        if(article!=emptyArticle)
            if (FindObjectOfType<ArticleManager>().AddToComputer(article))
            {
                removeButton.SetActive(true);
                addButton.SetActive(false);
                added = true;
            }
            
    }

    public void ResetButtons()
    {
        removeButton.SetActive(false);
        addButton.SetActive(true);
    }
    public void MoveToBillBoard()
    {
        if (FindObjectOfType<ArticleManager>().RemoveFromComputer(article))
        {
            added = false;
            removeButton.SetActive(false);
            addButton.SetActive(true);
        }
    }
    
    public void CloseUpView()
    {
        closeUpDisplay.DisplayArticle(article);
    }
}
