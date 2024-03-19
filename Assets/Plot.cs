using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    [SerializeField] private GameObject[] menuItemsRectTransforms;

    private GameObject tower;
    private Color startColor;
    private GameObject child;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (IsMouseOverUI())
        {
            return;
        }
        
        if (tower != null)
        {
            ToggleTowerRangeShadow();
            return;
        }

        TowerInfo towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.towerCost > LevelManager.main.currency)
        {
            return;
        }

        

        LevelManager.main.SpendCurrency(towerToBuild.towerCost);

        Vector3 offset = new Vector3(1.95f, -0.65f, 0f);
        tower = Instantiate(towerToBuild.prefab, transform.position + offset, Quaternion.identity);
    }

    private bool IsMouseOverUI()
    {
        // Check if the mouse is over a UI element
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void ToggleTowerRangeShadow()
    {
        child = tower.transform.Find("RangeShadow").gameObject;
        child.SetActive(!child.activeSelf);
    }
}
