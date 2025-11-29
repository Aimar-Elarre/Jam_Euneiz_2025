using System;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    

    [SerializeField] private LetterFactory factory;
    [Header("Configuración Casas")]
    [SerializeField][Range(1, 5)] public int num = 1; // con estos numeros haremos que salgan mas o menos cartas
    //se generan todas en el mismo objeto de la factory, asi que solo aparezca una
    [Header("Casas Configuration")]
    [SerializeField] private List<HouseData> casasvecinos = new List<HouseData>(); //la lista de las casas
    [System.Serializable]
    public class HouseData
    {
        public Letter.Destination destination;
        public string casasnombre = "Casa";//para ver si funciona, en el debug
    }
    
    //prueba si hace las cartas
    /*
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Asignarlettersacasas();
        }
    }
    */

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
        
        // hace que haya la misma cantidad de casas que en num
        while (casasvecinos.Count > num)
        {
            casasvecinos.RemoveAt(casasvecinos.Count - 1);
        }
        while (casasvecinos.Count < num)
        {
            casasvecinos.Add(new HouseData { casasnombre = $"Casa{casasvecinos.Count + 1}" });
        }
        // Crea cartas SOLO para 'num' casas
        for (int i = 0; i < num; i++)
        {
            HouseData house = casasvecinos[i];
            factory.GenerateLetter(house.destination);
            Debug.Log($"Carta {i + 1} para {house.casasnombre}");
            
        }
        
    } 
}

