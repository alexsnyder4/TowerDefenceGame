using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Text currencyUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;

    
    private void Update()
    {
        LevelManager.main.menuOpen = isMenuOpen;
    }

    public void ToggleMenu()
    { 

        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }
    private void OnGUI()
    {
        currencyUI.text = "Currency: " + LevelManager.main.currency.ToString();
    }

    public void SetSelected()
    {

    }
}
