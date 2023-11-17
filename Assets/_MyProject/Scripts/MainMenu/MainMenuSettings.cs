using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSettings : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    private void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ShowPanel);
    }

    void ShowPanel()
    {
        settingsPanel.SetActive(true);
    }
}
