using UnityEngine;

public class TowerTile : ScriptableObject
{
    public Transform TowerTransform { get; private set; }

    public bool IsOccupied { get; private set; } = false;

    public GameObject CurrentTower { get; private set; }

    public void SetTile(Transform tile) 
        => TowerTransform = tile;
    public void SetTowerPosition(Transform transform)
        => TowerTransform = transform;
    public void CreateTower(GameObject tower)
    {
        CurrentTower = tower;

        Instantiate(tower, TowerTransform.position, Quaternion.identity);

        IsOccupied = true;
    }

    public void DestroyTower()
    {
        Destroy(CurrentTower);

        IsOccupied = false;
    }
}
