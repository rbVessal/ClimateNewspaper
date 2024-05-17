using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEconomy
{
    public int Money { get; set; }
    public int Reach { get; set; }
    public float ClimateImpact { get; set; }

    //Constructor 
    public GameEconomy (int money, int mediaReach, float climateImpact)
    {
        Money         = money;
        Reach         = mediaReach;
        ClimateImpact = climateImpact;

        // Ensure ClimateImpact stays within the range of 0 to 100
        if (ClimateImpact > 100)
        {
            ClimateImpact = 100;
        }
        else if (ClimateImpact < 0)
        {
            ClimateImpact = 0;
        }
    }

    //Debug method
    public void GetValues()
    {
        //Return the current instance of the economy. 
        //Anything calling this can use dot accessors to get individual values. GetValues.Money, etc.
        Debug.Log("Current Money is " + this.Money);
        Debug.Log("Current Reach is " + this.Reach);
        Debug.Log("Current Climate impact is " + this.ClimateImpact);
    }

    //Updates values
    public void UpdateValues(int moneyChanges = 0, int reachChanges = 0, float climateChanges = 0)
    {
        Money         += moneyChanges;
        Reach         += reachChanges;
        ClimateImpact += climateChanges;

        // Ensure ClimateImpact stays within the range of 0 to 100
        if (ClimateImpact > 100)
        {
            ClimateImpact = 100;
        }
        else if (ClimateImpact < 0)
        {
            ClimateImpact = 0;
        }
    }
}
