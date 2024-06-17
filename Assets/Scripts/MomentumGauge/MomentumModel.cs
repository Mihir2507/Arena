using System;
using UnityEngine;

public class MomentumModel
{

    public event Action OnMomentumChanged;

    private float maxMomentum = 5f;
    private float currentMomentum = 0f;
    private float passiveFillRate = .25f; // Momentum gained per second
    private float overdriveFillMultiplier = 2f;
    private int segments = 5;
    private bool isOverdriveActive = false;

    public float CurrentMomentum
    {
        get => currentMomentum;
        private set
        {
            currentMomentum = Mathf.Clamp(value, 0, maxMomentum);
            OnMomentumChanged?.Invoke();
        }
    }

    public float MaxMomentum => maxMomentum;
    public float PassiveFillRate => passiveFillRate;
    public int Segments => segments;

    public void UpdateMomentum(float deltaTime)
    {
        float fillRate = isOverdriveActive ? passiveFillRate * overdriveFillMultiplier : passiveFillRate;
        CurrentMomentum += fillRate * deltaTime;
    }

    public bool CanDeployTroop(TroopStats troopStats)
    {
        return CurrentMomentum >= troopStats.momentumCost;
    }

    public void DeployTroop(TroopStats troopStats)
    {
        if (CanDeployTroop(troopStats))
        {
            CurrentMomentum -= troopStats.momentumCost;
        }
        else
        {
            Debug.LogWarning("Not enough momentum to deploy this troop.");
        }
    }

    public void ActivateOverdrive()
    {
        Debug.Log("Momentum increased");
        isOverdriveActive = true;
        OnMomentumChanged?.Invoke();
    }

    public void DeactivateOverdrive()
    {
        Debug.Log("Momentum decreased");
        isOverdriveActive = false;
        OnMomentumChanged?.Invoke();
    }
}