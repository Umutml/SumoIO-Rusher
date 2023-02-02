using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private bool isTimerStarted;
    private int seconds;
    private float timerValue = 90;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI timerText;
    
    private void OnEnable()
    {
        EventManager.OnGameOverEvent += GameOverPanel;
        EventManager.OnGameStartedEvent += StartTimer;
    }

    private void OnDisable()
    {
        EventManager.OnGameOverEvent -= GameOverPanel;
        EventManager.OnGameStartedEvent -= StartTimer;
        
    }

    private void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    private void StartTimer()
    {
        isTimerStarted = true;
    }

    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (!isTimerStarted) return;

        timerValue -= Time.deltaTime;
        seconds = Convert.ToInt32(timerValue % 90);
        timerText.text = seconds.ToString();
        if (timerValue <= 0)
        {
            isTimerStarted = false;
            EventManager.OnGameOver();
        }
    }
}
