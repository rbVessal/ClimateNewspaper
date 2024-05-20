using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Town_calculate : TownStateBase
{
    Texture2D veryBad;
    Texture2D bad;
    Texture2D normal;
    Texture2D good;
    public override void EnterState(TownStateManager town)
    {
        veryBad = town.VeryBadTown;
        bad     = town.badTown;
        normal  = town.normalTown;
        good    = town.goodTown;

        Debug.Log("Entered " + town.state + " state.");
        //Get the current climate impact
        GetClimate(town);
        Debug.Log("Climate metric: " + town.econManager.GetEconomy().ClimateImpact);
        //Determine changes on the town.
        DetermineChanges(town);
        //Check state to go to
        if (town.doTimePass)
        {
            //Go to time pass state
            town.ChangeState(town.timepass);
            Debug.Log("Going to time pass from calculate");
        }
        else
        {
            //Go to idle state
            town.ChangeState(town.idle);
        }
        
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

            //Set Town Image
            DetermineTownImage(town, veryBad);

            //NO MUSIC FOR THIS LEVEL
            SoundManager.main.DetermineMusic(null);
            //Setting town ambience
            SoundManager.main.DetermineTownAmbience(SoundManager.main.Town[0]);

        }
        else if (econ.ClimateImpact >= town.stage1_bound && econ.ClimateImpact < town.stage2_bound)
        {
            // Code for ClimateImpact greater than or equal to 25 and less than 50

            //Set Town Image
            DetermineTownImage(town, bad);

            //Setting music style
            SoundManager.main.DetermineMusic(SoundManager.main.MainGameMusic[0]);
            //Setting town ambience
            SoundManager.main.DetermineTownAmbience(SoundManager.main.Town[1]);
        }
        else if (econ.ClimateImpact >= town.stage2_bound && econ.ClimateImpact < town.stage3_bound)
        {
            // Code for ClimateImpact greater than or equal to 50 and less than 75

            //Set Town Image
            DetermineTownImage(town, normal);

            //Setting music style
            SoundManager.main.DetermineMusic(SoundManager.main.MainGameMusic[1]);
            //Setting town ambience
            SoundManager.main.DetermineTownAmbience(SoundManager.main.Town[2]);
        }
        else if (econ.ClimateImpact >= town.stage3_bound && econ.ClimateImpact <= town.stage4_bound)
        {
            // Code for ClimateImpact greater than or equal to 75 and less than or equal to 100

            //Set Town Image
            DetermineTownImage(town, good);

            //Setting music style
            SoundManager.main.DetermineMusic(SoundManager.main.MainGameMusic[2]);
            //Setting town ambience
            SoundManager.main.DetermineTownAmbience(SoundManager.main.Town[3]);
        }
        else
        {
            Debug.LogWarning("Out or range.");
        }
    }

    void GetClimate(TownStateManager town)
    {
        town.climateImpact = town.econManager.GetEconomy().ClimateImpact;
    }

    void DetermineTownImage(TownStateManager town, Texture2D tex)
    {
        if(town.currentTownImage.texture != null)
        {
            town.nextImage = tex;
            town.tempImageHolder.texture = town.nextImage;
        }
        else
        {
            SetTownImage(town,tex);
            Debug.Log("TownImageSet in calculate.");
        }

    }

    void SetTownImage(TownStateManager town, Texture2D tex)
    {
        //Set Town Image
        town.currentTownImage.texture = tex;
    }
}
