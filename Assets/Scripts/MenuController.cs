using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void Start()
    {

    }
    public void IrAScena(int escena)
    {
        SceneController.Instance.CargarEscena(escena);
    }
    public void CerrarScena(int escena)
    {
        SceneController.Instance.CerrarEscena(escena);
    }
    public void Salir()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}