using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Configuración de Niveles")]
    public int nivelActual = 1;
    public float tiempoPorNivel = 60f; // Cada minuto sube de nivel
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
        Time.timeScale = 1f;
        jugador = FindObjectOfType<PlayerController>();
        if (panelFinal != null) panelFinal.SetActive(false);
    }

    void Update()
    {
        if (juegoTerminado) return;

        if (jugador != null)
        {
            textoVidas.text = "Vidas: " + jugador.vidas;
        }

        // Control del reloj del nivel actual
        cronometro += Time.deltaTime;
        float tiempoRestante = Mathf.Max(0f, tiempoPorNivel - cronometro);
        
        // Formato visual: Muestra el nivel y el tiempo restante para el siguiente
        textoTiempo.text = "Nivel: " + nivelActual + " | Sig: " + tiempoRestante.ToString("F0") + "s";

        // Cuando el reloj llega a 0, avanzamos de nivel en lugar de ganar
        if (tiempoRestante <= 0f)
        {
            SiguienteNivel();
        }
    }

    void SiguienteNivel()
    {
        nivelActual++;
        cronometro = 0f; // Reinicia el reloj para el nuevo nivel
        Debug.LogWarning("¡Subiste al Nivel " + nivelActual + "! La horda se vuelve más fuerte.");
    }

    public void PerderJuego()
    {
        juegoTerminado = true;
        Time.timeScale = 0f;
        
        if (panelFinal != null) panelFinal.SetActive(true);

        if (textoResultado != null)
        {
            // Cambia el mensaje para presumir hasta qué nivel logró sobrevivir
            textoResultado.text = "PERDISTE EN NIVEL " + nivelActual;
            textoResultado.color = Color.red;
        }
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}