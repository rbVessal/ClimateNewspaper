using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_timepass : TownStateBase
{
    public override void EnterState(TownStateManager town)
    {
        town.doTimePass = false;
        Debug.Log("Entered " + town.state + " state.");
        Debug.Log("Beginning passage of time.");
        town.AdjustFog();
        //SoundManager.main.PlayMusic();
        FadeInTown(town);
    }

    public override void UpdateState(TownStateManager town)
    {
        //Check for exit condition back to idle.
    }

    void FadeInTown(TownStateManager town)
    {
        // Fade this image out
        town.currentTownImage.DOFade(0, town.duration).OnComplete(() => {
            // This will execute on completion of the DOFade tween
            town.currentTownImage.texture = town.nextImage;
            Color color = town.currentTownImage.color;
            color.a = 1;
            town.currentTownImage.color = color;
            Debug.Log("Passage of Time Complete");
        });
    }

}
