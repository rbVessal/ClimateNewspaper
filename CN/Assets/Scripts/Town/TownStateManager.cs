using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public enum TownState
{
    Idle,
    TimePass,
    Calculate
}
public class TownStateManager : MonoBehaviour
{
    public TownState townState;

    public TownStateBase state;

    public Town_idle idle           = new Town_idle();
    public Town_timepass timepass   = new Town_timepass();
    public Town_calculate calculate = new Town_calculate();

    
    [Header("Climate Percentage")]
    [Tooltip("True = See climate changes immediately, regardless of state")] public bool useDebug = false;
    [Range(0,100)] public float climateImpact;
    [Header("Time Pass State")]
    [Tooltip("Duration of time pass in seconds.")] public float duration;
    [Header("Thresholds")]
    [HideInInspector] public float stage1_bound = 25;
    [HideInInspector] public float stage2_bound = 50;
    [HideInInspector] public float stage3_bound = 75;
    [HideInInspector] public float stage4_bound = 100;
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
        climateImpact = econManager.GetEconomy().ClimateImpact;
        AdjustFog();
    }

    // Update is called once per frame
    void Update()
    {
        if (useDebug)
        {   
            DebugClimateControl();
        }
        DebugStateChange(); //Allows us to change the state manually in editor for debug purposes, it skips exit conditions.
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
            
            econManager.SetEconomy(0, 0, climateImpact);
            DebugAdjustFog();
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

            case TownState.Calculate:
                if (state != calculate)
                {
                    ChangeState(calculate);
                }
                break;

            default: Debug.Log("No state chosen."); break;
        }
    }

    void DebugAdjustFog()
    {
        // Normalize the value to a range between 0 and 1
        float normalizedValue = Mathf.Clamp01(climateImpact / 100f);
        // Adjust the alpha based on the normalized value (0 to 1)
        float alpha = 1 - normalizedValue;
        // Get the color from the gradient based on the normalized value
        Color fogColor = fogGradient.Evaluate(normalizedValue);
        // Set the color with the adjusted alpha
        fogColor.a = alpha;
        // Apply the color to the fog particle system
        var mainModule = fogParticleSystem.main;
        mainModule.startColor = fogColor;

        //Debug.Log("Alpha value " + alpha);
    }

    public void AdjustFog()
    {
        // Normalize the value to a range between 0 and 1
        float normalizedValue = Mathf.Clamp01(climateImpact / 100f);
        // Adjust the alpha based on the normalized value (0 to 1)
        float alpha = 1 - normalizedValue;
        // Get the color from the gradient based on the normalized value
        Color fogColor = fogGradient.Evaluate(normalizedValue);

        // Store the initial alpha value
        float initialAlpha = fogParticleSystem.main.startColor.color.a;
        // Set the color with the adjusted alpha
        // Create a tween to change the alpha value over time
        DOTween.To(() => initialAlpha, x =>
        {
            initialAlpha = x;
            fogColor.a = initialAlpha;
            var mainModule = fogParticleSystem.main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(fogColor);
        }, alpha, duration)
        .OnUpdate(() =>
        {
            //Debug.Log("Alpha value " + initialAlpha);
        });
        
    }

    public void PublishButtonEvent()
    {
        if(state == idle)
        {
            ChangeState(timepass);
        }
    }
}
