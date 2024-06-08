// TroopAIManager.cs

using UnityEngine;
using System.Collections;

public class TroopAIManager : MonoBehaviour
{
    #region Fields
    public GameObject troopPrefab;
    public Transform spawnLeft;
    public Transform spawnRight;

    private ITroopPresenter presenter;
    #endregion

    private void Start()
    {
        presenter = new TroopPresenter(troopPrefab);
        StartCoroutine(SpawnTroopsRandomly());
    }

    private IEnumerator SpawnTroopsRandomly()
    {
        while (true)
        {
            bool moveForward = Random.value > 0.5f;
            Vector3 spawnPosition = moveForward ? spawnLeft.position : spawnRight.position;
            presenter.OnSpawnTroop(moveForward, spawnPosition);
            yield return new WaitForSeconds(Random.Range(10, 15)); // Wait for a random time between spawns
        }
    }
}