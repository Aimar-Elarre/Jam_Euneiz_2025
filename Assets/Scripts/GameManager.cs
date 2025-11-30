using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [Header("Estado del Juego")]
    public UnityEvent win;
    public UnityEvent loss;
    private int indicecartas = 0;  
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
        CreateLeter();
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
    
    private void Update()
    {
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
    private void CreateLeter()
    {
      while (indicecartas < casasvecinos.Count)
      {
            if (!cartaEnEscena)
            {
                if(indicecartas == 0)
                {
                    factory.GenerateLetter(Letter.Destination.House1);
                }
                else if (indicecartas == 1)
                {
                    factory.GenerateLetter(Letter.Destination.House2);
                }
                else if (indicecartas == 2)
                {
                    factory.GenerateLetter(Letter.Destination.House3);
                }
                else if (indicecartas == 3)
                {
                    factory.GenerateLetter(Letter.Destination.House4);
                }
                else if (indicecartas == 4)
                {
                    factory.GenerateLetter(Letter.Destination.House5);
                }
                cartaEnEscena = true;
                indicecartas++;
            }
      }
    }
    private void Win()
    {
        win.Invoke();
    }
    private void Loss()
    {
        //se le llamara desde leter con un evento para decir si se le acaba el tiempo

        loss.Invoke();
    }
   


    
}

