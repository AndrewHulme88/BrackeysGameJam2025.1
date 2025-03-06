using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float destroyTime = 5f;

    private Vector2 direction;

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    public void Initialize(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Kill player");
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
