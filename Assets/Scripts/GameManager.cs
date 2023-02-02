using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool gameStarted;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
            EventManager.OnGameStarted();
    }
    private void OnEnable()
    {
        EventManager.OnGameStartedEvent += StartGame;
    }

    private void OnDisable()
    {
        EventManager.OnGameStartedEvent -= StartGame;
    }

    private void StartGame()
    {
        gameStarted = true;
    }
}