using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TownState
{
    Idle,
    TimePass
}
public class TownStateMachine : MonoBehaviour
{
    public TownState state;


    // Start is called before the first frame update
    void Start()
    {
        state = TownState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case TownState.Idle:
                break;

            case TownState.TimePass:
                break;

            default: Debug.Log("No state selected."); break;
        }
    }
}
