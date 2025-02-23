using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] ParticleSystem walkParticles;
    [SerializeField] ParticleSystem landParticles;
    [SerializeField] GameObject particleContainer;
    [SerializeField] GameObject deathParticles;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip landSound;
    [SerializeField] AudioClip walkSound;
    [SerializeField] AudioSource oneShotAudioSource;
    [SerializeField] AudioSource walkAudioSource;
    [SerializeField] float coyotyeTime = 0.2f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private bool wasGrounded;
    private RespawnManager respawnManager;
    private Vector3 particlesOriginalScale;
    private bool isMoving = false;
    private AudioManager audioManager;
    private bool isFrozen = false;
    private float coyotyeTimeCounter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        respawnManager = FindFirstObjectByType<RespawnManager>();
        particlesOriginalScale = walkParticles.transform.localScale;
        walkAudioSource.loop = true;
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    private void Update()
    {
        if(isFrozen)
        {
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if(isGrounded)
        {
            coyotyeTimeCounter = coyotyeTime;
        }
        else
        {
            coyotyeTimeCounter -= Time.deltaTime;
        }

        if(!wasGrounded && isGrounded)
        {
            PlayLandParticles();
        }

        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocityY);

        isMoving = moveInput != 0;

        if(moveInput > 0)
        {
            particleContainer.transform.localScale = particlesOriginalScale;
        }
        else if(moveInput < 0)
        {
            particleContainer.transform.localScale = new Vector3(-1, particlesOriginalScale.y, particlesOriginalScale.z);
        }

        if(isMoving && !walkParticles.isPlaying && isGrounded)
        {
            walkParticles.Play();
        }
        else if(!isMoving && walkParticles.isPlaying || !isGrounded)
        {
            walkParticles.Stop();
        }

        if (isMoving && isGrounded)
        {
            if (!walkAudioSource.isPlaying)
            {
                walkAudioSource.clip = walkSound;
                walkAudioSource.Play();
            }
        }
        else
        {
            walkAudioSource.Stop();
        }

        if (Input.GetButtonDown("Jump") && coyotyeTimeCounter > 0f)
        {
            if(jumpSound != null)
            {
                oneShotAudioSource.PlayOneShot(jumpSound);
            }

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            coyotyeTimeCounter = 0f;
        }

        wasGrounded = isGrounded;

        anim.SetBool("isWalking", Mathf.Abs(rb.linearVelocity.x) > 0.1f);
        anim.SetBool("isJumping", rb.linearVelocity.y > 0.1f && !isGrounded);
        anim.SetBool("isFalling", rb.linearVelocity.y < -0.1f && !isGrounded);
    }

    public void FreezePlayer(bool freeze)
    {
        isFrozen = freeze;
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isWalking", false);
        anim.SetBool("isJumping", false);
        anim.SetBool("isFalling", false);
    }

    private void OnDrawGizmosSelected()
    {
        if(groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    private void PlayLandParticles()
    {
        if (landParticles != null)
        {
            landParticles.Play();
        }

        if(landSound != null)
        {
            oneShotAudioSource.PlayOneShot(landSound); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Hazard")
        {
            audioManager.PlaySound();
            GameObject newDeathParticles = Instantiate(deathParticles, transform.position, Quaternion.identity);
            respawnManager?.Respawn();
            DeactivatePlayer();
        }
    }

    public void DeactivatePlayer()
    {
        gameObject.SetActive(false);
    }

    public void ActivatePlayer()
    {
        gameObject.SetActive(true);
    }
}
