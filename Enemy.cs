using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform ledgeCheck;
    [SerializeField] float checkDistance = 0.5f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isMovingRight = true;
    [SerializeField] float pauseTime = 3f;

    [Header("Combat")]
    [SerializeField] float playerDetectDistance = 5f;
    [SerializeField] LayerMask playerMask;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float shootCooldown = 1f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isPaused = false;
    private bool canShoot = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(!isPaused)
        {
            anim.SetBool("isMoving", true);
            rb.linearVelocity = new Vector2(isMovingRight ? moveSpeed : -moveSpeed, rb.linearVelocity.y);

            bool wallHit = Physics2D.Raycast(wallCheck.position, Vector2.right * (isMovingRight ? 1 : -1), checkDistance, groundLayer);
            bool noLedge = !Physics2D.Raycast(ledgeCheck.position, Vector2.down, checkDistance, groundLayer);
            RaycastHit2D firstHit = Physics2D.Raycast(wallCheck.position, Vector2.right * (isMovingRight ? 1 : -1), playerDetectDistance, groundLayer | playerMask);

            if(wallHit || noLedge)
            {
                StartCoroutine(PausePatrolling());
            }

            if(firstHit.collider != null && ((1 << firstHit.collider.gameObject.layer) & playerMask) != 0)
            {
                StartCoroutine(Shoot());
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private IEnumerator Shoot()
    {
        if(canShoot)
        {
            canShoot = false;
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            isPaused = true;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Vector2 shootDirection = isMovingRight ? Vector2.right : Vector2.left;
            bullet.GetComponent<Bullet>().Initialize(shootDirection);
            yield return new WaitForSeconds(shootCooldown);
            isPaused = false;
            canShoot = true;
        }
    }

    private IEnumerator PausePatrolling()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        isPaused = true;
        yield return new WaitForSeconds(pauseTime);
        isPaused = false;
        Flip();
    }

    private void Flip()
    {
        isMovingRight = !isMovingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * (isMovingRight ? checkDistance : -checkDistance));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + Vector3.down * checkDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * (isMovingRight ? playerDetectDistance : -playerDetectDistance));
    }
}
