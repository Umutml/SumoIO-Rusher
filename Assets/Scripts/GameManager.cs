using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        EventManager.OnGameOverEvent += GameOver;
        
    }

    private void OnDisable()
    {
        EventManager.OnGameStartedEvent -= StartGame;
        EventManager.OnGameOverEvent -= GameOver;
    }

    private void StartGame()
    {
        gameStarted = true;
    }

    private void GameOver()
    {
        gameStarted = false;
        Invoke(nameof(RestartGame),2f);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}