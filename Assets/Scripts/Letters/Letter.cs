using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Letter : MonoBehaviour
{
    public UnityEvent entregada;
    public UnityEvent perdiste;//crear el temporizador
    public enum Destination { House1, House2, House3, House4, House5 }
    private Destination destination;

    private bool pickedUp = false;

    private Transform playerTransform;
    private PlayerInteraction player;
    [Header("Temporizador")]
    public float timeRemaining = 120f;   // Tiempo inicial en segundos
    public bool isRunning = false;
    public TextMeshProUGUI countdownText;

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
        isRunning = true;
    }

    public void Deliver()
    {
        entregada.Invoke();
        player.withLetter = false;
        Destroy(gameObject);
    }
    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                countdownText.text = Mathf.Ceil(timeRemaining).ToString();
            }
            else
            {
                perdiste.Invoke();
                player.withLetter = false;
                Destroy(gameObject);
                timeRemaining = 0;
                isRunning = false;
                countdownText.text = "¡Tiempo!";
            }
        }
    }
}