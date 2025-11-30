using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField]
    private Letter.Destination houseDestination;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInteraction player = other.GetComponent<PlayerInteraction>();
        if (player != null && player.withLetter)
        {
            Letter letter = other.GetComponentInChildren<Letter>();
            if (letter != null && letter.GetDestination() == houseDestination)
            {
                letter.Deliver();
                letterRecieved();
            }
        }
    }

    public void letterRecieved()
    {
        Debug.Log("CartaRecivida");
    }

    //Método para remarcar la casa
    public void Highlight(bool active)
    {
        //cambiar color del SpriteRenderer
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = active ? Color.red : Color.white;
        }
    }
}
