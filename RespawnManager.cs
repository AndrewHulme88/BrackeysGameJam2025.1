using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] float respawnDelay = 1f;

    private PlayerController player;
    private Vector2 respawnPoint;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        respawnPoint = player.transform.position;
    }

    public void Respawn()
    {
        StartCoroutine(StartRespawn());
    }

    IEnumerator StartRespawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        player.transform.position = respawnPoint;
        player.ActivatePlayer();
    }
}
