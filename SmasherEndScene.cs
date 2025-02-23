using System.Collections;
using UnityEngine;

public class SmasherEndScene : MonoBehaviour
{
    [SerializeField] float delayTime = 5f;

    private Animator anim;
    private ScreenTransition screenTransition;

    void Start()
    {
        anim = GetComponent<Animator>();
        screenTransition = FindFirstObjectByType<ScreenTransition>();
        StartCoroutine(Smash());
    }

    IEnumerator Smash()
    {
        yield return new WaitForSeconds(delayTime);
        anim.SetTrigger("smash");
        StartCoroutine(GoToCredits());
    }

    IEnumerator GoToCredits()
    {
        yield return new WaitForSeconds(delayTime);
        screenTransition.LoadScene("Credits");
    }
}
