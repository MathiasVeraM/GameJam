using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    void Awake()
    {
        // Verificamos si ya existe una instancia para evitar duplicados
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Esto hace que este objeto persista entre escenas
        }
        else
        {
            Destroy(gameObject); // Destruye la copia que se intenta crear en la nueva escena
            return;
        }
    }

    public void OnClick(){
        SceneManager.LoadScene(1);
    }
}
