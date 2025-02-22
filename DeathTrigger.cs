using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] AudioClip playerFallSound;

    private RespawnManager spawnManager;
    private AudioSource audioSource;

    private void Start()
    {
        spawnManager = FindFirstObjectByType<RespawnManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            audioSource.PlayOneShot(playerFallSound);
            spawnManager.Respawn();
        }

        if(collision.tag == "Box")
        {
            Destroy(collision.gameObject);
        }
    }
}
