using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    //[SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private TowerInfo[] towers;

    private int selectedTower;

    private void Awake() {
        main = this;
    }
    
    //Gets selected towers prefab
    public TowerInfo GetSelectedTower() {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        Debug.Log("Selectedtower" +  _selectedTower);
        selectedTower = _selectedTower;
    }
}
