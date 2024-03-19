using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerCostUI : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField]
    TowerInfo tower;
    // Start is called before the first frame update
    void Start()
    {
        text.text = tower.towerName.ToString() + "\n" + tower.towerCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
