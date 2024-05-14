using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_pause : TownStateBase
{
    public override void EnterState(TownStateManager town)
    {
        Debug.Log("Entered " + town.state + " state.");
    }

    public override void UpdateState(TownStateManager town)
    {
        
    }
}
