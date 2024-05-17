using System.Collections;
using System.Collections.Generic;
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
}
