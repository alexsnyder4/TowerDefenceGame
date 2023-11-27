using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;

    [SerializeField] private int currencyWorth = 50;


    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private IEnumerator DeathEvent()
    {
        Debug.Log("In death anim coroutine now");
        anim.SetTrigger("death");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;

        
        if (hitPoints <= 0 )
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            StartCoroutine(DeathEvent());
            
        }
    }
}
