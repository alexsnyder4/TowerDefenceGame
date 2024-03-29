using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [SerializeField]
    public Transform startPoint;
    [SerializeField]
    public Transform[] path;
    [SerializeField]
    Text HPtext;
    [SerializeField]
    private int kingdomHP = 15;
    [SerializeField]
    Text loseText;

    [SerializeField]
    public bool menuOpen;

    public int currency;
    private void Awake()
    {
        main = this;
    }

    private void Start ()
    {
        kingdomHP = 15;
        currency = 1000;
        loseText.enabled = false;
    }
    public void Update()
    {
        if (kingdomHP >= 0)
        {
            HPtext.text = "HP: " + kingdomHP.ToString();
        }
        else
        {
            HPtext.text = "HP: 0";
        }
        if(kingdomHP <= 0)
        {
            loseText.text = "YOU LOSE";
            loseText.enabled = true;
            StartCoroutine(BackTimer(2.0f));
        }
    }
    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {

            return false;
        }
    }

    public void KingdomHit(string enemyTag)
    {
        if (enemyTag == "White")
        {
            kingdomHP--;
        }
        else if (enemyTag == "Yellow")
        {
            kingdomHP = kingdomHP - 2;
        }
        else if (enemyTag == "Red")
        {
            kingdomHP = kingdomHP - 5;
        }
    }

    IEnumerator BackTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("LevelSelect");
    }
}
