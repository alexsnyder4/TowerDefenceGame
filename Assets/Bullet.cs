using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 7.5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float freezeEffect = 1;

    private Transform target;
    
    public void SetTarget(Transform _target)
    {
        if (_target == null)
        {
            Destroy(gameObject);
           // Debug.Log("Trying to destroy the bullet because tager is null");
            return ;
        }
        target = _target;
    }

    private void FixedUpdate() 
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);
        Color origColor = other.gameObject.GetComponent<SpriteRenderer>().color;
        float red = origColor.r;
        float green = origColor.g;
        float blue = origColor.b;
        
        if (freezeEffect < 1)
        {
            float currentSpeed = other.gameObject.GetComponent<Enemy1Movement>().GetSpeed();
            if (currentSpeed > 0.5f) 
            {
                other.gameObject.GetComponent<Enemy1Movement>().UpdateSpeed(freezeEffect * currentSpeed);
                Color frozenColor = new Color(0.65f * red, 0.8f * green, 0.11f + blue, 1f);
                other.gameObject.GetComponent<SpriteRenderer>().color = frozenColor;
            }
        }
    }
}
