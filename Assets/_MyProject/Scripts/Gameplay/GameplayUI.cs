using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] Button pauseButton;
    [SerializeField] PausePanel pausePanel;
    [SerializeField] LosePanel losePanel;
    [SerializeField] TextMeshProUGUI moneyDisplay;
    [SerializeField] WinPanel winPanel;

    private void OnEnable()
    {
        pauseButton.onClick.AddListener(Pause);
        BaseHealthHandler.ILost += Lost;
        GameplayManager.UpdatedMoney += ShowMoney;
        WaveProgress.Won += Won;
    }

    private void OnDisable()
    {
        pauseButton.onClick.RemoveListener(Pause);
        BaseHealthHandler.ILost -= Lost;
        GameplayManager.UpdatedMoney -= ShowMoney;
        WaveProgress.Won -= Won;
    }

    private void Start()
    {
        ShowMoney();
    }

    private void Won()
    {
        winPanel.Setup();
    }

    void Pause()
    {
        pausePanel.Setup();
    }

    void Lost()
    {
        StartCoroutine(LostRoutine());
    }

    IEnumerator LostRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        losePanel.Setup();
    }

    private void ShowMoney()
    {
        moneyDisplay.text = GameplayManager.Instance.Money.ToString();
    }
}
