using System;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [Header("Estado del Juego")]
    private int indicecartasactual = 0;  
    private bool totalcartasentregadas = false;

    [SerializeField] private LetterFactory factory;
    [Header("Configuración Casas")]
    [SerializeField] public int num = 1; // con estos numeros haremos que salgan mas o menos cartas
    //se generan todas en el mismo objeto de la factory, asi que solo aparezca una
    [Header("Casas Configuration")]
    [SerializeField] private List<HouseData> casasvecinos = new List<HouseData>(); //la lista de las casas
    [System.Serializable]


    public class HouseData
    {
        public Letter.Destination destination;  
        public string casasnombre = "Casa";
        [TextArea(3, 1)] public string instructionText = "";//para ver si funciona, en el debug
    }
    
    //prueba si hace las cartas
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Asignarlettersacasas();
        }
    }
   

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("Game manager esta dando null");
                instance = new GameManager();
                return instance;
            }
            else { return instance; }
        }
    }
    //crear cartas y asignarle un enum, aunq esto ya se hace desde letterfactori
    //puntos, con las cartas y el evento

    public void Asignarlettersacasas()
    {
        indicecartasactual = 0;
        totalcartasentregadas = false;

        // hace que haya la misma cantidad de casas que en num
        while (casasvecinos.Count > num)
        {
            casasvecinos.RemoveAt(casasvecinos.Count - 1);
        }
        while (casasvecinos.Count < num)
        {
            int numcasas = System.Enum.GetValues(typeof(Letter.Destination)).Length;
            if (numcasas > 2)
            {
                Debug.Log("Esta siendo mayor a dos");
            }
            else
            {
                casasvecinos.Add(new HouseData
                {
                    casasnombre = $"Casa{casasvecinos.Count + 1}",
                    destination = (Letter.Destination)UnityEngine.Random.Range(1, numcasas)
                }
                );
            }
        }
        SpawnNextLetter();
    }
    // Genera carta siguiente
    public void SpawnNextLetter()  
    {
        if (indicecartasactual < num)
        {
            HouseData house = casasvecinos[indicecartasactual];
            factory.GenerateLetter(house.destination);
            Debug.Log($"Carta {indicecartasactual + 1} para {house.casasnombre} (Destino: {house.destination})");
            indicecartasactual++;
        }
    }

    public void OnLetterDelivered()  //Llamado desde casas
    {
        if (indicecartasactual < num)
        {
            SpawnNextLetter();  // Genera siguiente
        }
        else
        {
            Debug.Log("¡Todas las cartas entregadas!");
            totalcartasentregadas = true;
        }
    }
}

