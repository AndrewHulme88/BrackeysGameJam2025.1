using System.Collections;
using UnityEngine;

public class EnemySpy : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float waitTime = 3f;
    [SerializeField] bool loop = true;
    [SerializeField] float playerDetectDistance = 10f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float attackMoveSpeed = 10f;

    private int currentPointIndex = 0;
    private bool movingForward = true;
    private bool isWaiting = false;
    private bool isAttacking = false;
    private PlayerController playerController;

    void Update()
    {
        if(isAttacking)
        {
            Attack();
            return;
        }

        RaycastHit2D playerDetected = Physics2D.Raycast(transform.position, Vector2.down, playerDetectDistance, playerLayer);

        if(playerDetected)
        {
            isAttacking = true;
            playerController = playerDetected.collider.GetComponent<PlayerController>();
        }

        if (!isWaiting && patrolPoints.Length > 0 && !isAttacking)
        {
            MoveToNextPoint();
        }
    }

    private void Attack()
    {
        if(playerController != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerController.gameObject.transform.position, attackMoveSpeed * Time.deltaTime);
        }
        else
        {
            isAttacking = false;
        }    
    }

    private void MoveToNextPoint()
    {
        Transform targetPoint = patrolPoints[currentPointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            StartCoroutine(WaitAtPoint());
        }
    }

    private IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        UpdatePatrolPoints();
    }

    private void UpdatePatrolPoints()
    {
        if(loop)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
        else
        {
            if(movingForward)
            {
                currentPointIndex++;

                if(currentPointIndex >= patrolPoints.Length)
                {
                    movingForward = false;
                    currentPointIndex = patrolPoints.Length - 2;
                }
            }
            else
            {
                currentPointIndex--;

                if(currentPointIndex < 0)
                {
                    movingForward = true;
                    currentPointIndex = 1;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Kill Player
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * playerDetectDistance);
    }
}
