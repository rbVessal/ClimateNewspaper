using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArticleCloseUpDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text headline;
    [SerializeField] TMP_Text body;
    [SerializeField] Image art;

    [SerializeField]private GameObject displayer;
    
    // Start is called before the first frame update
    void Start()
    {
        displayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayArticle(ArticleScriptableObject article)
    {
        headline.text = article.headline;
        body.text = article.bodyText;
        art.sprite = article.articleArt;
        displayer.SetActive(true);
    }
}
