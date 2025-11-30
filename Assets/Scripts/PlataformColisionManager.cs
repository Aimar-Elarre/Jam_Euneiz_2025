using UnityEngine;

public class PlataformColisionManager : MonoBehaviour
{
    [Header("Collider")]
    public Collider2D plataformas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        plataformas.enabled = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        plataformas.enabled = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        plataformas.enabled=true;
    }
}
