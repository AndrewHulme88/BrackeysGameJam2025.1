using UnityEngine;

public class ButtonSpawnObject : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Transform spawnLocation;
    [SerializeField] AudioClip clickSound;

    private GameObject spawnedObject;
    private bool isPlayerInRange = false;
    private Animator anim;
    private AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("up", true);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(spawnedObject == null)
        {
            anim.SetBool("up", true);
        }
        else
        {
            anim.SetBool("up", false);
        }

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (spawnedObject == null)
            {
                audioSource.PlayOneShot(clickSound);
                PressButton();
            }
        }
    }

    private void PressButton()
    {
        spawnedObject = Instantiate(objectToSpawn, spawnLocation.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
