using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;
using System;
using static UnityEditor.Rendering.FilterWindow;

public class EconomyUIManager : MonoBehaviour
{
    
    public GameObject moneyParent;
    public GameObject reachParent;
    public GameObject impactParent;

    TMP_Text moneyText;
    TMP_Text reachText;
    TMP_Text impactText;
    private List<GameObject> values = new List<GameObject>();

    //Subscribe to article pop up display event
    private void OnEnable()
    {
        ArticleCloseUpDisplay.SignalUIFocus += DetermineFocus;
        EconomyManager.Link += SetValues;
    } 
    private void OnDisable()
    {
        ArticleCloseUpDisplay.SignalUIFocus -= DetermineFocus;
        EconomyManager.Link -= SetValues;
    }
    private void Awake()
    {
        GetTextRefs();
    }
    private void Start()
    {
        //Add UI element parent objects to a list for easy un-focusing
        if(values.Count== 0)
        {
            values.Add(moneyParent);
            values.Add(reachParent);
            values.Add(impactParent);
        }
    }

    private void Update()
    {
        //Testing
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("ToggleFocus");
            FocusElement(moneyParent);
            FocusElement(impactParent);
        }
    }



    //Sets the text fields in the economy display UI to the current values for money, reach and climate impact. 
    public void SetValues(GameEconomy economy)
    {
        moneyText.text  = economy.Money.ToString();
        reachText.text  = economy.Reach.ToString();
        impactText.text = economy.ClimateImpact.ToString();
    }

    public void FocusElement(GameObject parent)
    {
        UIElementManager element = parent.GetComponentInChildren<UIElementManager>();
        if(element.isFocused)
        {
            element.indicator.DOFade(0, .1f);
            element.isFocused = false;
        }
        else
        {
            element.indicator.DOFade(1,.25f);
            element.isFocused = true;
        }
    }

    public void UnfocusElement()
    {
        foreach(GameObject obj in values)
        {
            UIElementManager value = obj.GetComponentInChildren<UIElementManager>();
            if (value.isFocused)
            {
                value.indicator.DOFade(0, .1f);
                value.isFocused = false;
            }
        }
    }

    //This would determine to focus the economy images if the selected article has non-zero changes to any of the values. 
    public void DetermineFocus(ArticleScriptableObject article)
    {
        if (article.moneyChange != 0)
        {
            FocusElement(moneyParent);
        }

        if (article.reachChange != 0)
        {
            FocusElement(reachParent);
        }

        if (article.climateChange != 0)
        {
            FocusElement(impactParent);
        }
    }

    private void GetTextRefs()
    {
        moneyText  = moneyParent.transform.Find("Value_text").GetComponent<TMP_Text>();
        reachText  = reachParent.transform.Find("Value_text").GetComponent<TMP_Text>();
        impactText = impactParent.transform.Find("Value_text").GetComponent<TMP_Text>();
    }
}
