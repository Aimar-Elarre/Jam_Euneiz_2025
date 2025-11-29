using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    

    //instancia de las variables de las escenas
    string prinivel;
    string segnivel;
    string trinivel;
    //


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //en las variables hay que poner los nombre de las escenas
        prinivel = SceneManager.GetActiveScene().name;
        prinivel = "";
        segnivel = SceneManager.GetActiveScene().name;
        segnivel = "";
        trinivel = SceneManager.GetActiveScene().name;
        trinivel = "";
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

    // se necesita que cuando se entregen la carta los enemigos tengan un check


    //para cambiar de escena
    //tendra que recivir una señal para cambiar de escenas
    public void Firtslevel() 
    {
        SceneManager.LoadScene(prinivel);
    }
    public void SecondLevel() 
    {
        Debug.Log("Cambio a nivel2");
        SceneManager.LoadScene(segnivel);
    }
    public void ThirdLevel() 
    {
        Debug.Log("Cambio a nivel3");
        SceneManager.LoadScene(trinivel);
    }

}
