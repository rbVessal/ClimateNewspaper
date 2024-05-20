using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Page : MonoBehaviour
{
    [SerializeField]
    GameObject headerText;
    [SerializeField]
    GameObject dayText;

    public bool shouldShowHeaderText = true;
    public bool shouldShowDayText = true;

    // Start is called before the first frame update
    void Start()
    {
        headerText.SetActive(shouldShowHeaderText);
        dayText.SetActive(shouldShowDayText);
    }

    public void UpdateDay()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        TextMeshProUGUI textMeshPro = dayText.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = "Day " + (gameManager.GetDay() + 1);
    }
}
