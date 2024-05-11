using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EconomyManager : MonoBehaviour
{
    //Set this up in the inspector
    public UnityEvent<GameEconomy> Link;
    private GameEconomy economy;

    //base values
    public int moneyBase;
    public int reachBase;
    public float impactBase;

    // Start is called before the first frame update
    void Start()
    {
        //instantiate Economy class
        economy = new GameEconomy(moneyBase, reachBase, impactBase);
        //Testing
        economy.UpdateValues(0, 15, 20.0f);
        economy.GetValues();
        SendToUI();
    }

    public void Update()
    {
        //Testing only
        if(Input.GetKeyDown(KeyCode.Space))
        {
            economy.UpdateValues(-20, 100, 5);
            SendToUI();
        }
    }
    public void SendToUI()
    {
        Link.Invoke(economy);
    }
}
