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

    private Transform target;
    
    public void SetTarget(Transform _target)
    {
        if (_target == null)
        {
            Destroy(gameObject);
            Debug.Log("Trying to destroy the bullet because tager is null");
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
        Debug.Log("Collided with " + other);
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }
}
