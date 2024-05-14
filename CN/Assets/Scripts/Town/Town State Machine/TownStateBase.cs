using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TownStateBase
{
    public abstract void EnterState(TownStateManager town);
    public abstract void UpdateState(TownStateManager town);
}
