using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RespawnManager : MonoBehaviour
{
    private ScreenTransition screenTransition;

    private void Start()
    {
        screenTransition = FindFirstObjectByType<ScreenTransition>();
    }

    public void Respawn()
    {
        screenTransition.ReloadCurrentScene();
    }
}
