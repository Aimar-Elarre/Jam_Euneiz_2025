using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public bool withLetter = false;
    [SerializeField]
    private float interactionRange = 0.8f;

    private InputSystem_Actions inputActions;
    private bool Interaction = false;


    private void Awake()
    {
        inputActions.Player.Interact.performed += ctx => Interaction = true;
        inputActions.Player.Interact.canceled += ctx => Interaction = false;
    }
   
    private void FixedUpdate()
    {
        Debug.Log("e");

        if (!withLetter && Interaction == true)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactionRange);

            Letter nearestLetter = null;
            float lowerDistance = interactionRange + 1f;

            //por cada collider que toca
            foreach (var c in hitColliders)
            {
                Debug.Log("col");
                //busca un plato
                Letter letter = c.GetComponent<Letter>();
                if (letter != null)
                {
                    //el más cercano
                    float distance = Vector2.Distance(transform.position, letter.transform.position);
                    if (distance < lowerDistance)
                    {
                        //actualiza el plato
                        lowerDistance = distance;
                        nearestLetter = letter;
                    }
                }
            }

            if (nearestLetter != null)
            {
                //interactua con el plato más cercano
                nearestLetter.OnInteract();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
