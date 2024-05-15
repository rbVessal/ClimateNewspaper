using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_calculate : TownStateBase
{
    public override void EnterState(TownStateManager town)
    {
        Debug.Log("Entered " + town.state + " state.");
        DetermineChanges(town);
    }

    public override void UpdateState(TownStateManager town)
    {
        
    }

    void DetermineChanges(TownStateManager town)
    {
        //Get local reference to game economy
        GameEconomy econ = town.econManager.GetEconomy();

        //Check stage bounds
        if (econ.ClimateImpact < town.stage1_bound)
        {
            // Code for ClimateImpact less than 25
        }
        else if (econ.ClimateImpact >= town.stage1_bound && econ.ClimateImpact < town.stage2_bound)
        {
            // Code for ClimateImpact greater than or equal to 25 and less than 50
        }
        else if (econ.ClimateImpact >= town.stage2_bound && econ.ClimateImpact < town.stage3_bound)
        {
            // Code for ClimateImpact greater than or equal to 50 and less than 75
        }
        else if (econ.ClimateImpact >= town.stage3_bound && econ.ClimateImpact <= town.stage4_bound)
        {
            // Code for ClimateImpact greater than or equal to 75 and less than or equal to 100
        }
        else
        {
            Debug.LogWarning("Out or range.");
        }
    }
}
