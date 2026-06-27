using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float velocidadEnemigo = 3f;
    
    [Header("Ajuste de Delay")]
    public float delayMovimiento = 0.8f; // El enemigo espera antes de moverse
    private float cronometroDelay = 0f;

    private Transform jugador;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        GameObject jugadorObj = GameObject.Find("Jugador");
        if (jugadorObj != null) jugador = jugadorObj.transform;
    }

    void FixedUpdate()
    {
        // Avanzamos el contador de tiempo
        cronometroDelay += Time.fixedDeltaTime;

        // Si no ha pasado el delay de 0.8s, se queda quieto
        if (cronometroDelay < delayMovimiento) return;

        if (jugador != null)
        {
            Vector2 direccion = ((Vector2)jugador.position - rb.position).normalized;
            rb.MovePosition(rb.position + direccion * velocidadEnemigo * Time.fixedDeltaTime);
        }
    }

    // --- SISTEMA PARA MORIR ---
    void OnTriggerEnter2D(Collider2D objetoQueMeToco)
    {
        // Si lo que tocó al enemigo tiene la etiqueta "Bala"
        if (objetoQueMeToco.CompareTag("Bala"))
        {
            Destroy(gameObject); // Se destruye el enemigo
            Destroy(objetoQueMeToco.gameObject); // Se destruye la bala
        }
    }
}