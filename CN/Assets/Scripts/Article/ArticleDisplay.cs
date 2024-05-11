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
        }
        

    }


    public void ChangeArticle(ArticleScriptableObject newArticle)
    {
        article = newArticle;
        UpdateUI();
    }

    public void CloseUpView()
    {
        closeUpDisplay.DisplayArticle(article);
    }
}
