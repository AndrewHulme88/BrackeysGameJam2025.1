using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject deathParticles;
    [SerializeField] AudioClip splatSound;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Box")
        {
            audioManager.PlaySpecificSound(splatSound);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
