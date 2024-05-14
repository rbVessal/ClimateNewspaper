using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public enum TownState
{
    Idle,
    TimePass,
    Paused
}
public class TownStateManager : MonoBehaviour
{
    public TownState townState;

    public TownStateBase state;

    public Town_idle idle         = new Town_idle();
    public Town_timepass timepass = new Town_timepass();
    public Town_calculate calculate       = new Town_calculate();


    [Header("Climate Percentage")]
    [Range(0,100)] public float climateImpact;
    [Header("Thresholds")]
    public float stage1_bound = 25;
    public float stage2_bound = 50;
    public float stage3_bound = 75;
    public float stage4_bound = 100;
    [Header("Visual Effects")]
    public ParticleSystem fogParticleSystem;
    public Gradient fogGradient;
    [Header("Audio Effects")]
    public AudioClip AmbientSFX;
    public EconomyManager econManager;
    


    // Start is called before the first frame update
    void Start()
    {
        state = idle;
        state.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        DebugStateChange(); //Allows us to change the state manually in editor for debug purposes, it skips exit conditions. 
        DebugClimateControl();
        state.UpdateState(this);
    }
    void ChangeState(TownStateBase newState)
    {
        state = newState;
        newState.EnterState(this);
    }
    void DebugClimateControl()
    {
        if(econManager != null)
        {
            climateImpact = econManager.GetEconomy().ClimateImpact;
            econManager.SetEconomy(0, 0, climateImpact);
            AdjustFog();
        }
        else
        {
            Debug.LogWarning("No Economy Manager referenced on Town Manager");
        }
        
    }
    void DebugStateChange()
    {
        switch (townState)
        {
            case TownState.Idle:
                if (state != idle)
                {
                    ChangeState(idle);
                }
                break;

            case TownState.TimePass:
                if (state != timepass)
                {
                    ChangeState(timepass);
                }
                break;

            case TownState.Paused:
                if (state != calculate)
                {
                    ChangeState(calculate);
                }
                break;

            default: Debug.Log("No state chosen."); break;
        }
    }

    void AdjustFog()
    {
        // Normalize the value to a range between 0 and 1
        float normalizedValue = Mathf.Clamp01(climateImpact / 100f);
        // Adjust the alpha based on the normalized value (0 to 1)
        float alpha = normalizedValue;
        // Get the color from the gradient based on the normalized value
        Color fogColor = fogGradient.Evaluate(normalizedValue);
        // Set the color with the adjusted alpha
        fogColor.a = alpha;
        // Apply the color to the fog particle system
        var mainModule = fogParticleSystem.main;
        mainModule.startColor = fogColor;
    }
}
