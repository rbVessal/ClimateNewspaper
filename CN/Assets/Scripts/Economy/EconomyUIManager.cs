using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EconomyUIManager : MonoBehaviour
{
    public GameObject moneyParent;
    public GameObject reachParent;
    public GameObject impactParent;

    TMP_Text moneyText;
    TMP_Text reachText;
    TMP_Text impactText;

    private Image moneyIndicator;
    private Image reachIndicator;
    private Image impactIndicator;

    private List<Tween> activeTweens = new List<Tween>();
    private bool isFocused = false;

    private void Start()
    {
        GetRefs();
    }

    private void Update()
    {
       
    }



    //Sets the text fields in the economy display UI to the current values for money, reach and climate impact. 
    public void SetValues(GameEconomy economy)
    {
        moneyText.text  = economy.Money.ToString();
        reachText.text  = economy.Reach.ToString();
        impactText.text = economy.ClimateImpact.ToString();
    }

    private void ElementFocus(Image image)
    {

        //Use tween function 
        //Tween tween  = image.DOFade(1,.25f);
        //activeTweens.Add(tween);    
    }

    //When we are not focused on an article anymore, stop all tweens. 
    


    //This would determine to focus the economy images if the selected article has non-zero changes to any of the values. 
    public void DetermineFocus(ArticleScriptableObject article)
    {
        //if(article.moneyChange != 0)
        //{
        //    ElementFocus(moneyIndicator);
        //}

        //if(article.reachChange != 0)
        //{
        //    ElementFocus(reachIndicator);
        //}

        //if(article.climateChange != 0)
        //{
        //    ElementFocus(impactIndicator);
        //}
    }

    private void GetRefs()
    {
        moneyText  = moneyParent.transform.Find("Value_text").GetComponent<TMP_Text>();
        reachText  = reachParent.transform.Find("Value_text").GetComponent<TMP_Text>();
        impactText = impactParent.transform.Find("Value_text").GetComponent<TMP_Text>();

        moneyIndicator  = moneyParent.transform.Find("Indicator").GetComponent<Image>();
        reachIndicator  = reachParent.transform.Find("Indicator").GetComponent<Image>();
        impactIndicator = impactParent.transform.Find("Indicator").GetComponent<Image>();
    }
}
