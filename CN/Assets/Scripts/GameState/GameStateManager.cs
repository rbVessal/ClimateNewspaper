using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Billboard,
    Editing,
    Town
}

public class GameStateManager : MonoBehaviour
{
  
    [SerializeField] private GameState currentState;


    public static GameStateManager Main;
    
    private void Awake()
    {
        Main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeStateToEditing()
    {
        if(currentState!=GameState.Editing)
        {
            currentState = GameState.Editing;
            Debug.Log("Game state changed to editing");
        }
    }
    public void ChangeStateToBillboard()
    {
        if(currentState!=GameState.Billboard)
        {
            currentState = GameState.Billboard;
            Debug.Log("Game state changed to Billboard");
        }
    }
    public void ChangeStateToTown()
    {
        if(currentState!=GameState.Town)
        {
            currentState = GameState.Town;
            Debug.Log("Game state changed to town");
        }
    }
    
}
