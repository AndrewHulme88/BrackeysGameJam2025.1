using UnityEngine;

public class ArrowTrigger : MonoBehaviour
{
    [SerializeField] GameObject arrow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(arrow != null && collision.tag == "Player")
        {
            arrow.SetActive(true);
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(arrow != null && collision.tag == "Player")
        {
            arrow.SetActive(false);
        }
    }
}
