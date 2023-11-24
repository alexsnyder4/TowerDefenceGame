using System;
using UnityEngine;

[Serializable]
public class TowerInfo
{
    public string towerName;
    public int towerCost;
    public GameObject prefab;

    public TowerInfo(string towerName, int towerCost, GameObject prefab)
    {
        this.towerName = towerName;
        this.towerCost = towerCost;
        this.prefab = prefab;
    }
}
