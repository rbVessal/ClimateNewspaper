using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private Button StartDayButton;

    [SerializeField]
    private Button TownButton;
    [SerializeField]
    private Button BoardButton;
    [SerializeField]
    private Button EditorButton;

    // Start is called before the first frame update
    void Start()
    {
        TownButton.gameObject.SetActive(true);
        BoardButton.gameObject.SetActive(true);
        EditorButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
