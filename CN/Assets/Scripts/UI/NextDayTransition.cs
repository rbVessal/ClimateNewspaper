using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDayTransition : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallStateTransitionToTown()
    {
        GameStateManager.Main.ChangeStateToTown();
        FindObjectOfType<CameraManager>().ChangeCam(3);
    }
    public void FadeIn()
    {
        GetComponent<Animator>().SetTrigger("fadeIn");
        Invoke(nameof(FadeOut),5f);
    }

    public void FadeOut()
    {
        GetComponent<Animator>().SetTrigger("fadeOut");
    }
}
