// TroopPresenter.cs

using UnityEngine;

public interface ITroopPresenter
{
    void OnSpawnTroop(bool moveForward, Vector3 spawnPosition);
}

public class TroopPresenter : ITroopPresenter
{
    private GameObject troopPrefab;
    // private Transform[] waypoints;

    public TroopPresenter(GameObject prefab)
    {
        troopPrefab = prefab;
        // waypoints = waypointsParent.GetComponentsInChildren<Transform>();
    }

    public void OnSpawnTroop(bool moveForward, Vector3 spawnPosition)
    {
        if (troopPrefab != null)
        {
            GameObject troop = GameObject.Instantiate(troopPrefab, spawnPosition, Quaternion.identity);
            MyTroop myTroop = troop.GetComponent<MyTroop>();
            if (myTroop != null)
            {
                // myTroop.waypoints = waypoints;
                myTroop.moveForward = moveForward;
            }
        }
    }
}