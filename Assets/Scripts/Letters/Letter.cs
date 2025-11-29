using UnityEngine;

public class Letter : MonoBehaviour
{
    public enum Destination { House1, House2, House3 }

    private bool pickedUp = false;

    private Transform playerTransform;
    private PlayerInteraction player;

    public void Start()
    {
        playerTransform = FindAnyObjectByType<PlayerInteraction>().transform;
        player = playerTransform.GetComponent<PlayerInteraction>();
    }
    public void OnInteract()
    {
        Debug.Log("oninteract");
        if (!pickedUp)
        {
            PickUp();
        }
    }
    private void PickUp()
    {
        Debug.Log("Pickedup");
        pickedUp = true;
        player.withLetter = true;

        transform.GetComponent<Collider2D>().enabled = false;


        transform.SetParent(playerTransform);
        transform.position = playerTransform.position;
    }
}