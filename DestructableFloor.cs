using System.Collections;
using UnityEngine;

public class DestructableFloor : MonoBehaviour
{
    [SerializeField] GameObject particles;
    [SerializeField] Transform spawnPoint;
    [SerializeField] ScreenShake screenShake;
    [SerializeField] GameObject dialogManager;
    [SerializeField] GameObject canvas;

    private Animator anim;
    private AudioManager audioManager;
    private PlayerController playerController;
    private BoxCollider2D boxCollider;


    private void Start()
    {
        anim = GetComponent<Animator>();
        audioManager = FindFirstObjectByType<AudioManager>();
        playerController = FindFirstObjectByType<PlayerController>();
        boxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(DelayedFreeze());
    }

    IEnumerator DelayedFreeze()
    {
        yield return new WaitForEndOfFrame();
        playerController.FreezePlayer(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Box")
        {
            anim.SetTrigger("fall");
            audioManager.PlaySound();
            Destroy(audioManager, 3f);
            screenShake.TriggerShake();
            GameObject newParticles = Instantiate(particles, spawnPoint.transform.position, Quaternion.identity);
            StartCoroutine(SetDialogActive());
        }
    }

    public void DestroyFloor()
    {
        Destroy(gameObject, 4.1f);
    }

    IEnumerator SetDialogActive()
    {
        yield return new WaitForSeconds(3f);
        canvas.SetActive(true);
        dialogManager.SetActive(true);
    }
}
