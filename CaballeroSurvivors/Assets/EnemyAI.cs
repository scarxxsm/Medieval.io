using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float velocidadEnemigo = 3f;
    private Transform jugador;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        GameObject jugadorObj = GameObject.Find("Jugador");
        if (jugadorObj != null)
        {
            jugador = jugadorObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (jugador != null)
        {
            Vector2 direccion = ((Vector2)jugador.position - rb.position).normalized;
            rb.MovePosition(rb.position + direccion * velocidadEnemigo * Time.fixedDeltaTime);
        }
    }
}