using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EconomyManager : MonoBehaviour
{
    //Updates Economy Display UI
    public static event Action<GameEconomy> Link;
    
    private GameEconomy economy;

    [Header("Starting Values")]
    //base values
    public int moneyBase;
    public int reachBase;
    public float impactBase;
    private void Awake()
    {
        //instantiate Economy class
        economy = new GameEconomy(moneyBase, reachBase, impactBase);
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
    }
    //Sends economy values to UI
    public void SendToUI()
    {
        Link.Invoke(economy);
    }

    //Updates the economy additively 
    public void UpdateEconomy(int money = 0, int reach = 0, float climateImpact = 0)
    {
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
}
