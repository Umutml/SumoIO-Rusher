using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public static event Action OnGameStartedEvent;
    public static event Action OnCollectableTakenEvent;

    public static void OnGameStarted()
    {
        OnGameStartedEvent?.Invoke();
    }

    public static void OnCollectableTaken()
    {
        OnCollectableTakenEvent?.Invoke();
    }
}
