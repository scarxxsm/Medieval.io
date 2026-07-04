using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    [Header("Asigna aquí tu Panel de Pausa")]
    public GameObject panelPausa;
    
    private bool estaPausado = false;

    void Update()
    {
        // Detecta si presionamos la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estaPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        estaPausado = true;
        Time.timeScale = 0f; // Congela el tiempo del juego por completo
        panelPausa.SetActive(true); // Activa la interfaz del menú
    }

    public void Reanudar()
    {
        estaPausado = false;
        Time.timeScale = 1f; // Descongela el tiempo
        panelPausa.SetActive(false); // Oculta la interfaz
    }

    public void Salir()
    {
        Debug.Log("¡Cerrando el juego!"); 
        Application.Quit(); // Esta línea solo cierra el juego cuando ya está exportado (.exe)
    }
}