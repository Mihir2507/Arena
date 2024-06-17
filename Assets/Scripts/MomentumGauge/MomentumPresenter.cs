using UnityEngine;

public class MomentumPresenter : MonoBehaviour
{
    public static MomentumPresenter Instance { get; private set; }

    private MomentumModel model;
    [SerializeField] private MomentumView view;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional, keeps the instance across scenes if needed

        model = new MomentumModel();
        model.OnMomentumChanged += UpdateView;
    }

    private void Start()
    {
        OverdrivePresenter.Instance.OnOverdriveActivated += model.ActivateOverdrive;
        OverdrivePresenter.Instance.OnOverdriveDeactivated += model.DeactivateOverdrive;
    }

    private void OnDestroy()
    {
        OverdrivePresenter.Instance.OnOverdriveActivated -= model.ActivateOverdrive;
        OverdrivePresenter.Instance.OnOverdriveDeactivated -= model.DeactivateOverdrive;
    }

    private void Update()
    {
        model.UpdateMomentum(Time.deltaTime);
    }

    private void UpdateView()
    {
        view.UpdateMomentumUI(model.CurrentMomentum, model.MaxMomentum, model.Segments);
    }

    public bool CanDeployTroop(TroopStats troopStats)
    {
        return model.CanDeployTroop(troopStats);
    }

    public void DeployTroop(TroopStats troopStats)
    {
        model.DeployTroop(troopStats);
    }
}