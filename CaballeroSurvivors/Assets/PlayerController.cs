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
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + direccionMovimiento * velocidad * Time.fixedDeltaTime);
    }

    // Detecta cuando un enemigo se queda pegado a nosotros
    void OnCollisionStay2D(Collision2D collision)
    {
        // Si lo que nos toca tiene la etiqueta "Enemigo" y no somos invulnerables
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