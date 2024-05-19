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

    public void EnableNavigationButtons(bool enable)
    {
        TownButton.gameObject.SetActive(enable);
        BoardButton.gameObject.SetActive(enable);
        EditorButton.gameObject.SetActive(enable);
    }
}
