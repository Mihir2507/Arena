// TroopView.cs

using UnityEngine;
using UnityEngine.UI;

public class TroopView : MonoBehaviour
{
    #region Fields
    public Button clockwiseButton;
    public Button anticlockwiseButton;
    public Transform spawnLeft;
    public Transform spawnRight;
    public GameObject troopPrefab;

    private ITroopPresenter presenter;
    #endregion

    private void Start()
    {
        presenter = new TroopPresenter(troopPrefab);
        clockwiseButton.onClick.AddListener(OnClockwiseButtonClicked);
        anticlockwiseButton.onClick.AddListener(OnAnticlockwiseButtonClicked);
    }

    private void OnClockwiseButtonClicked()
    {
        presenter.OnSpawnTroop(true, spawnLeft.position);
    }

    private void OnAnticlockwiseButtonClicked()
    {
        presenter.OnSpawnTroop(false, spawnRight.position);
    }
}