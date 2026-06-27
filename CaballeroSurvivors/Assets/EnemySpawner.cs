using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemigoPrefab;
    private Transform jugador;

    [Header("Configuración de Tiempo")]
    public float tiempoRonda = 60f;       
    public float tiempoEntreSpawns = 2f;  

    [Header("Configuración de Radio")]
    public float radioMinimo = 14f;       
    public float radioMaximo = 20f;       

    [Header("Límites del Mapa (40x40)")]
    // Ponemos 18.5f para asegurar que nazcan un poquito antes de llegar a la pared
    public float limiteX = 18.5f;
    public float limiteY = 18.5f;

    private float cronometroRonda = 0f;
    private float cronometroSpawn = 0f;
    private bool rondaActiva = true;

    void Start()
    {
        GameObject jugadorObj = GameObject.Find("Jugador");
        if (jugadorObj != null) jugador = jugadorObj.transform;

        // Enemigos predefinidos seguros dentro del mapa
        SpawnEnemigoFijo(new Vector2(-15f, 15f));
        SpawnEnemigoFijo(new Vector2(15f, -15f));
        SpawnEnemigoFijo(new Vector2(0f, 15f));
    }

    void Update()
    {
        if (!rondaActiva || jugador == null) return;

        cronometroRonda += Time.deltaTime;
        if (cronometroRonda >= tiempoRonda)
        {
            rondaActiva = false;
            Debug.Log("¡Ronda Terminada! Tiempo completado.");
            return;
        }

        cronometroSpawn += Time.deltaTime;
        if (cronometroSpawn >= tiempoEntreSpawns)
        {
            SpawnEnemigoEnRadio();
            cronometroSpawn = 0f; 
        }
    }

    void SpawnEnemigoFijo(Vector2 posicionfija)
    {
        if (enemigoPrefab != null)
        {
            Instantiate(enemigoPrefab, posicionfija, Quaternion.identity);
        }
    }

    void SpawnEnemigoEnRadio()
    {
        if (enemigoPrefab == null) return;

        float angulo = Random.Range(0f, Mathf.PI * 2f);
        float distancia = Random.Range(radioMinimo, radioMaximo);

        // Calcular posición inicial basada en el jugador
        float spawnX = jugador.position.x + Mathf.Cos(angulo) * distancia;
        float spawnY = jugador.position.y + Mathf.Sin(angulo) * distancia;

        // --- EL CANDADO (CLAMP) ---
        // Forzamos a que la posición final jamás se pase de las fronteras del mapa
        spawnX = Mathf.Clamp(spawnX, -limiteX, limiteX);
        spawnY = Mathf.Clamp(spawnY, -limiteY, limiteY);

        Vector2 posicionSpawn = new Vector2(spawnX, spawnY);

        Instantiate(enemigoPrefab, posicionSpawn, Quaternion.identity);
    }
}