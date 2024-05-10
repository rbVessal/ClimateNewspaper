using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EconomyUIManager : MonoBehaviour
{
    //References to the UI components
    [Header("Text for numerical values")]
    public TMP_Text moneyValue;
    public TMP_Text reachValue;
    public TMP_Text impactValue;
    [Header("Visual representations for economy")]
    public Image moneyImage;
    public Image reachImage;
    public Image impactImage;

    private List<Tween> activeTweens = new List<Tween>();
    private float maxScale;
    private float scaleSpeed;
    private float baseScale = 1.0f;
    private bool isFocused = false;

    private void Start()
    {
        //Testing focusing
        //ElementFocus(reachImage);
        //ElementFocus(moneyImage);
    }

    private void Update()
    {
        //Testing purposes
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(isFocused)
            {
                ElementUnfocus();
                isFocused = false;
            }
            else
            {
                ElementFocus(impactImage);
                ElementFocus(moneyImage);
                isFocused = true;
            }
            
        }
    }

    //Sets the text fields in the economy display UI to the current values for money, reach and climate impact. 
    public void SetValues(GameEconomy economy)
    {
        moneyValue.text  = economy.Money.ToString();
        reachValue.text  = economy.Reach.ToString(); 
        impactValue.text = economy.ClimateImpact.ToString();
    }

    private void ElementFocus(Image image)
    {
        //Place logic here for signaling to the player that certain values in the game economy wil be affected. This could be anything we want. For testing purposes, I am using a simple scaling. 

        //Takes the image and scales it to 1.1 and then returns back to it's original. 
        //Create a tween and add it to a list 
       var tween = image.transform.DOScale(1.1f,.5f).SetLoops(-1,LoopType.Yoyo);
        activeTweens.Add(tween);
    }

    //When we are not focused on an article anymore, stop all tweens. 
    private void ElementUnfocus()
    {
        //This could change based on our needs and design

        //Example implementation using a tween scale approach
        if (activeTweens.Count > 0)
        {
            // Iterate over the list in reverse order to avoid Invalid Operation Exception (just learned about this)
            for (int i = activeTweens.Count - 1; i >= 0; i--)
            {
                Tween tween = activeTweens[i];
                if (tween.IsActive())
                {
                    tween.Restart(); //Resets the scale of the images
                    tween.Kill();    //Stops the tween
                }
                // Remove the tween from the list
                activeTweens.RemoveAt(i);
            }
        }
    }


    //This would determine to focus the economy images if the selected article has non-zero changes to any of the values. 
    public void DetermineFocus(ArticleScriptableObject article)
    {
        if(article.moneyChange != 0)
        {
            ElementFocus(moneyImage);
        }

        if(article.reachChange != 0)
        {
            ElementFocus(reachImage);
        }

        if(article.climateChange != 0)
        {
            ElementFocus(impactImage);
        }
    }
}
