using UnityEngine;
using TMPro; // Necesario para los textos modernos de Unity 6
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Configuración de la Ronda")]
    public float tiempoRonda = 60f;
    private float cronometro = 0f;
    private bool juegoTerminado = false;

    [Header("Referencias de la Interfaz (UI)")]
    public TMP_Text textoVidas;
    public TMP_Text textoTiempo;
    public GameObject panelFinal;
    public TMP_Text textoResultado;

    private PlayerController jugador;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f; // Asegura que el juego corra al reiniciar
        jugador = FindObjectOfType<PlayerController>();
        
        if (panelFinal != null) panelFinal.SetActive(false);
    }

    void Update()
    {
        if (juegoTerminado) return;

        // 1. Mostrar las vidas del jugador en pantalla
        if (jugador != null)
        {
            textoVidas.text = "Vidas: " + jugador.vidas;
        }

        // 2. Controlar y mostrar el tiempo restante
        cronometro += Time.deltaTime;
        float tiempoRestante = Mathf.Max(0f, tiempoRonda - cronometro);
        textoTiempo.text = "Tiempo: " + tiempoRestante.ToString("F0") + "s";

        // 3. Condición de Victoria (Sobrevivir el minuto)
        if (tiempoRestante <= 0f)
        {
            TerminarPartida(true);
        }
    }

    public void PerderJuego()
    {
        textoVidas.text = "Vidas: 0";
        TerminarPartida(false);
    }

    void TerminarPartida(bool gano)
    {
        juegoTerminado = true;
        Time.timeScale = 0f; // Congela el juego
        
        if (panelFinal != null) panelFinal.SetActive(true);

        if (textoResultado != null)
        {
            if (gano)
            {
                textoResultado.text = "¡VICTORIA!";
                textoResultado.color = Color.green;
            }
            else
            {
                textoResultado.text = "GAME OVER";
                textoResultado.color = Color.red;
            }
        }
    }

    // Esta función la usará el botón para volver a jugar
    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}