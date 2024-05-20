using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EconomyManager : MonoBehaviour
{
    //Updates Economy Display UI
    public static event Action<GameEconomy> Link;
    public static event Action EconomyProcessed;
    
    private GameEconomy economy;
    private GameEconomy temporaryEconomy;//temporary change variable

    [Header("Starting Values")]
    //base values
    public int moneyBase;
    public int reachBase;
    public float impactBase;

    [Header("Debug: Set Values Directly")]
    public bool useDebug = false;
    public int debug_Money;
    public int debug_Reach;
    public float debug_climateImpact;
    
    [Header("Change values on article drag and drop?")]
    [SerializeField] bool changeValuesOnDragDrop;
    private void Awake()
    {
        //instantiate Economy class
        economy = new GameEconomy(moneyBase, reachBase, impactBase);
        temporaryEconomy = new GameEconomy(moneyBase, reachBase, impactBase);

    }

    private void OnEnable()
    {
        NewspaperEditor.PublishClicked += changeEconomyPermanently;
    }

    private void OnDisable()
    {
        NewspaperEditor.PublishClicked -= changeEconomyPermanently;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //Send those values to EconomyUI for display
        SendToUI();
    }

    public void Update()
    {
        //Testing only
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UpdateEconomy(-20, 100, 5);
            Debug.Log("Debug input 'space' detected.");
        }

        if(useDebug)
        {
            SetEconomy(0,0, debug_climateImpact);
        }
    }
    //Sends economy values to UI
    public void SendToUI()
    {
        Link?.Invoke(temporaryEconomy);
    }

    //Updates the economy additively 
    public void UpdateEconomy(int money = 0, int reach = 0, float climateImpact = 0)
    {
        Debug.LogFormat("Economy Manager: money: {0}, reach: {1}, climateImpact:{2}", money, reach, climateImpact);
        economy.UpdateValues(money, reach, climateImpact);
        SendToUI();
    }

    //Set economy directly. Usually for debug purposes. 
    public void SetEconomy(int money, int reach, float climateImpact)
    {
        economy.Money = money != 0 ? money : economy.Money;
        economy.Reach = reach != 0 ? reach : economy.Reach;
        economy.ClimateImpact = climateImpact != 0 ? climateImpact : economy.ClimateImpact;
        SendToUI();
    }

    //Returns current instance of game economy
    public GameEconomy GetEconomy()
    {
        return economy;
    }
    
    public ArticleValues CalculateArticleValues(ArticleScriptableObject articleSO, GameObject pageGameObject, GameObject articleSlotGameObject)
    {
        ArticleValues articleValues = new ArticleValues();

        float pageMultiplier = 1.0f;
        CoBonusStats pageMultiplierBonusStats = pageGameObject.GetComponent<CoBonusStats>();
        if(pageMultiplierBonusStats != null) 
        {
            pageMultiplier = pageMultiplierBonusStats.multiplier;
        }

        float articleSlotMultiplier = 1.0f;
        CoBonusStats articleBonusStats = articleSlotGameObject.GetComponent<CoBonusStats>();
        if(articleBonusStats != null) 
        {
            articleSlotMultiplier = articleBonusStats.multiplier;
        }


        int finalReachChange = articleSO.reachChange + economy.Reach;
        int finalMoneyChange = articleSO.moneyChange>0?articleSO.moneyChange * (1 + economy.Reach / 100):articleSO.moneyChange;
        float finalClimateChange = articleSO.climateChange>0?articleSO.climateChange * (1 + economy.Reach / 100):articleSO.climateChange;

        articleValues.money = (int)(finalMoneyChange * pageMultiplier * articleSlotMultiplier);
        articleValues.reach = (int)(finalReachChange * pageMultiplier * articleSlotMultiplier);
        articleValues.climate = finalClimateChange * pageMultiplier * articleSlotMultiplier;

        return articleValues;
    }

    public void AddToTemporaryEconomy(ArticleValues articleValues)
    {
        temporaryEconomy.Money += articleValues.money;
        temporaryEconomy.Reach += articleValues.reach;
        temporaryEconomy.ClimateImpact += articleValues.climate;
        if(changeValuesOnDragDrop)
            SendToUI();
    }

    public void SubtractFromTemporaryEconomy(ArticleValues articleValues)
    {
        temporaryEconomy.Money -= articleValues.money;
        temporaryEconomy.Reach -= articleValues.reach;
        temporaryEconomy.ClimateImpact -= articleValues.climate;
        if(changeValuesOnDragDrop)  
            SendToUI();
    }

    public void changeEconomyPermanently()
    {
        if (FindObjectOfType<GameManager>().GetDay() > 0)//does not apply on tutorial days
        {
            
            economy.Money = temporaryEconomy.Money;
            economy.ClimateImpact = temporaryEconomy.ClimateImpact;
            economy.Reach = temporaryEconomy.Reach;
        }

        temporaryEconomy.Money = economy.Money;
        temporaryEconomy.Reach = economy.Reach;
        temporaryEconomy.ClimateImpact = economy.ClimateImpact;
        SendToUI();
        EconomyProcessed?.Invoke();
        Debug.Log("Economy Changed " + temporaryEconomy.Money + temporaryEconomy.Reach + temporaryEconomy.ClimateImpact);
    }

}
