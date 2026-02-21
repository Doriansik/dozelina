using System;

public static class MinigameEvents
{
    public static event Action OnMinigameSuccess;
    public static bool PendingSuccess { get; private set; }

    public static void RaiseMinigameSuccess()
    {
        PendingSuccess = true;
        OnMinigameSuccess?.Invoke();
    }

    public static void ConsumeSuccess()
    {
        PendingSuccess = false;
    }
}