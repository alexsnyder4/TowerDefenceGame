using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    private GameObject tower;
    private Color startColor;
    private GameObject child;

    private void Start()
    {
        startColor = sr.color;
  
    }
    private void OnMouseEnter() {
        sr.color = hoverColor;
        
    }
    private void OnMouseExit() {
        sr.color = startColor;
    }

    private void OnMouseDown() {
        
        Debug.Log(Input.mousePosition);
        if (tower != null)
        {
            
            child = tower.transform.Find("RangeShadow").gameObject;
            if (child.activeSelf == false)
            {
                child.SetActive(true);
            }
            else
            {
                child.SetActive(false);
            }

            return;
        }

        Vector3 offset = new Vector3(1.95f, -0.65f, 0f);
        TowerInfo towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.towerCost > LevelManager.main.currency)
        {
                
                return;
        }

        if (LevelManager.main.menuOpen && (Input.mousePosition.x >= 2258.0f || Input.mousePosition.y <= 93.0f))
        {
            return;
        }
        LevelManager.main.SpendCurrency(towerToBuild.towerCost);

        tower = Instantiate(towerToBuild.prefab, transform.position + offset, Quaternion.identity);
    }
    
}
