using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Configuración Base")]
    public float velocidadBase = 3.5f; 
    private float velocidadActual;
    private Transform jugador;

    void Start()
    {
        // 1. Buscar al jugador automáticamente
        GameObject jugadorObj = GameObject.Find("Jugador");
        if (jugadorObj != null) 
        {
            jugador = jugadorObj.transform;
        }

        // 2. Configurar la velocidad inicial
        velocidadActual = velocidadBase;

        // LÓGICA DE DIFICULTAD Y COLORES POR NIVEL
        if (GameManager.Instance != null)
        {
            int nivel = GameManager.Instance.nivelActual;

            // Aumentar velocidad por nivel
            velocidadActual += (nivel - 1) * 0.8f; 

            // Cambiar color por nivel
            SpriteRenderer spriteComp = GetComponent<SpriteRenderer>();
            if (spriteComp != null)
            {
                if (nivel == 2) spriteComp.color = new Color(0.4f, 1f, 0.4f);      // Nivel 2: Verde
                else if (nivel == 3) spriteComp.color = new Color(0.9f, 0.3f, 1f); // Nivel 3: Morado
                else if (nivel >= 4) spriteComp.color = new Color(1f, 0.4f, 0.4f); // Nivel 4+:  
            }
        }
    }

    void Update()
    {
        if (jugador == null) return;

        // PERSECUCIÓN
        transform.position = Vector2.MoveTowards(
            transform.position, 
            jugador.position, 
            velocidadActual * Time.deltaTime
        );
    }

    // LÓGICA DE COLISIÓN PARA MORIR Y DESTRUIR BALA
    void OnTriggerEnter2D(Collider2D objetoQueMeToco)
    {
        if (objetoQueMeToco.CompareTag("Bala"))
        {
            // 1. DESTRUYE LA BALA (¡Ya no la atraviesa infinitamente!)
            Destroy(objetoQueMeToco.gameObject); 

            // 2. DESTRUYE AL ENEMIGO
            Destroy(gameObject); 
        }
    }
}