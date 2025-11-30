using UnityEngine;

public class Cursor : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        UnityEngine.Cursor.visible = false;
    }
}
