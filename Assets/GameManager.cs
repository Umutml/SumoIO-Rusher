using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool gameStarted = true;
    
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.OnGameStarted();
            gameStarted = true;
        }
    }
}
