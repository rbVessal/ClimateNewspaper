using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_timepass : TownStateBase
{
    public override void EnterState(TownStateManager town)
    {
        Debug.Log("Entered " + town.state + " state.");
        Debug.Log("Beginning passage of time.");
        //town.econManager.SetEconomy(0,0,town.climateImpact);
        town.AdjustFog();
    }

    public override void UpdateState(TownStateManager town)
    {
        //Check for exit condition back to idle.
    }


}
