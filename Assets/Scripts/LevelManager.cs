using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [SerializeField]
    public Transform startPoint;
    [SerializeField]
    public Transform[] path;

    [SerializeField]
    public bool menuOpen;

    public int currency;
    private void Awake()
    {
        main = this;
    }

    private void Start ()
    {
        currency = 1000;
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
            Debug.Log("Not enough to purchase"); //will tie into UI eventually
            return false;
        }
    }
}
