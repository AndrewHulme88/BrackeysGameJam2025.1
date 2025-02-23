using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] GameObject landParticles;
    [SerializeField] Transform particleSpawnPoint;
    [SerializeField] AudioClip hitGroundSound;

    private CapsuleCollider2D capsuleCollider;
    private AudioSource audioSource;
    private LoopingAudioManager loopingAudioManager;
    private Rigidbody2D rb;
    private bool isDestroyed = false;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        loopingAudioManager = FindFirstObjectByType<LoopingAudioManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDestroyed) return;

        if(rb.linearVelocityX > 0.1f || rb.linearVelocityX < -0.1f)
        {
            loopingAudioManager.PlaySound();
        }
        else
        {
            loopingAudioManager.StopSound();
        }
    }

    public void StopSound()
    {
        isDestroyed = true;
        loopingAudioManager.StopSound();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Hazard")
        {
            Destroy(gameObject);
        }

        if(collision.tag == "Ground")
        {
            audioSource.PlayOneShot(hitGroundSound);
            Instantiate(landParticles, particleSpawnPoint.position, Quaternion.identity);
            capsuleCollider.enabled = false;
        }
    }
}
