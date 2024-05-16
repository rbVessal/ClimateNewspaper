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
    [SerializeField] private GameObject EditorCanvas;
    [SerializeField] private GameObject BillboardCanvas;
    [SerializeField] private GameObject TownCanvas;

    public static GameStateManager Main;
    
    private void Awake()
    {
        Main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
           //ChangeStateToTown();
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
            EditorCanvas.SetActive(true);
            TownCanvas.SetActive(false);
            BillboardCanvas.SetActive(false);
            FindObjectOfType<NewspaperManager>().UpdateArticles();
            Debug.Log("Game state changed to editing");
        }
    }
    public void ChangeStateToBillboard()
    {
        if(currentState!=GameState.Billboard)
        {
            currentState = GameState.Billboard;
            EditorCanvas.SetActive(false);
            TownCanvas.SetActive(false);
            BillboardCanvas.SetActive(true);
            FindObjectOfType<BulletinManager>().UpdateBillBoardUI();
            Debug.Log("Game state changed to Billboard");
        }
    }
    public void ChangeStateToTown()
    {
        if(currentState!=GameState.Town)
        {
            currentState = GameState.Town;
            EditorCanvas.SetActive(false);
            TownCanvas.SetActive(true);
            BillboardCanvas.SetActive(false);
            Debug.Log("Game state changed to town");
        }
    }

    
}
