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
}
