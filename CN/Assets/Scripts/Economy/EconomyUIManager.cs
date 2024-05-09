using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EconomyUIManager : MonoBehaviour
{
    //References to the text components
    public TMP_Text moneyValue;
    public TMP_Text reachValue;
    public TMP_Text impactValue;

    public void SetValues(GameEconomy economy)
    {
        moneyValue.text  = economy.Money.ToString();
        reachValue.text  = economy.Reach.ToString(); 
        impactValue.text = economy.ClimateImpact.ToString();
    }
}
