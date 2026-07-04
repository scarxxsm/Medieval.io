using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;
    private Rigidbody2D rb;
    private Vector2 direccionMovimiento;

    [Header("Sistema de Vida")]
    public int vidas = 3;
    public float tiempoInvulnerabilidad = 1f; // Tiempo de espera para volver a recibir daño
    private float cronometroInvulnerabilidad;
    private bool esInvulnerable = false;

    [Header("Sistema de Disparo")]
    public GameObject bala_Prefab;
    public float tiempoEntreDisparosBase = 0.5f; // Ajusta esto en el Inspector (ejemplo: 0.5 o 0.4)
    private float cronometroDisparo = 0f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        direccionMovimiento = new Vector2(moveX, moveY).normalized;

        // Controlar el tiempo de inmunidad
        if (esInvulnerable)
        {
            cronometroInvulnerabilidad += Time.deltaTime;
            if (cronometroInvulnerabilidad >= tiempoInvulnerabilidad)
            {
                esInvulnerable = false;
            }
        }

        // Llamamos a la lógica de disparar constantemente
        ManejarDisparo();
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + direccionMovimiento * velocidad * Time.fixedDeltaTime);
    }

    void ManejarDisparo()
    {
        cronometroDisparo += Time.deltaTime;
        
        // REESCRITURA AGRESIVA POR NIVEL: Dividimos el tiempo base entre el nivel actual
        float nivelActual = GameManager.Instance != null ? GameManager.Instance.nivelActual : 1f;
        float cooldownDisparoReal = tiempoEntreDisparosBase / nivelActual;

        // Candado de seguridad para que no intente disparar a ráfaga de 0 segundos y trabe el juego
        cooldownDisparoReal = Mathf.Max(0.08f, cooldownDisparoReal); 

        // Usa 'cooldownDisparoReal' en tu cronómetro de disparo
        if (cronometroDisparo >= cooldownDisparoReal)
        {
            // Usa 'cooldownDisparoReal' en tu cronómetro de disparo
        if (cronometroDisparo >= cooldownDisparoReal)
        {
            // ====== ¡AQUÍ ESTÁ TU LÍNEA DE DISPARO AUTOMÁTICO! ======
            if (bala_Prefab != null)
            {
                Instantiate(bala_Prefab, transform.position, Quaternion.identity);
            }
            // ========================================================

            cronometroDisparo = 0f; // Reiniciamos el cronómetro de la bala
        }

            cronometroDisparo = 0f; // Reiniciamos el cronómetro de la bala
        }
    }

    // Detecta cuando un enemigo se queda pegado a nosotros
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo") && !esInvulnerable)
        {
            RecibirDano();
        }
    }

    void RecibirDano()
    {
        vidas--;
        esInvulnerable = true;
        cronometroInvulnerabilidad = 0f;
        
        Debug.LogWarning("¡Te golpearon! Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            AlPerder();
        }
    }

    void AlPerder()
    {
        Debug.LogError("¡GAME OVER! Te quedaste sin vidas.");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PerderJuego(); 
        }
        else
        {
            Time.timeScale = 0f;
        }
    }
}