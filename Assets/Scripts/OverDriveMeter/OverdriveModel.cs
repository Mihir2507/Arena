using System;
using UnityEngine;

public class OverdriveModel
{
    public float CurrentOverdrive { get; private set; }
    public float MaxOverdrive { get; private set; }
    public int Segments { get; private set; }
    public bool IsInOverdriveMode { get; private set; }

    public event Action OnOverdriveChanged;
    public event Action OnOverdriveActivated;
    public event Action OnOverdriveDeactivated;

    private float decreaseRate = 1f; // Rate at which the overdrive meter decreases
    private float overdriveDuration = 10f;

    public OverdriveModel(float maxOverdrive, int segments)
    {
        MaxOverdrive = maxOverdrive;
        Segments = segments;
        CurrentOverdrive = 0; //Ensure it starts at the Zero[0]
    }

    public void UpdateOverdrive(float deltaTime)
    {
        if (!IsInOverdriveMode)
        {
            CurrentOverdrive -= decreaseRate * deltaTime;
            CurrentOverdrive = Mathf.Clamp(CurrentOverdrive, 0, MaxOverdrive);
            OnOverdriveChanged?.Invoke();
        }
    }

    public void IncreaseOverdrive(float amount)
    {
        if (!IsInOverdriveMode)
        {
            CurrentOverdrive += amount;
            if (CurrentOverdrive >= MaxOverdrive)
            {
                CurrentOverdrive = MaxOverdrive;
                ActivateOverdriveMode();
            }
            OnOverdriveChanged?.Invoke();
        }
    }

    // private void ActivateOverdriveMode()
    // {
    //     IsInOverdriveMode = true;
    //     // Invoke the method to deactivate overdrive mode after the duration
    //     UnityEngine.MonoBehaviour.Invoke("DeactivateOverdriveMode", overdriveDuration);
    // }

    // private void DeactivateOverdriveMode()
    // {
    //     IsInOverdriveMode = false;
    //     CurrentOverdrive = 0;
    //     OnOverdriveChanged?.Invoke();
    // }
    private void ActivateOverdriveMode()
    {
        Debug.Log("Model activated the overdrive.");
        IsInOverdriveMode = true;
        OnOverdriveActivated?.Invoke();
    }

    public void DeactivateOverdriveMode()
    {
        IsInOverdriveMode = false;
        CurrentOverdrive = 0;
        OnOverdriveDeactivated?.Invoke();
        OnOverdriveChanged?.Invoke();
    }
}