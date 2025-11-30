using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [Header("Estado del Juego")]
    public UnityEvent win;
    public UnityEvent loss;
    public int indicecartas = 0;  
    private bool cartasEntregadas = false;
    private bool cartaEnEscena = false;

    [SerializeField] private LetterFactory factory;
    
    [Header("Casas Configuration")]
    [SerializeField] private List<House> casasvecinos = new List<House>(); //la lista de las casas

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
    
    private void Update()
    {
       //CrearCartas
       if (!cartaEnEscena && indicecartas < casasvecinos.Count)
       {
            Debug.Log("Carta");
            factory.GenerateLetter((Letter.Destination)(indicecartas));
            cartaEnEscena = true;
            indicecartas++;
       }
       //crear condicion de ganar 
       if (cartasEntregadas && indicecartas == casasvecinos.Count-1 && !cartaEnEscena)
       {
            Win();
       }
    }
    public void CartaEntregada()
    {
        cartaEnEscena = false ;
    }   
    public void Win()
    {
        win.Invoke();
    }
    public void Loss()
    {
        //se le llamara desde leter con un evento para decir si se le acaba el tiempo

        loss.Invoke();
    }
   


    
}

