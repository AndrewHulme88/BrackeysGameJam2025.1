using UnityEngine;

public class DestroyMusic : MonoBehaviour
{
    private MusicManager musicManager;

    void Start()
    {
        musicManager = FindFirstObjectByType<MusicManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(musicManager != null)
        {
            musicManager.StopMusic();
        }
    }
}
