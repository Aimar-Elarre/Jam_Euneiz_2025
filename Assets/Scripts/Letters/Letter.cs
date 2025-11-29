using Unity.VisualScripting;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public enum Destination { House1, House2, House3, House4, House5 }
    private Destination destination;

    private bool pickedUp = false;

    private Transform playerTransform;
    private PlayerInteraction player;

    public void Start()
    {
        playerTransform = FindAnyObjectByType<PlayerInteraction>().transform;
        player = playerTransform.GetComponent<PlayerInteraction>();
    }

    public void SetDestination(Destination des) { destination = des; }
    public Destination GetDestination() { return destination; }

    public void OnInteract()
    {
        if (!pickedUp)
        {
            PickUp();
        }
    }
    private void PickUp()
    {
        pickedUp = true;
        player.withLetter = true;

        transform.GetComponent<Collider2D>().enabled = false;


        transform.SetParent(playerTransform);
        transform.position = playerTransform.position;
    }

    public void Deliver()
    {
        player.withLetter = false;
        Destroy(gameObject);
    }
}