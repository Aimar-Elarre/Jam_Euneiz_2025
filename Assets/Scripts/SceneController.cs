using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as SceneController;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        CargarEscena(1);
    }
    protected virtual void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
    public void CargarEscena(int scen)
    {

        SceneManager.LoadScene(scen);
    }
    public void CerrarEscena(int scen)
    {
        SceneManager.UnloadSceneAsync(scen);
    }
}