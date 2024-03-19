using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask  enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator anim;
 
    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float bps = 1f; //Bullets per second

    private Transform target;
    private float timeUntilFire;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update() 
    {
        if(target == null)
        {
            anim.SetBool("isIdle", true);
            anim.SetInteger("isCasting", 0);
            FindTarget();
            return;
        }

        if(!CheckTargetIsInRange())
        {
            target = null;
        } 
        else 
        {
            if(timeUntilFire <= 0)
            {
                //Shoot();
            }
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >=1f/bps)
            {
                

            
            timeUntilFire = 0f; // Reset the timer immediately

            // Shoot immediately and then wait for the next time interval
            Shoot();
            }
        }
        if(target != null)
        { 
            
        }
        else
        {
            
            //spriteRenderer.sprite = sprites[2];
        }
    }

    private void Shoot()
    {
        Vector3 directionToEnemy = target.transform.position - transform.position;
        float angle1 = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x);

        float angleInDegrees = angle1 * Mathf.Rad2Deg;
        GetQuadrant(angleInDegrees);
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        Vector3 directionToTarget = target.position - firingPoint.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // Set the rotation of the bullet to point towards the target
        bulletScript.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 180));

        // Pass the target to the bullet script
        bulletScript.SetTarget(target);    
    }
    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (UnityEngine.Vector2)transform.position, 0f, enemyMask);

        if(hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
        
    }
    private bool CheckTargetIsInRange() {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }
    private void FixedUpdate()
    {
        
    }

    private int GetQuadrant(float angle)
        {
            if (angle >= -45 && angle < 45)
            {
                 // Bottom right
                anim.SetInteger("isCasting", 1);
                spriteRenderer.flipX = true;
                return 3;
            }
            else if (angle >= 45 && angle < 135)
            {
                 // Top right
                anim.SetInteger("isCasting", 2);
                spriteRenderer.flipX = true;
                return 1;
            }
            else if (angle >= -135 && angle < -45)
            {
                anim.SetInteger("isCasting", 1);
                spriteRenderer.flipX = false;
                return 2; // Bottom left
            }
            else
            {
                anim.SetInteger("isCasting", 2);
                spriteRenderer.flipX = false;
                return 0; // Top left
            }
                
        }
}
