using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Movement : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField]
    private float moveSpeed = 1.0f;

    private Transform target;
    private int pathIndex = 0;

    private float baseSpeed;

    private Animator animator;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();


        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                LevelManager.main.KingdomHit();
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
        
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        
        
        if (direction.x > 0 && direction.y < 0)
        {
            Debug.Log(direction);
            animator.SetInteger("MvmtDirection", 0);
            sr.flipX = true;
        }
        else if (direction.x > 0 && direction.y > 0)
        {
            animator.SetInteger("MvmtDirection", 1);
            sr.flipX = true;
        }
        else if (direction.x < 0 && direction.y < 0)
        {
            animator.SetInteger("MvmtDirection", 0);
            sr.flipX = false;

        }
        else if (direction.x < 0 && direction.y > 0)
        {
            animator.SetInteger("MvmtDirection", 1);
            sr.flipX = false;

        }

        rb.velocity = direction * moveSpeed;
    }


    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }
}
