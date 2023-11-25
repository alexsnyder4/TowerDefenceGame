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

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bps = 1f; //Bullets per second
    [SerializeField] public Sprite[] sprites;

    private Transform target;
    private float timeUntilFire;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() 
    {
        if(target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if(!CheckTargetIsInRange())
        {
            target = null;
        } 
        else 
        {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >=1f/bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
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
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
        if(target != null)
        { 
        Vector3 directionToEnemy = target.transform.position - transform.position;
        float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x);

        float angleInDegrees = angle * Mathf.Rad2Deg;

        int quadrant = GetQuadrant(angleInDegrees);
        SwitchSprite(quadrant);
        }
        else
        {
            spriteRenderer.sprite = sprites[2];
        }
    }

    private int GetQuadrant(float angle)
        {
            if (angle >= -45 && angle < 45)
            {
                 // Bottom right
                spriteRenderer.flipX = true;
                return 3;
            }
            else if (angle >= 45 && angle < 135)
            {
                 // Top right
                spriteRenderer.flipX = true;
                return 1;
            }
            else if (angle >= -135 && angle < -45)
            {
                spriteRenderer.flipX = false;
                return 2; // Bottom left
            }
            else
            {
                spriteRenderer.flipX = false;
                return 0; // Top left
            }
                
        }

    void SwitchSprite(int quadrant)
    {
        spriteRenderer.sprite = sprites[quadrant];
    }
}
