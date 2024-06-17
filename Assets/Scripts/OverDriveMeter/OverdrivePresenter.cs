using System;
using UnityEngine;

public class OverdrivePresenter : MonoBehaviour
{
    public static OverdrivePresenter Instance { get; private set; }

    public event Action OnOverdriveActivated;
    public event Action OnOverdriveDeactivated;

    private bool isOverdriveActive = false;

    private OverdriveModel model;
    [SerializeField] private OverdriveView view;
    [SerializeField] private float maxOverdrive = 100f;
    [SerializeField] private int segments = 10;
    private float overdriveDuration = 10f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional, keeps the instance across scenes if needed

        model = new OverdriveModel(maxOverdrive, segments);
        model.OnOverdriveChanged += UpdateView;
        model.OnOverdriveActivated += HandleOverdriveActivated;
        // model.OnOverdriveDeactivated += HandleOverdriveDeactivated;
    }

    private void Update()
    {
        model.UpdateOverdrive(Time.deltaTime);
    }

    // public void TriggerOverdrive()
    // {
    //     if (!isOverdriveActive)
    //     {
    //         isOverdriveActive = true;
    //         OnOverdriveActivated?.Invoke();
    //         Invoke("EndOverdrive", overdriveDuration);
    //     }
    // }

    private void EndOverdrive()
    {
        isOverdriveActive = false;
        model.DeactivateOverdriveMode();
        OnOverdriveDeactivated?.Invoke();
    }

    private void UpdateView()
    {
        view.UpdateOverdriveUI(model.CurrentOverdrive, model.MaxOverdrive, model.Segments);
    }

    public void IncreaseOverdrive(float amount)
    {
        model.IncreaseOverdrive(amount);
    }

    public bool IsInOverdriveMode()
    {
        return model.IsInOverdriveMode;
    }

    private void HandleOverdriveActivated()
    {
        // Activate overdrive mode in the game
        Debug.Log("Overdrive mode activated! by presenter");
        isOverdriveActive = true;
        OnOverdriveActivated?.Invoke();
        Invoke("EndOverdrive", overdriveDuration);
        // Schedule deactivation of overdrive mode
        // Invoke("DeactivateOverdriveMode", overdriveDuration);
    }

    // private void HandleOverdriveDeactivated()
    // {
    //     OnOverdriveDeactivated?.Invoke();
    //     // Notify subscribers about overdrive mode deactivation
    //     Debug.Log("Overdrive mode deactivated.");
    // }

    // private void DeactivateOverdriveMode()
    // {
    //     model.DeactivateOverdriveMode();
    //     Debug.Log("Overdrive mode deactivated.");
    // }
}