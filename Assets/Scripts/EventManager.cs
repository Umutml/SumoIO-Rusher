using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public static event Action OnGameStartedEvent;
    public static event Action OnCollectableTakenEvent;
    public static event Action OnGameOverEvent; 

    public static void OnGameStarted()
    {
        OnGameStartedEvent?.Invoke();
    }

    public static void OnCollectableTaken()
    {
        OnCollectableTakenEvent?.Invoke();
    }

    public static void OnGameOver()
    {
        OnGameOverEvent?.Invoke();
    }
}
