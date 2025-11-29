using UnityEngine;

public class LetterFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject letterPrefab;

    public void GenerateLetter(Letter.Destination destination)
    {
        GameObject createdLetter = Instantiate(letterPrefab, transform.position, Quaternion.identity);
        Letter letter = createdLetter.GetComponent<Letter>();
        if (letter != null)
        {
            letter.SetDestination(destination);
        }
    }
}
